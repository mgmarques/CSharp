using Moq;
using Microsoft.AspNetCore.Mvc;
using GameShopping.Web.Services.IServices;
using GameShopping.Web.Controllers;


namespace UnitTestWeb
{
    public class ProductControlerUnitTests
    {
        [Fact]
        public async Task ProductCreateReturnsViewResult()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();

            var controller = new ProductController(productServiceMock.Object);

            // Act
            var result = await controller.ProductCreate();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task ProductIndexReturnsViewResult()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();

            var controller = new ProductController(productServiceMock.Object);

            // Act
            var result = await controller.ProductIndex();

            // Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}
