﻿/*
 * To run this test you need already put to run a RabbitMQ docker image
 * Run the command below in a terminal:
 * docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.13-management
 * 
 * In a production solution, image download and execution can be automated via a bitbucket pipeline.
 * 
 */
using GameShopping.PaymentAPI.Messages;
using GameShopping.PaymentAPI.RabbitMQSender;
using Moq;
using RabbitMQ.Client;

namespace UnitTestPaymentAPI;


public class RabbitMQPaymentSenderUnitTest
{
    [Fact]
    public void RabbitMQSendMessageTest()
    {
        // Arrange
        var mockModel = new Mock<IModel>();
        var mockConnection = new Mock<IConnection>();
        mockConnection.Setup(c => c.CreateModel()).Returns(mockModel.Object);

        // , string queueName
        UpdatePaymentResultMessage messageVO = new UpdatePaymentResultMessage
        {
            OrderId = 1,
            Status = true,
            Email = "mgmarques3000@gmail.com"
        };
        var messageSender = new RabbitMQMessageSender();

        // Act
        messageSender.SendMessage(message: messageVO);

        // Assert
        mockModel.Verify(check => check.BasicPublish(It.IsAny<string>(),
                                                     It.IsAny<string>(),
                                                     It.IsAny<bool>(),
                                                     It.IsAny<IBasicProperties>(),
                                                     It.IsAny<ReadOnlyMemory<byte>>()),
       Times.Never);

    }
}
