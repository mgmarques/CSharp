using Moq;
using Microsoft.AspNetCore.Mvc;
using GameShopping.CartAPI.Repository;
using GameShopping.CartAPI.Data.ValueObjects;
using GameShopping.CartAPI.Controllers;
using GameShopping.CartAPI.RabbitMQSender;
using GameShopping.CartAPI.Model;


namespace UnitTestCartAPI;

public class CartControllerTest
{
    [Fact]
    public async Task CartControllerSaveOrUpdateCartSuccess()
    {
        // Arrange
        var mockCartRepo = new Mock<ICartRepository>();
        var mockCouponRepo = new Mock<ICouponRepository>();
        var mockRabbitMQRepo = new Mock<IRabbitMQMessageSender>();

        ProductVO productVO = new ProductVO
        {
            Id = 4,
            Name = "Wireless Gaming Controller",
            Price = new decimal(39.99),
            Description = "Wireless Gaming Controller for Xbox One, and Windows 10/11 Black",
            ImageURL = "https://m.media-amazon.com/images/I/61h-zQxJhOL._SX522_.jpg",
            CategoryName = "Control"
        };

        CartHeaderVO cartHeaderVO = new CartHeaderVO
        {
            Id = 100,
            UserId = "mgm3000",
            CouponCode = "GAMER_2024_10"
        };
        CartVO cartVO = new CartVO
        {
            CartHeader = cartHeaderVO,
            CartDetails = new CartDetailVO[] {
                new CartDetailVO {
                    Id = 100,
                    CartHeaderId = 100, CartHeader = cartHeaderVO,
                    ProductId = 4 , Product = productVO,
                    Count = 1 } }
        };

        mockCartRepo.Setup(service => service.SaveOrUpdateCart(It.IsAny<CartVO>())).ReturnsAsync(cartVO);
        var controller = new CartController(mockCartRepo.Object, mockCouponRepo.Object, mockRabbitMQRepo.Object);

        // Act
        var result = await controller.AddCart(cartVO);

        // Assert
        var actionResult = Assert.IsType<ActionResult<CartVO>>(result);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnValue = Assert.IsType<CartVO>(okObjectResult.Value);

        Assert.Equal(cartVO.CartHeader.Id, returnValue.CartHeader.Id);
        Assert.Equal(cartVO.CartHeader.CouponCode, returnValue.CartHeader.CouponCode);
        Assert.Equal(cartVO.CartDetails, returnValue.CartDetails);

        CartHeaderVO cartHeaderVOUpdated = new CartHeaderVO
        {
            Id = 100,
            UserId = "mgm3000",
            CouponCode = "GAMER_2024_15"
        };
        CartVO cartVOUpdated = new CartVO
        {
            CartHeader = cartHeaderVO,
            CartDetails = new CartDetailVO[] {
                new CartDetailVO {
                    Id = 100,
                    CartHeaderId = 100, CartHeader = cartHeaderVOUpdated,
                    ProductId = 4 , Product = productVO,
                    Count = 2 } }
        };

        // Act
        var resultUpdated = await controller.UpdateCart(cartVO);

        // Assert
        var actionResultUpdated = Assert.IsType<ActionResult<CartVO>>(resultUpdated);
        var okObjectResultUpdated = Assert.IsType<OkObjectResult>(actionResultUpdated.Result);
        var returnValueUpdated = Assert.IsType<CartVO>(okObjectResultUpdated.Value);

        Assert.Equal(cartVOUpdated.CartHeader.Id, returnValueUpdated.CartHeader.Id);
        Assert.Equal(cartVOUpdated.CartHeader.CouponCode, returnValueUpdated.CartHeader.CouponCode);
    }

