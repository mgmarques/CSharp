using Microsoft.EntityFrameworkCore;
using GameShopping.ProductAPI.Model;
using GameShopping.ProductAPI.Model.Context;
using GameShopping.ProductAPI.Repository;
using AutoMapper;
using GameShopping.ProductAPI.Data.ValueObjects;


namespace UnitTestProductAPI;


public class ProductAPIRepositoryUnitTests
{

    IMapper mapper = GameShopping.ProductAPI.Config.MappingConfig.RegisterMaps().CreateMapper();
    DbContextOptions<MySQLContext> options;

    public async Task Setup()
    {
        await using (var context = new MySQLContext(options))
        {
            context.Products.Add(new Product
            {
                Id = 2,
                Name = "Xbox Series X",
                Price = new decimal(559.99),
                Description = "Xbox Series X, the fastest, most powerful Xbox ever. Explore rich new worlds with 12 teraflops of raw graphic processing power, DirectX ray tracing, a custom SSD, and 4K gaming. Make the most of every gaming minute with Quick Resume, lightning-fast load times, and gameplay of up to 120 FPS—all powered by Xbox Velocity Architecture. Enjoy thousands of games from four generations of Xbox, with hundreds of optimized titles that look and play better than ever. And when you add Xbox Game Pass Ultimate (membership sold separately), you get online multiplayer to play with friends and an instant library of 100+ high-quality games, including day one releases from Xbox Game Studios.*",
                ImageURL = "https://m.media-amazon.com/images/I/61c937dHIvL._SX522_.jpg",
                CategoryName = "Console"
            });
            context.Products.Add(new Product
            {
                Id = 3,
                Name = "Darth Vader Helmet Star Wars Black Series",
                Price = new decimal(359.99),
                Description = "Fans can imagine the biggest battles and missions in the Star Wars saga with helmets from The Black Series! With exquisite features and decoration, this series embodies the quality and realism that Star Wars devotees love. Imagine donning the signature helmet of Darth Vader with the Darth Vader Premium Electronic Helmet, featuring collar, mask, and hood pieces for multi-piece, adjustable fit and assembly. When the collar is put on, wearers can activate breathing sound effects. When the mask is fitted to the collar with the secondary button and magnetic holds, helmet sealing sound effects will be activated. Using the primary button, wearers can activate breathing sound effects, and when removing the mask, activate helmet removal sounds. With movie-accurate sound effects and premium interior and exterior design, this helmet delivers on the iconic presentation and detail of roleplay items from Star Wars The Black Series. Additional products each sold separately. Star Wars products are produced by Hasbro under license from Lucasfilm Ltd. Hasbro and all related terms are trademarks of Hasbro.",
                ImageURL = "https://m.media-amazon.com/images/I/7100QE31gZL._AC_SY879_.jpge",
                CategoryName = "Replicase"
            });
            context.Products.Add(new Product
            {
                Id = 4,
                Name = "PDP Victrix Pro BFG Wireless Gaming Controller",
                Price = new decimal(99.99),
                Description = "PDP Victrix Pro BFG Wireless Gaming Controller for Xbox Series X|Series S, Xbox One, and Windows 10/11, Dolby Atmos Audio, Remappable Buttons, Customizable Triggers/Paddles/D-Pad, PC App, White",
                ImageURL = "https://m.media-amazon.com/images/I/61h-zQxJhOL._SX522_.jpg",
                CategoryName = "Control"
            });
            await context.SaveChangesAsync();
        }
    }

    // setup
    public ProductAPIRepositoryUnitTests()
    {
        options = new DbContextOptionsBuilder<MySQLContext>()
            .UseInMemoryDatabase(databaseName: "ProductDatabase")
            .Options;
        _ = Setup();
    }

    [Fact]
    public async Task CreateProductTest()
    {

        ProductVO vo = new ProductVO
            {
                Id = 1,
                Name = "PlayStation 5 Digital Edition",
                Price = new decimal(569.9),
                Description = "<li>Lightning Speed - Harness the power of a custom CPU, GPU, and SSD with Integrated I/O that rewrite the rules of what a PlayStation console can do.</li>\r\n<li>Stunning Games - Marvel at incredible graphics and experience new PS5 features.</li>\r\n<li>Breathtaking Immersion - Discover a deeper gaming experience with support for haptic feedback, adaptive triggers, and 3D Audio technology.</li>\r\n<li>Model Number CFI-1102B</li>",
                ImageURL = "https://m.media-amazon.com/images/I/61loOpDhuML._SX522_.jpg",
                CategoryName = "Console"
            };

        await using (var context = new MySQLContext(options))
        {
            var repository = new ProductRepository(context, mapper);
            var product = await repository.Create(vo);

            Assert.NotNull(product);
            Assert.Equal("PlayStation 5 Digital Edition", product.Name);
        }
    }

    [Fact]
    public async Task FindByIdSuccessTest()
    {
        await using (var context = new MySQLContext(options))
        {
            var repository = new ProductRepository(context, mapper);
            var product = await repository.FindById(2);

            Assert.NotNull(product);
            Assert.Equal("Xbox Series X", product.Name);
        }
    }

    [Fact]
    public async Task FindByIdNotFound()
    {
        await using (var context = new MySQLContext(options))
        {
            var repository = new ProductRepository(context, mapper);
            var product = await repository.FindById(9);

            Assert.Null(product);
        }
    }

    [Fact]
    public async Task UpdateProductTest()
    {

        ProductVO vo = new ProductVO
        {
            Id = 4,
            Name = "Universal Wireless Gaming Controller",
            Price = new decimal(99.99),
            Description = "PDP Victrix Pro BFG Wireless Gaming Controller for Xbox Series X|Series S, Xbox One, and Windows 10/11, Dolby Atmos Audio, Remappable Buttons, Customizable Triggers/Paddles/D-Pad, PC App, White",
            ImageURL = "https://m.media-amazon.com/images/I/61h-zQxJhOL._SX522_.jpg",
            CategoryName = "Control"
        };

        await using (var context = new MySQLContext(options))
        {
            var repository = new ProductRepository(context, mapper);
            var product = await repository.Update(vo);

            Assert.NotNull(product);
            Assert.Equal("Universal Wireless Gaming Controller", product.Name);
        }
    }

    [Fact]
    public async Task FindAllReturnsListProducts()
    {
        await using (var context = new MySQLContext(options))
        {
            var repository = new ProductRepository(context, mapper);
            var products = await repository.FindAll();

            Assert.NotNull(products);
            Assert.Equal(3, products.Count());
        }
    }


    [Fact]
    public async Task DeleteSuccess()
    {
        await using (var context = new MySQLContext(options))
        {
            var repository = new ProductRepository(context, mapper);
            var response = await repository.Delete(2);

            Assert.True(response);
        }
    }

    [Fact]
    public async Task DeleteNotFound()
    {
        await using (var context = new MySQLContext(options))
        {
            var repository = new ProductRepository(context, mapper);
            var response = await repository.Delete(7);

            Assert.False(response);
        }
    }
}