using GameShopping.Web.Models;
using GameShopping.Web.Services;

namespace UnitTestWeb;

public class WebServicesUnitTests
{
    [Fact]
    public async Task GetCouponSuccessl()
    {
        // Arrange
        var code = "TEST";
        var token = "TEST.133.12421-fef-3-sdfs";
        var expectedCoupon = new CouponViewModel { Id = 1, CouponCode = "TEST", DiscountAmount = 20 };

        var mockHandler = new MockHttpCouponHandler();
        var httpClient = new HttpClient(mockHandler)
        {
            // Dummy base address
            BaseAddress = new Uri("http://localhost"), 
        };
        var couponService = new CouponService(httpClient);

        // Act
        var actualCoupon = await couponService.GetCoupon(code, token);

        // Assert
        Assert.NotNull(actualCoupon);
        Assert.Equal(expectedCoupon.DiscountAmount, actualCoupon.DiscountAmount);
        Assert.Equal(expectedCoupon.CouponCode, actualCoupon.CouponCode);
    }

    [Fact]
    public async Task FindAllProductsSuccess()
    {
        // Arrange
        var token = "TEST.133.12421-fef-3-sdfs";

        var mockHandler = new MockHttpProductListHandler();
        var httpClient = new HttpClient(mockHandler)
        {
            // Dummy base address
            BaseAddress = new Uri("http://localhost"),
        };
        var productService = new ProductService(httpClient);

        // Act
        var actualProduct = await productService.FindAllProducts(token);

        // Assert
        Assert.NotNull(actualProduct);
    }


    [Fact]
    public async Task FindProductByIdSuccess()
    {
        // Arrange
        var token = "TEST.133.12421-fef-3-sdfs";

        var mockHandler = new MockHttpProductHandler();
        var httpClient = new HttpClient(mockHandler)
        {
            // Dummy base address
            BaseAddress = new Uri("http://localhost"),
        };
        var productService = new ProductService(httpClient);

        // Act
        var actualProduct = await productService.FindProductById(1, token);

        // Assert
        Assert.NotNull(actualProduct);
    }

    [Fact]
    public async Task CreateProductSuccess()
    {
        // Arrange
        var token = "TEST.133.12421-fef-3-sdfs";
        var productVM = new ProductViewModel
        {
            Name = "Produt Unit Test",
            Price = 20,
            Description = "Product Test",
            CategoryName = "Unit Test",
            ImageURL = "localhost",
            Count = 1
        };

        var mockHandler = new MockHttpProductHandler();
        var httpClient = new HttpClient(mockHandler)
        {
            // Dummy base address
            BaseAddress = new Uri("http://localhost"),
        };
        var productService = new ProductService(httpClient);

        // Act
        var actualProduct = await productService.CreateProduct(productVM, token);

        // Assert
        Assert.NotNull(actualProduct);
    }


    [Fact]
    public async Task UpdateProductSuccess()
    {
        // Arrange
        var token = "TEST.133.12421-fef-3-sdfs";
        var productVM = new ProductViewModel
        {
            Name = "Produt Unit Test",
            Price = 20,
            Description = "Product Test",
            CategoryName = "Unit Test",
            ImageURL = "localhost",
            Count = 1
        };

        var mockHandler = new MockHttpProductHandler();
        var httpClient = new HttpClient(mockHandler)
        {
            // Dummy base address
            BaseAddress = new Uri("http://localhost"),
        };
        var productService = new ProductService(httpClient);

        // Act
        var actualProduct = await productService.UpdateProduct(productVM, token);

        // Assert
        Assert.NotNull(actualProduct);
    }

    [Fact]
    public void DeleteProductRefuse()
    {
        // Arrange
        var token = "TEST.133.12421-fef-3-sdfs";

        var httpClient = new HttpClient()
        {
            // Dummy base address
            BaseAddress = new Uri("http://localhost"),
        };
        var productService = new ProductService(httpClient);

        // Assert
        _ = Assert.ThrowsAsync<HttpRequestException>(() => productService.DeleteProductById(1, token));
    }


    [Fact]
    public async Task FindCartByUserIdSuccess()
    {
        // Arrange
        var token = "TEST.133.12421-fef-3-sdfs";

        var mockHandler = new MockHttpCartHandler();
        var httpClient = new HttpClient(mockHandler)
        {
            // Dummy base address
            BaseAddress = new Uri("http://localhost"),
        };
        var cartService = new CartService(httpClient);

        // Act
        var actualCart = await cartService.FindCartByUserId("mgm3000", token);

        // Assert
        Assert.NotNull(actualCart);
    }

