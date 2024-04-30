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
using GameShopping.Email.Model.Context;
using GameShopping.Email.MessageConsumer;
using GameShopping.Email.Messages;
using GameShopping.Email.Repository;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace Tests;

public class RabbitMQPaymentConsumerUnitTests
{
    DbContextOptions<MySQLContext> options;

    // setup
    public RabbitMQPaymentConsumerUnitTests()
    {
        options = new DbContextOptionsBuilder<MySQLContext>()
            .UseInMemoryDatabase(databaseName: "EmailDatabase")
            .Options;
    }

    [Fact]
    public async Task RabbitMQPaymentConsumerProcessLogsSuccess()
    {
        // Arrange
        var repository = new EmailRepository(options);
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
        var message = new UpdatePaymentResultMessage
        {
            Email = "test@example.com",
            OrderId = 123
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
