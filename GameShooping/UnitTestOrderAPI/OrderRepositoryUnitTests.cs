using Moq;
using Microsoft.EntityFrameworkCore;
using GameShopping.OrderAPI.Model.Context;
using GameShopping.OrderAPI.Repository;
using GameShopping.OrderAPI.Messages;
using GameShopping.OrderAPI.Model;


namespace UnitTestOrderAPI;


public class OrderRepositoryUnitTests
{
    DbContextOptions<MySQLContext> options;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) { }
    }

    // setup
    public OrderRepositoryUnitTests()
    {
        options = new DbContextOptionsBuilder<MySQLContext>()
            .UseInMemoryDatabase(databaseName: "OrderDatabase")
            .Options;
    }

    [Fact]
    public async Task AddOrderTestSuccess()
    {
        await using (var context = new MySQLContext(options))
        {

            var repository = new OrderRepository(options);

            OrderHeader orderHeader = new OrderHeader
            {
                Id = 1,
                Email = "test@example.com",
                UserId = "test",
                FirstName = "Marcelo",
                LastName = "Marques",
                Phone = "55 21 120471947",
                CardNumber = "1259790571752091751997",
                CVV = "417",
                DateTime = DateTime.Now,
                OrderTime = DateTime.Now,
                PaymentStatus = true,
                ExpiryMonthYear = "1028",
                CouponCode = "MGM_2024_10",
                DiscountAmount = 10,
                PurchaseAmount = 100,
                CartTotalItens = 1
            };

            // Act
            var response = await repository.AddOrder(orderHeader);

            // Assert
            Assert.True(response);
        }
    }
}