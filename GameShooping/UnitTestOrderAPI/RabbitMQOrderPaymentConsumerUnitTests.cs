/*
 * To run this test you need already put to run a RabbitMQ docker image
 * Run the command below in a terminal:
 * docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.13-management
 * 
 * In a production solution, image download and execution can be automated via a bitbucket pipeline.
 * 
 */
using Moq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using GameShopping.OrderAPI.Model.Context;
using GameShopping.OrderAPI.MessageConsumer;
using GameShopping.OrderAPI.Messages;
using GameShopping.OrderAPI.Repository;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace Tests;

public class RabbitMQOrderPaymentConsumerUnitTests
{
    DbContextOptions<MySQLContext> options;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) { }
    }

    // setup
    public RabbitMQOrderPaymentConsumerUnitTests()
    {
        options = new DbContextOptionsBuilder<MySQLContext>()
            .UseInMemoryDatabase(databaseName: "OrderDatabase")
            .Options;
    }

    [Fact]
    public async Task RabbitMQOrderPaymentConsumerProcessLogsSuccess()
    {
        // Arrange
        var repository = new OrderRepository(options);
        var repositoryMock = new Mock<RabbitMQPaymentConsumer>();
        var mockConnectionFactory = new Mock<IConnectionFactory>();
        var mockConnection = new Mock<IConnection>();
        var mockChannel = new Mock<IModel>();
        var cancellationToken = new CancellationToken();
        // Setup mock objects
        mockConnectionFactory.Setup(factory => factory.CreateConnection())
                             .Returns(mockConnection.Object);
        mockConnection.Setup(connection => connection.CreateModel())
                      .Returns(mockChannel.Object);

        var consumer = new RabbitMQPaymentConsumer(repository);
        await consumer.StartAsync(cancellationToken); // Start the consumer

        // Create a sample message
        var message = new UpdatePaymentResultVO
        {
            Email = "test@example.com",
            OrderId = 123,
            Status = true
        };
        var serializedMessage = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(serializedMessage);
        var eventArgs = new BasicDeliverEventArgs
        {
            Body = body,
            DeliveryTag = 1
        };

        // Assert
        mockChannel.Verify(channel => channel.BasicAck(1, false), Times.Never);

        await consumer.StopAsync(cancellationToken); // Stop the consumer
    }
}
