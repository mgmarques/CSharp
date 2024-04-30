using Moq;
using Microsoft.AspNetCore.Mvc;
using GameShopping.ProductAPI.Repository;
using GameShopping.ProductAPI.Data.ValueObjects;
using GameShopping.ProductAPI.Controllers;

namespace UnitTestProductAPI;

public class ProductControllerTest
{
    [Fact]
    public async Task ProductControllerCreateSucess()
    {
        // Arrange & Act
        var mockRepo = new Mock<IProductRepository>();
        ProductVO vo = new ProductVO
        {
            Id = 1,
            Name = "PlayStation 5 Digital Edition",
            Price = new decimal(569.9),
            Description = "<li>Lightning Speed - Harness the power of a custom CPU, GPU, and SSD with Integrated I/O that rewrite the rules of what a PlayStation console can do.</li>\r\n<li>Stunning Games - Marvel at incredible graphics and experience new PS5 features.</li>\r\n<li>Breathtaking Immersion - Discover a deeper gaming experience with support for haptic feedback, adaptive triggers, and 3D Audio technology.</li>\r\n<li>Model Number CFI-1102B</li>",
            ImageURL = "https://m.media-amazon.com/images/I/61loOpDhuML._SX522_.jpg",
            CategoryName = "Console"
        };
        mockRepo.Setup(service => service.Create(It.IsAny<ProductVO>())).ReturnsAsync(vo);
        var controller = new ProductController(mockRepo.Object);
        // Act
        var result = await controller.Create(vo: vo);

        // Assert
        var actionResult = Assert.IsType<ActionResult<ProductVO>>(result);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnValue = Assert.IsType<Task<ProductVO>>(okObjectResult.Value);
    }

    [Fact]
    public async Task ProductControllerFindByIdReturnsProduct()
    {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();
        ProductVO vo = new ProductVO
        {
            Id = 1,
            Name = "PlayStation 5 Digital Edition",
            Price = new decimal(569.9),
            Description = "<li>Lightning Speed - Harness the power of a custom CPU, GPU, and SSD with Integrated I/O that rewrite the rules of what a PlayStation console can do.</li>\r\n<li>Stunning Games - Marvel at incredible graphics and experience new PS5 features.</li>\r\n<li>Breathtaking Immersion - Discover a deeper gaming experience with support for haptic feedback, adaptive triggers, and 3D Audio technology.</li>\r\n<li>Model Number CFI-1102B</li>",
            ImageURL = "https://m.media-amazon.com/images/I/61loOpDhuML._SX522_.jpg",
            CategoryName = "Console"
        };
        mockRepo.Setup(service => service.FindById(It.IsAny<long>())).ReturnsAsync(vo);
        var controller = new ProductController(mockRepo.Object);

        // Act
        var result = await controller.FindById(1);

        // Assert
        var actionResult = Assert.IsType<ActionResult<ProductVO>>(result);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnValue = Assert.IsType<ProductVO>(okObjectResult.Value);

        Assert.Equal(vo.Id, returnValue.Id);
        Assert.Equal(vo.Name, returnValue.Name);
    }

    [Fact]
    public async Task ProductControllerFindAllReturnsProducts()
    {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();


        List<ProductVO> productsList = new List<ProductVO>
            {
                new ProductVO
                {
                    Id = 1,
                    Name = "PlayStation 5 Digital Edition",
                    Price = new decimal(569.9),
                    Description = "<li>Lightning Speed - Harness the power of a custom CPU, GPU, and SSD with Integrated I/O that rewrite the rules of what a PlayStation console can do.</li>\r\n<li>Stunning Games - Marvel at incredible graphics and experience new PS5 features.</li>\r\n<li>Breathtaking Immersion - Discover a deeper gaming experience with support for haptic feedback, adaptive triggers, and 3D Audio technology.</li>\r\n<li>Model Number CFI-1102B</li>",
                    ImageURL = "https://m.media-amazon.com/images/I/61loOpDhuML._SX522_.jpg",
                    CategoryName = "Console"
                },
                new ProductVO
                {
                    Id = 2,
                    Name = "Xbox Series X",
                    Price = new decimal(559.99),
                    Description = "Xbox Series X, the fastest, most powerful Xbox ever. Explore rich new worlds with 12 teraflops of raw graphic processing power, DirectX ray tracing, a custom SSD, and 4K gaming. Make the most of every gaming minute with Quick Resume, lightning-fast load times, and gameplay of up to 120 FPS—all powered by Xbox Velocity Architecture. Enjoy thousands of games from four generations of Xbox, with hundreds of optimized titles that look and play better than ever. And when you add Xbox Game Pass Ultimate (membership sold separately), you get online multiplayer to play with friends and an instant library of 100+ high-quality games, including day one releases from Xbox Game Studios.*",
                    ImageURL = "https://m.media-amazon.com/images/I/61c937dHIvL._SX522_.jpg",
                    CategoryName = "Console"
                }
            };

        IEnumerable<ProductVO> products = productsList;

        mockRepo.Setup(service => service.FindAll()).ReturnsAsync(products);
        var controller = new ProductController(mockRepo.Object);

        // Act
        var result = await controller.FindAll();

        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<ProductVO>>>(result);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnValue = Assert.IsType<List<ProductVO>>(okObjectResult.Value);
 
        Assert.Equal("Xbox Series X", returnValue[1].Name);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task ProductControllerUpdateSuccess()
    {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();
        ProductVO vo = new ProductVO
        {
            Id = 1,
            Name = "PlayStation 5",
            Price = new decimal(469.9),
            Description = "<li>Lightning Speed - Harness the power of a custom CPU, GPU, and SSD with Integrated I/O that rewrite the rules of what a PlayStation console can do.</li>\r\n<li>Breathtaking Immersion - Discover a deeper gaming experience with support for haptic feedback, adaptive triggers, and 3D Audio technology.",
            ImageURL = "https://m.media-amazon.com/images/I/61loOpDhuML._SX522_.jpg",
            CategoryName = "Console"
        };
        mockRepo.Setup(service => service.Update(It.IsAny<ProductVO>())).ReturnsAsync(vo);
        var controller = new ProductController(mockRepo.Object);

        // Act
        var result = await controller.Update(vo);

        // Assert
        var actionResult = Assert.IsType<ActionResult<ProductVO>>(result);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnValue = Assert.IsType<ProductVO>(okObjectResult.Value);

        Assert.Equal(vo.Id, returnValue.Id);
        Assert.Equal(vo.Name, returnValue.Name);
    }

    [Fact]
    public async Task ProductControllerDeleteSuccess()
    {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();
        mockRepo.Setup(service => service.Delete(It.IsAny<long>())).ReturnsAsync(true);
        var controller = new ProductController(mockRepo.Object);

        // Act
        var result = await controller.Delete(1);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, actionResult.StatusCode);
    }

    [Fact]
    public async Task ProductControllerDeleteFailed()
    {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();
        mockRepo.Setup(service => service.Delete(It.IsAny<long>())).ReturnsAsync(false);
        var controller = new ProductController(mockRepo.Object);

        // Act
        var result = await controller.Delete(7);

        // Assert
        var actionResult = Assert.IsType<BadRequestResult>(result);
        Assert.Equal(400, actionResult.StatusCode);
    }
}