    [Fact]
    public async Task CartControllerFindByIdSuccess()
    {
        // Arrange
        var mockCartRepo = new Mock<ICartRepository>();
        var mockCouponRepo = new Mock<ICouponRepository>();
        var mockRabbitMQRepo = new Mock<IRabbitMQMessageSender>();

        ProductVO productVO = new ProductVO
        {
            Id = 4,
            Name = "Wireless Gaming Controller",
            Price = new decimal(39.99),
            Description = "Wireless Gaming Controller for Xbox One, and Windows 10/11 Black",
            ImageURL = "https://m.media-amazon.com/images/I/61h-zQxJhOL._SX522_.jpg",
            CategoryName = "Control"
        };

        CartHeaderVO cartHeaderVO = new CartHeaderVO
        {
            Id = 100,
            UserId = "mgm3000",
            CouponCode = "GAMER_2024_10"
        };
        CartVO cartVO = new CartVO
        {
            CartHeader = cartHeaderVO,
            CartDetails = new CartDetailVO[] {
                new CartDetailVO {
                    Id = 100,
                    CartHeaderId = 100, CartHeader = cartHeaderVO,
                    ProductId = 4 , Product = productVO,
                    Count = 1 } }
        };

        mockCartRepo.Setup(service => service.FindCartByUserId(It.IsAny<string>())).ReturnsAsync(cartVO);
        var controller = new CartController(mockCartRepo.Object, mockCouponRepo.Object, mockRabbitMQRepo.Object);

        // Act
        var result = await controller.FindById("mgm3000");

        // Assert
        var actionResult = Assert.IsType<ActionResult<CartVO>>(result);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnValue = Assert.IsType<CartVO>(okObjectResult.Value);

        Assert.Equal(cartVO.CartHeader.Id, returnValue.CartHeader.Id);
        Assert.Equal(cartVO.CartHeader.CouponCode, returnValue.CartHeader.CouponCode);
        Assert.Equal(cartVO.CartDetails, returnValue.CartDetails);
    }

    [Fact]
    public async Task CartControllerRemoveFromCartSuccess()
    {
        // Arrange
        var mockCartRepo = new Mock<ICartRepository>();
        var mockCouponRepo = new Mock<ICouponRepository>();
        var mockRabbitMQRepo = new Mock<IRabbitMQMessageSender>();

        mockCartRepo.Setup(service => service.RemoveFromCart(It.IsAny<long>())).ReturnsAsync(true);
        var controller = new CartController(mockCartRepo.Object, mockCouponRepo.Object, mockRabbitMQRepo.Object);

        // Act
        var result = await controller.RemoveCart(1);

        // Assert
        var actionResult = Assert.IsType<ActionResult<CartVO>>(result);
    }

