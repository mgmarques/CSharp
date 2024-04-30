using Microsoft.EntityFrameworkCore;
using AutoMapper;
using GameShopping.CartAPI.Model;
using GameShopping.CartAPI.Model.Context;
using GameShopping.CartAPI.Repository;
using GameShopping.CartAPI.Data.ValueObjects;


namespace UnitTestCartAPI;


public class CartAPIRepositoryUnitTests
{

    IMapper mapper = GameShopping.CartAPI.Config.MappingConfig.RegisterMaps().CreateMapper();
    DbContextOptions<MySQLContext> options;

    // setup
    public CartAPIRepositoryUnitTests()
    {
        options = new DbContextOptionsBuilder<MySQLContext>()
            .UseInMemoryDatabase(databaseName: "CartDatabase")
            .Options;
        _ = Setup();
    }
    public async Task Setup()
    {
        await using (var context = new MySQLContext(options))
        {
            CartHeader cartHeader = new CartHeader
            {
                Id = 1,
                UserId = "mgm3000",
                CouponCode = "GAMER_2024_10"
            };

            context.CartHeaders.Add(cartHeader);
            await context.SaveChangesAsync();

            Product product1 = new Product
            {
                Id = 1,
                Name = "Xbox Series X",
                Price = new decimal(559.99),
                Description = "Xbox Series X, the fastest, most powerful Xbox ever. Explore rich new worlds with 12 teraflops of raw graphic processing power, DirectX ray tracing, a custom SSD, and 4K gaming. Make the most of every gaming minute with Quick Resume, lightning-fast load times, and gameplay of up to 120 FPS—all powered by Xbox Velocity Architecture. Enjoy thousands of games from four generations of Xbox, with hundreds of optimized titles that look and play better than ever. And when you add Xbox Game Pass Ultimate (membership sold separately), you get online multiplayer to play with friends and an instant library of 100+ high-quality games, including day one releases from Xbox Game Studios.*",
                ImageURL = "https://m.media-amazon.com/images/I/61c937dHIvL._SX522_.jpg",
                CategoryName = "Console"
            };
            context.Products.Add(product1);

            Product product2 = new Product
            {
                Id = 2,
                Name = "Darth Vader Helmet Star Wars Black Series",
                Price = new decimal(359.99),
                Description = "Fans can imagine the biggest battles and missions in the Star Wars saga with helmets from The Black Series! With exquisite features and decoration, this series embodies the quality and realism that Star Wars devotees love. Imagine donning the signature helmet of Darth Vader with the Darth Vader Premium Electronic Helmet, featuring collar, mask, and hood pieces for multi-piece, adjustable fit and assembly. When the collar is put on, wearers can activate breathing sound effects. When the mask is fitted to the collar with the secondary button and magnetic holds, helmet sealing sound effects will be activated. Using the primary button, wearers can activate breathing sound effects, and when removing the mask, activate helmet removal sounds. With movie-accurate sound effects and premium interior and exterior design, this helmet delivers on the iconic presentation and detail of roleplay items from Star Wars The Black Series. Additional products each sold separately. Star Wars products are produced by Hasbro under license from Lucasfilm Ltd. Hasbro and all related terms are trademarks of Hasbro.",
                ImageURL = "https://m.media-amazon.com/images/I/7100QE31gZL._AC_SY879_.jpge",
                CategoryName = "Replicase"
            };
            context.Products.Add(product2);

            Product product3 = new Product
            {
                Id = 3,
                Name = "PDP Victrix Pro BFG Wireless Gaming Controller",
                Price = new decimal(99.99),
                Description = "PDP Victrix Pro BFG Wireless Gaming Controller for Xbox Series X|Series S, Xbox One, and Windows 10/11, Dolby Atmos Audio, Remappable Buttons, Customizable Triggers/Paddles/D-Pad, PC App, White",
                ImageURL = "https://m.media-amazon.com/images/I/61h-zQxJhOL._SX522_.jpg",
                CategoryName = "Control"
            };
            context.Products.Add(product3);
            await context.SaveChangesAsync();

            CartDetail cartDetail1 = new CartDetail
            {
                Id = 1,
                CartHeaderId = 1,
                CartHeader = cartHeader,
                ProductId = 1,
                Product = product1,
                Count = 2
            };
            context.CartDetails.Add(cartDetail1);

            CartDetail cartDetail2 = new CartDetail
            {
                Id = 2,
                CartHeaderId = 1,
                CartHeader = cartHeader,
                ProductId = 2,
                Product = product2,
                Count = 3
            };

            context.CartDetails.Add(cartDetail2);


            CartDetail cartDetail3 = new CartDetail
            {
                Id = 3,
                CartHeaderId = 1,
                CartHeader = cartHeader,
                ProductId = 3,
                Product = product3,
                Count = 1
            };

            context.CartDetails.Add(cartDetail3);

            await context.SaveChangesAsync();

            Cart cart = new Cart
            {
                CartHeader = cartHeader,
                CartDetails = new List<CartDetail> { cartDetail1, cartDetail2, cartDetail3 }
            };

            await context.SaveChangesAsync();
        }
    }


