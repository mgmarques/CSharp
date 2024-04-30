using Microsoft.EntityFrameworkCore;
using GameShopping.Email.Model.Context;
using GameShopping.Email.Repository;
using GameShopping.Email.Messages;


namespace UnitTestCouponAPI;

public class EmailRepositoryUnitTests
{
    DbContextOptions<MySQLContext> options;

    // setup
    public EmailRepositoryUnitTests()
    {
        options = new DbContextOptionsBuilder<MySQLContext>()
            .UseInMemoryDatabase(databaseName: "EmailDatabase")
            .Options;
    }

    [Fact]
    public async Task LogEmailTest()
    {
        await using (var context = new MySQLContext(options))
        {

            var repository = new EmailRepository(options);

            var message = new UpdatePaymentResultMessage
            {
                Email = "mgmarques3000@gmail.com",
                OrderId = 1
            };

            // Act
            await repository.LogEmail(message);

            // Assert
            var loggedEmail = await context.Emails.FirstOrDefaultAsync();
            Assert.NotNull(loggedEmail);
            Assert.Equal("mgmarques3000@gmail.com", loggedEmail.Email);
        }
    }
}