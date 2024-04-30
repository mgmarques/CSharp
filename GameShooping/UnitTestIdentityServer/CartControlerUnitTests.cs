using Moq;
using Microsoft.AspNetCore.Mvc;
using GameShopping.Web.Services.IServices;
using GameShopping.Web.Controllers;


namespace UnitTestWeb
{
    public class CartControlerUnitTests
    {
        [Fact]
        public async Task ConfirmationReturnsViewResult()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();
            var cartServiceMock = new Mock<ICartService>();
            var couponServiceMock = new Mock<ICouponService>();

            var controller = new CartController(productServiceMock.Object, cartServiceMock.Object, couponServiceMock.Object);

            // Act
            var result = await controller.Confirmation();

            // Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}