    [Fact]
    public async Task SaveOrUpdateCartTest()
    {
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
        await using (var context = new MySQLContext(options))
        {
            var repository = new CartRepository(context, mapper);

            var cartUpdate = await repository.SaveOrUpdateCart(cartVO);

            Assert.NotNull(cartUpdate);
            Assert.Equal("mgm3000", cartUpdate.CartHeader.UserId);
        }
    }

    [Fact]
    public async Task FindCartByUserIdSuccessTest()
    {
        await using (var context = new MySQLContext(options))
        {
            var repository = new CartRepository(context, mapper);
            var cart = await repository.FindCartByUserId("mgm3000");

            Assert.NotNull(cart);
            Assert.Equal("mgm3000", cart.CartHeader.UserId);
        }
    }

    [Fact]
    public async Task FindCartByUserIdNotFound()
    {
        await using (var context = new MySQLContext(options))
        {
            var repository = new CartRepository(context, mapper);
            var cart = await repository.FindCartByUserId("171");
            
            Assert.Empty(cart.CartDetails);
        }
    }

    [Fact]
    public async Task RemoveCouponSuccess()
    {
        await using (var context = new MySQLContext(options))
        {
            var repository = new CartRepository(context, mapper);
            var response = await repository.RemoveCoupon("mgm3000");

            Assert.True(response);
        }
    }

    [Fact]
    public async Task RemoveCouponNotFound()
    {
        await using (var context = new MySQLContext(options))
        {
            var repository = new CartRepository(context, mapper);
            var response = await repository.RemoveCoupon("171");

            Assert.False(response);
        }
    }

    [Fact]
    public async Task ApplyCouponSuccess()
    {
        await using (var context = new MySQLContext(options))
        {
            var repository = new CartRepository(context, mapper);
            var response = await repository.ApplyCoupon("mgm3000", "VALUE_1000");

            Assert.True(response);
        }
    }

    [Fact]
    public async Task ApplyCouponNotFound()
    {
        await using (var context = new MySQLContext(options))
        {
            var repository = new CartRepository(context, mapper);
            var response = await repository.ApplyCoupon("171", "VALUE_1000");

            Assert.False(response);
        }
    }


    [Fact]
    public async Task RemoveFromCartSuccess()
    {
        await using (var context = new MySQLContext(options))
        {
            var repository = new CartRepository(context, mapper);
            var response = await repository.RemoveFromCart(3);

            Assert.True(response);
        }
    }

    [Fact]
    public async Task RemoveFromCartNotFound()
    {
        await using (var context = new MySQLContext(options))
        {
            var repository = new CartRepository(context, mapper);
            var response = await repository.RemoveFromCart(7);

            Assert.False(response);
        }
    }

    [Fact]
    public async Task ClearCartSuccess()
    {
        await using (var context = new MySQLContext(options))
        {
            var repository = new CartRepository(context, mapper);
            var response = await repository.ClearCart("mgm3000");

            Assert.True(response);
        }
    }

    [Fact]
    public async Task ClearCartNotFound()
    {
        await using (var context = new MySQLContext(options))
        {
            var repository = new CartRepository(context, mapper);
            var response = await repository.ClearCart("171");

            Assert.False(response);
        }
    }
}