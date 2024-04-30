using Microsoft.AspNetCore.Authentication;
using Moq;

namespace UnitTestWeb;
public class MockHttpCouponHandler : HttpMessageHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Simulate your desired response here
        var responseContent = @"{
            ""CouponCode"": ""TEST"",
            ""DiscountAmount"": 20
        }";

        var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
        {
            Content = new StringContent(responseContent, System.Text.Encoding.UTF8, "application/json")
        };
        return response;
    }
}

public class MockHttpProductListHandler : HttpMessageHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Simulate your desired response here
        var responseContent = @"[{
            ""Name"": ""Produt Unit Test"",
            ""Price"": 20,
            ""Description"": ""Product Test"",
            ""CategoryName"": ""Unit Test"",
            ""ImageURL"": ""localhost"",
            ""Count"": 1
        }]";

        var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
        {
            Content = new StringContent(responseContent, System.Text.Encoding.UTF8, "application/json")
        };
        return response;
    }
}

public class MockHttpProductHandler : HttpMessageHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Simulate your desired response here
        var responseContent = @"{
            ""Name"": ""Produt Unit Test"",
            ""Price"": 20,
            ""Description"": ""Product Test"",
            ""CategoryName"": ""Unit Test"",
            ""ImageURL"": ""localhost"",
            ""Count"": 1
        }";

        var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
        {
            Content = new StringContent(responseContent, System.Text.Encoding.UTF8, "application/json")
        };
        return response;
    }
}

public class MockHttpCartHandler : HttpMessageHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Simulate your desired response here
        var responseContent = @"{
            ""CartHeaderViewModel"": {
                ""Id"": 1,
                ""UserId"": ""mgm3000"",
                ""CouponCode"": ""TEST_CODE"",
                ""PurchaseAmount"": 100,
                ""DiscountAmount"": 10,
                ""FirstName"": ""Marcelo"",
                ""LastName"": ""Marques"",
                ""Phone"": ""1234567879"",
                ""Email"": ""mgm@gmail.com"",
                ""CardNumber"": ""263473771313177"",
                ""CVV"": ""124"",
                ""ExpiryMothYear"": ""1227""
            },
            ""CartDetails"": []
        }";

        var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
        {
            Content = new StringContent(responseContent, System.Text.Encoding.UTF8, "application/json")
        };
        return response;
    }

    private void MockHttpContextGetToken(Mock<IHttpContextAccessor> httpContextAccessorMock, string tokenName, string tokenValue)
    {
        var authenticationServiceMock = new Mock<IAuthenticationService>();
        httpContextAccessorMock.Setup(x => x.HttpContext.RequestServices.GetService(typeof(IAuthenticationService)))
            .Returns(authenticationServiceMock.Object);

        authenticationServiceMock.Setup(_ => _.GetTokenAsync(It.IsAny<HttpContext>(), It.IsAny<string>()))
            .Returns(Task.FromResult(tokenValue));
    }

}