    [Fact]
    public async Task CartControllerApplyCouponSuccess()
    {
        // Arrange
        ProductVO productVO = new ProductVO
        {
            Id = 4,
            Name = "Wireless Gaming Controller",
            Price = new decimal(39.99),
            Description = "Wireless Gaming Controller for Xbox One, and Windows 10/11 Black",
            ImageURL = "https://m.media-amazon.com/images/I/61h-zQxJhOL._SX522_.jpg",
            CategoryName = "Control"
        };

        CartHeaderVO cartHeaderVO = new CartHeaderVO
        {
            Id = 100,
            UserId = "mgm3000",
            CouponCode = "GAMER_2024_10"
        };
        CartVO cartVO = new CartVO
        {
            CartHeader = cartHeaderVO,
            CartDetails = new CartDetailVO[] {
                new CartDetailVO {
                    Id = 100,
                    CartHeaderId = 100, CartHeader = cartHeaderVO,
                    ProductId = 4 , Product = productVO,
                    Count = 1 } }
        };

        var mockCartRepo = new Mock<ICartRepository>();
        var mockCouponRepo = new Mock<ICouponRepository>();
        var mockRabbitMQRepo = new Mock<IRabbitMQMessageSender>();

        mockCartRepo.Setup(service => service.ApplyCoupon(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
        var controller = new CartController(mockCartRepo.Object, mockCouponRepo.Object, mockRabbitMQRepo.Object);

        // Act
        var result = await controller.ApplyCoupon(cartVO);

        // Assert
        var actionResult = Assert.IsType<ActionResult<CartVO>>(result);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(200, Assert.IsType<int>(okObjectResult.StatusCode));
    }

    [Fact]
    public async Task CartControllerRemoveCouponSuccess()
    {
        // Arrange
        ProductVO productVO = new ProductVO
        {
            Id = 4,
            Name = "Wireless Gaming Controller",
            Price = new decimal(39.99),
            Description = "Wireless Gaming Controller for Xbox One, and Windows 10/11 Black",
            ImageURL = "https://m.media-amazon.com/images/I/61h-zQxJhOL._SX522_.jpg",
            CategoryName = "Control"
        };

        CartHeaderVO cartHeaderVO = new CartHeaderVO
        {
            Id = 100,
            UserId = "mgm3000",
            CouponCode = "GAMER_2024_10"
        };
        CartVO cartVO = new CartVO
        {
            CartHeader = cartHeaderVO,
            CartDetails = new CartDetailVO[] {
                new CartDetailVO {
                    Id = 100,
                    CartHeaderId = 100, CartHeader = cartHeaderVO,
                    ProductId = 4 , Product = productVO,
                    Count = 1 } }
        };

        var mockCartRepo = new Mock<ICartRepository>();
        var mockCouponRepo = new Mock<ICouponRepository>();
        var mockRabbitMQRepo = new Mock<IRabbitMQMessageSender>();

        mockCartRepo.Setup(service => service.RemoveCoupon(It.IsAny<string>())).ReturnsAsync(true);
        var controller = new CartController(mockCartRepo.Object, mockCouponRepo.Object, mockRabbitMQRepo.Object);

        // Act
        var result = await controller.ApplyCoupon("171");

        // Assert
        var actionResult = Assert.IsType<ActionResult<CartVO>>(result);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(200, Assert.IsType<int>(okObjectResult.StatusCode));
    }

    [Fact]
    public async Task CartControllerApplyCouponFaild()
    {
        // Arrange
        ProductVO productVO = new ProductVO
        {
            Id = 4,
            Name = "Wireless Gaming Controller",
            Price = new decimal(39.99),
            Description = "Wireless Gaming Controller for Xbox One, and Windows 10/11 Black",
            ImageURL = "https://m.media-amazon.com/images/I/61h-zQxJhOL._SX522_.jpg",
            CategoryName = "Control"
        };

        CartHeaderVO cartHeaderVO = new CartHeaderVO
        {
            Id = 100,
            UserId = "mgm3000",
            CouponCode = "GAMER_2024_10"
        };
        CartVO cartVO = new CartVO
        {
            CartHeader = cartHeaderVO,
            CartDetails = new CartDetailVO[] {
                new CartDetailVO {
                    Id = 100,
                    CartHeaderId = 100, CartHeader = cartHeaderVO,
                    ProductId = 4 , Product = productVO,
                    Count = 1 } }
        };

        var mockCartRepo = new Mock<ICartRepository>();
        var mockCouponRepo = new Mock<ICouponRepository>();
        var mockRabbitMQRepo = new Mock<IRabbitMQMessageSender>();

        mockCartRepo.Setup(service => service.ApplyCoupon(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);
        var controller = new CartController(mockCartRepo.Object, mockCouponRepo.Object, mockRabbitMQRepo.Object);

        // Act
        var result = await controller.ApplyCoupon(cartVO);

        var actionResult = Assert.IsType<ActionResult<CartVO>>(result);
        var notFoundResult = Assert.IsType<NotFoundResult>(actionResult.Result);
        Assert.Equal(404, Assert.IsType<int>(notFoundResult.StatusCode));
        Assert.Null(result.Value);
    }

    [Fact]
    public async Task CartControllerRemoveCouponFaild()
    {
        // Arrange
        ProductVO productVO = new ProductVO
        {
            Id = 4,
            Name = "Wireless Gaming Controller",
            Price = new decimal(39.99),
            Description = "Wireless Gaming Controller for Xbox One, and Windows 10/11 Black",
            ImageURL = "https://m.media-amazon.com/images/I/61h-zQxJhOL._SX522_.jpg",
            CategoryName = "Control"
        };

        CartHeaderVO cartHeaderVO = new CartHeaderVO
        {
            Id = 100,
            UserId = "mgm3000",
            CouponCode = "GAMER_2024_10"
        };
        CartVO cartVO = new CartVO
        {
            CartHeader = cartHeaderVO,
            CartDetails = new CartDetailVO[] {
                new CartDetailVO {
                    Id = 100,
                    CartHeaderId = 100, CartHeader = cartHeaderVO,
                    ProductId = 4 , Product = productVO,
                    Count = 1 } }
        };

        var mockCartRepo = new Mock<ICartRepository>();
        var mockCouponRepo = new Mock<ICouponRepository>();
        var mockRabbitMQRepo = new Mock<IRabbitMQMessageSender>();

        mockCartRepo.Setup(service => service.RemoveCoupon(It.IsAny<string>())).ReturnsAsync(false);
        var controller = new CartController(mockCartRepo.Object, mockCouponRepo.Object, mockRabbitMQRepo.Object);

        // Act
        var result = await controller.ApplyCoupon("171");

        // Assert
        var actionResult = Assert.IsType<ActionResult<CartVO>>(result);
        var notFoundResult = Assert.IsType<NotFoundResult>(actionResult.Result);
        Assert.Equal(404, Assert.IsType<int>(notFoundResult.StatusCode));
        Assert.Null(result.Value);
    }

    [Fact]
    public async Task CartControllerRemoveFromCartFailed()
    {
        // Arrange
        var mockCartRepo = new Mock<ICartRepository>();
        var mockCouponRepo = new Mock<ICouponRepository>();
        var mockRabbitMQRepo = new Mock<IRabbitMQMessageSender>();

        mockCartRepo.Setup(service => service.RemoveFromCart(It.IsAny<long>())).ReturnsAsync(false);
        var controller = new CartController(mockCartRepo.Object, mockCouponRepo.Object, mockRabbitMQRepo.Object);

        // Act
        var result = await controller.RemoveCart(7);

        // Assert
        var actionResult = Assert.IsType<ActionResult<CartVO>>(result);
        var badRequestResult = Assert.IsType<BadRequestResult>(actionResult.Result);
        Assert.Equal(400, Assert.IsType<int>(badRequestResult.StatusCode));
        Assert.Null(result.Value);
    }

    [Fact]
    public async Task CartControllerFindByIdNotFoud()
    {
        // Arrange
        var mockCartRepo = new Mock<ICartRepository>();
        var mockCouponRepo = new Mock<ICouponRepository>();
        var mockRabbitMQRepo = new Mock<IRabbitMQMessageSender>();
        mockCartRepo.Setup(service => service.FindCartByUserId(It.IsAny<string>()));
        var controller = new CartController(mockCartRepo.Object, mockCouponRepo.Object, mockRabbitMQRepo.Object);
        controller.ModelState.AddModelError("error", "some error");

        // Act
        var cart = await controller.FindById("GAMER2026");

        // Assert

        var actionResult = Assert.IsType<ActionResult<CartVO>>(cart);
        var notFoundResult = Assert.IsType<NotFoundResult>(actionResult.Result);
        Assert.Equal(404, Assert.IsType<int>(notFoundResult.StatusCode));
        Assert.Null(cart.Value);
    }


    [Fact]
    public async Task CartControllerAddCartNotFoud()
    {
        // Arrange
        var mockCartRepo = new Mock<ICartRepository>();
        var mockCouponRepo = new Mock<ICouponRepository>();
        var mockRabbitMQRepo = new Mock<IRabbitMQMessageSender>();
        mockCartRepo.Setup(service => service.SaveOrUpdateCart(It.IsAny<CartVO>()));
        var controller = new CartController(mockCartRepo.Object, mockCouponRepo.Object, mockRabbitMQRepo.Object);
        controller.ModelState.AddModelError("error", "some error");

        // Act
        var cart = await controller.AddCart(new CartVO());

        // Assert

        var actionResult = Assert.IsType<ActionResult<CartVO>>(cart);
        var notFoundResult = Assert.IsType<NotFoundResult>(actionResult.Result);
        Assert.Equal(404, Assert.IsType<int>(notFoundResult.StatusCode));
        Assert.Null(cart.Value);
    }

    [Fact]
    public async Task CartControllerUpdateCartNotFoud()
    {
        // Arrange
        var mockCartRepo = new Mock<ICartRepository>();
        var mockCouponRepo = new Mock<ICouponRepository>();
        var mockRabbitMQRepo = new Mock<IRabbitMQMessageSender>();
        mockCartRepo.Setup(service => service.SaveOrUpdateCart(It.IsAny<CartVO>()));
        var controller = new CartController(mockCartRepo.Object, mockCouponRepo.Object, mockRabbitMQRepo.Object);
        controller.ModelState.AddModelError("error", "some error");

        // Act
        var cart = await controller.UpdateCart(new CartVO());

        // Assert

        var actionResult = Assert.IsType<ActionResult<CartVO>>(cart);
        var notFoundResult = Assert.IsType<NotFoundResult>(actionResult.Result);
        Assert.Equal(404, Assert.IsType<int>(notFoundResult.StatusCode));
        Assert.Null(cart.Value);
    }
}