    [Fact]
    public async Task AddItemToCartSuccess()
    {
        // Arrange
        var token = "TEST.133.12421-fef-3-sdfs";
        ProductViewModel productVM = new ProductViewModel
        {
            Id = 4,
            Name = "Wireless Gaming Controller",
            Price = new decimal(39.99),
            Description = "Wireless Gaming Controller for Xbox One, and Windows 10/11 Black",
            ImageURL = "https://m.media-amazon.com/images/I/61h-zQxJhOL._SX522_.jpg",
            CategoryName = "Control"
        };

        CartHeaderViewModel cartHeaderVM = new CartHeaderViewModel
        {
            Id = 100,
            UserId = "mgm3000",
            CouponCode = "GAMER_2024_10"
        };
        CartViewModel cartVM = new CartViewModel
        {
            CartHeader = cartHeaderVM,
            CartDetails = new CartDetailViewModel[] {
                new CartDetailViewModel {
                    Id = 100,
                    CartHeaderId = 100, CartHeader = cartHeaderVM,
                    ProductId = 4 , Product = productVM,
                    Count = 1 } }
        };

        var mockHandler = new MockHttpCartHandler();
        var httpClient = new HttpClient(mockHandler)
        {
            // Dummy base address
            BaseAddress = new Uri("http://localhost"),
        };
        var cartService = new CartService(httpClient);

        // Act
        var actualCart = await cartService.AddItemToCart(cartVM, token);

        // Assert
        Assert.NotNull(actualCart);
    }


    [Fact]
    public async Task UpdateCartSuccess()
    {
        // Arrange
        var token = "TEST.133.12421-fef-3-sdfs";
        ProductViewModel productVM = new ProductViewModel
        {
            Id = 4,
            Name = "Wireless Gaming Controller",
            Price = new decimal(39.99),
            Description = "Wireless Gaming Controller for Xbox One, and Windows 10/11 Black",
            ImageURL = "https://m.media-amazon.com/images/I/61h-zQxJhOL._SX522_.jpg",
            CategoryName = "Control"
        };

        CartHeaderViewModel cartHeaderVM = new CartHeaderViewModel
        {
            Id = 100,
            UserId = "mgm3000",
            CouponCode = "GAMER_2024_10"
        };
        CartViewModel cartVM = new CartViewModel
        {
            CartHeader = cartHeaderVM,
            CartDetails = new CartDetailViewModel[] {
                new CartDetailViewModel {
                    Id = 100,
                    CartHeaderId = 100, CartHeader = cartHeaderVM,
                    ProductId = 4 , Product = productVM,
                    Count = 1 } }
        };

        var mockHandler = new MockHttpCartHandler();
        var httpClient = new HttpClient(mockHandler)
        {
            // Dummy base address
            BaseAddress = new Uri("http://localhost"),
        };
        var cartService = new CartService(httpClient);

        // Act
        var actualCart = await cartService.UpdateCart(cartVM, token);

        // Assert
        Assert.NotNull(actualCart);
    }

    [Fact]
    public void RemoveFromCartRefuse()
    {
        // Arrange
        var token = "TEST.133.12421-fef-3-sdfs";

        var httpClient = new HttpClient()
        {
            // Dummy base address
            BaseAddress = new Uri("http://localhost"),
        };
        var cartService = new CartService(httpClient);

        // Assert
        _ = Assert.ThrowsAsync<HttpRequestException>(() => cartService.RemoveFromCart(1, token));
    }


    [Fact]
    public void RemoveCouponRefuse()
    {
        // Arrange
        var token = "TEST.133.12421-fef-3-sdfs";

        var httpClient = new HttpClient()
        {
            // Dummy base address
            BaseAddress = new Uri("http://localhost"),
        };
        var cartService = new CartService(httpClient);

        // Assert
        _ = Assert.ThrowsAsync<HttpRequestException>(() => cartService.RemoveCoupon("TEST_CODE", token));
    }

    [Fact]
    public async Task ApplyCouponRefuse()
    {
        // Arrange
        var token = "TEST.133.12421-fef-3-sdfs";
        ProductViewModel productVM = new ProductViewModel
        {
            Id = 4,
            Name = "Wireless Gaming Controller",
            Price = new decimal(39.99),
            Description = "Wireless Gaming Controller for Xbox One, and Windows 10/11 Black",
            ImageURL = "https://m.media-amazon.com/images/I/61h-zQxJhOL._SX522_.jpg",
            CategoryName = "Control"
        };

        CartHeaderViewModel cartHeaderVM = new CartHeaderViewModel
        {
            Id = 100,
            UserId = "mgm3000",
            CouponCode = "GAMER_2024_10"
        };
        CartViewModel cartVM = new CartViewModel
        {
            CartHeader = cartHeaderVM,
            CartDetails = new CartDetailViewModel[] {
                new CartDetailViewModel {
                    Id = 100,
                    CartHeaderId = 100, CartHeader = cartHeaderVM,
                    ProductId = 4 , Product = productVM,
                    Count = 1 } }
        };

        var httpClient = new HttpClient()
        {
            // Dummy base address
            BaseAddress = new Uri("http://localhost"),
        };
        var cartService = new CartService(httpClient);

        // Assert
        _ = Assert.ThrowsAsync<HttpRequestException>(() => cartService.ApplyCoupon(cartVM, token));
    }
}
