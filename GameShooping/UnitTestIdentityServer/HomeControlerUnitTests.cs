using Moq;
using Microsoft.AspNetCore.Mvc;
using GameShopping.Web.Services.IServices;
using GameShopping.Web.Controllers;


namespace UnitTestWeb
{
    public class HomeControlerUnitTests
    {
        [Fact]
        public async Task HomeIndexReturnsViewResult()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();
            var cartServiceMock = new Mock<ICartService>();

            var controller = new HomeController(null, productServiceMock.Object, cartServiceMock.Object);

            // Act
            var result = await controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void PrivaceReturnsViewResult()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();
            var cartServiceMock = new Mock<ICartService>();

            var controller = new HomeController(null, productServiceMock.Object, cartServiceMock.Object);

            // Act
            var result = controller.Privacy();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void LogoutReturnsSignOutResult()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();
            var cartServiceMock = new Mock<ICartService>();

            var controller = new HomeController(null, productServiceMock.Object, cartServiceMock.Object);

            var mockHttpContext = new Mock<Microsoft.AspNetCore.Http.HttpContext>();
            var mockControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            };
            controller.ControllerContext = mockControllerContext;

            // Act
            var result = controller.Logout();

            // Assert
            Assert.IsType<SignOutResult>(result);
        }
    }
}
