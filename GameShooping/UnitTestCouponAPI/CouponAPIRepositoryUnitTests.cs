using Microsoft.EntityFrameworkCore;
using GameShopping.CouponAPI.Model;
using GameShopping.CouponAPI.Model.Context;
using GameShopping.CouponAPI.Repository;
using AutoMapper;


namespace UnitTestCouponAPI;


public class CouponAPIRepositoryUnitTests
{

    IMapper mapper = GameShopping.CouponAPI.Config.MappingConfig.RegisterMaps().CreateMapper();
    DbContextOptions<MySQLContext> options;

    public async Task Setup()
    {
        await using (var context = new MySQLContext(options))
        {
            context.Coupons.Add(new Coupon
            {
                Id = 1,
                CouponCode = "GAMER_2024_10",
                DiscountAmount = 10
            });
            context.Coupons.Add(new Coupon
            {
                Id = 2,
                CouponCode = "GAMER_2024_15",
                DiscountAmount = 15
            });
            await context.SaveChangesAsync();
        }
    }

    // setup
    public CouponAPIRepositoryUnitTests()
    {
        options = new DbContextOptionsBuilder<MySQLContext>()
            .UseInMemoryDatabase(databaseName: "CouponDatabase")
            .Options;
        _ = Setup();
    }

    [Fact]
    public async Task GetCouponByCouponCodeTest()
    {
        await using (var context = new MySQLContext(options))
        {
            var repository = new CouponRepository(context, mapper);
            var coupon = await repository.GetCouponByCouponCode("GAMER_2024_10");

            Assert.NotNull(coupon);
            Assert.Equal(10, coupon.DiscountAmount);
        }
    }

    [Fact]
    public async Task CouponAPIRepositoryUnitNotFound()
    {
        await using (var context = new MySQLContext(options))
        {
            var repository = new CouponRepository(context, mapper);
            var coupon = await repository.GetCouponByCouponCode("GAME_2026_10");

            Assert.Null(coupon);
        }
    }
}