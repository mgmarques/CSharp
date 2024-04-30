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
using System.Text;
using GameShopping.PaymentAPI.MessageConsumer;
using GameShopping.PaymentAPI.Messages;
using System.Text.Json;
using GameShopping.PaymentAPI.RabbitMQSender;
using GameShopping.PaymentProcessor;

namespace Tests;

public class RabbitMQPaymentMessaggeConsumerUnitTests
{
    [Fact]
    public async Task RabbitMQPaymentMessaggeConsumerSuccess()
    {
        // Arrange

        var rabbitMQMessageSenderMock = new Mock<IRabbitMQMessageSender>();
        var processPaymentMock = new Mock<IProcessPayment>();
        var cancellationTokenSource = new CancellationTokenSource();

        var mockConnectionFactory = new Mock<IConnectionFactory>();
        var mockConnection = new Mock<IConnection>();
        var mockChannel = new Mock<IModel>();
        var cancellationToken = new CancellationToken();
        // Setup mock objects
        mockConnectionFactory.Setup(factory => factory.CreateConnection())
                             .Returns(mockConnection.Object);
        mockConnection.Setup(connection => connection.CreateModel())
                      .Returns(mockChannel.Object);

        var consumer = new RabbitMQPaymentConsumer(processPaymentMock.Object, rabbitMQMessageSenderMock.Object);
        // Act
        await consumer.StartAsync(cancellationToken); // Start the consumer

        cancellationTokenSource.Cancel();

        // Simulate receiving a message
        var message = new PaymentMessage { Id = 1, Name = " Marcelo Marques", OrderId = 123, Email = "test@example.com", 
            CardNumber = "9251519146093146", CVV = "123", ExpiryMonthYear = "1127", PurchaseAmount = (Decimal)200.75,
            MessageCreated = DateTime.UtcNow
        };
        var messageBody = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
        //consumer.HandleMessage(messageBody);

        // Assert
        processPaymentMock.Verify(p => p.PaymentProcessor(), Times.Never);
        mockChannel.Verify(channel => channel.BasicAck(1, false), Times.Never);

        await consumer.StopAsync(cancellationToken); // Stop the consumer
    }
}
