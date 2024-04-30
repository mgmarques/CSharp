using Moq;
using Microsoft.AspNetCore.Mvc;
using GameShopping.CouponAPI.Repository;
using GameShopping.CouponAPI.Data.ValueObjects;
using GameShopping.CouponAPI.Controllers;


namespace UnitTestCouponAPI;

public class CouponControllerTest
{
    [Fact]
    public async Task CouponControllerGetCouponByCouponCodeSuccess()
    {
        // Arrange
        var mockRepo = new Mock<ICouponRepository>();
        CouponVO vo = new CouponVO
        {
            Id = 1,
            CouponCode = "GAMER_2024_10",
            DiscountAmount = 10
        };
        mockRepo.Setup(service => service.GetCouponByCouponCode(It.IsAny<string>())).ReturnsAsync(vo);
        var controller = new CouponController(mockRepo.Object);

        // Act
        var result = await controller.GetCouponByCouponCode("GAMER_2024_10");

        // Assert
        var actionResult = Assert.IsType<ActionResult<CouponVO>>(result);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnValue = Assert.IsType<CouponVO>(okObjectResult.Value);

        Assert.Equal(vo.Id, returnValue.Id);
        Assert.Equal(vo.CouponCode, returnValue.CouponCode);
        Assert.Equal(vo.DiscountAmount, returnValue.DiscountAmount);
    }

    [Fact]
    public async Task CouponControllerGetCouponByCouponCodeNotFoud()
    {
        // Arrange
        var mockRepo = new Mock<ICouponRepository>();
        mockRepo.Setup(service => service.GetCouponByCouponCode(It.IsAny<string>()));
        var controller = new CouponController(mockRepo.Object);
        controller.ModelState.AddModelError("error", "some error");

        // Act
        var coupon = await controller.GetCouponByCouponCode("GAMER_2026_15");

        // Assert
        var actionResult = Assert.IsType<ActionResult<CouponVO>>(coupon);
        var notFoundResult = Assert.IsType<NotFoundResult>(actionResult.Result);
        Assert.Equal(404, Assert.IsType<int>(notFoundResult.StatusCode));
        Assert.Null(coupon.Value);
    }
}
