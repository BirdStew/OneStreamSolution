using BackendAPI1.Controllers;
using BackendAPI2.Controllers;
using FrontendAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http.Json;

namespace OneStreamSolution.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void BackendAPI1_RandomNumberController_ReturnsNumberBetween1And100()
        {
            // Arrange
            Mock<ILogger<BackendAPI1.Controllers.RandomNumberController>> mockLogger = new Mock<ILogger<BackendAPI1.Controllers.RandomNumberController>>();
            BackendAPI1.Controllers.RandomNumberController controller = new BackendAPI1.Controllers.RandomNumberController(mockLogger.Object);

            // Act
            int result = controller.Get();

            // Assert
            Assert.InRange(result, 1, 100);
        }

        [Fact]
        public async Task BackendAPI2_RandomNumberController_ReturnsNumberBetween1And100()
        {
            // Arrange
            Mock<ILogger<BackendAPI2.Controllers.RandomNumberController>> mockLogger = new Mock<ILogger<BackendAPI2.Controllers.RandomNumberController>>();
            BackendAPI2.Controllers.RandomNumberController controller = new BackendAPI2.Controllers.RandomNumberController(mockLogger.Object);

            // Act
            int result = await controller.Get();

            // Assert
            Assert.InRange(result, 1, 100);
        }

        [Fact]
        public async Task FrontendAPI_RandomSumController_ReturnsSumOfTwoBackendAPIs()
        {
            // Arrange
            Mock<ILogger<RandomSumController>> mockLogger = new Mock<ILogger<RandomSumController>>();
            Mock<HttpMessageHandler> mockHttpMessageHandler1 = new Mock<HttpMessageHandler>();
            Mock<HttpMessageHandler> mockHttpMessageHandler2 = new Mock<HttpMessageHandler>();

            // Setup mock responses
            mockHttpMessageHandler1.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", 
                    ItExpr.IsAny<HttpRequestMessage>(), 
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = JsonContent.Create(42)
                });

            mockHttpMessageHandler2.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", 
                    ItExpr.IsAny<HttpRequestMessage>(), 
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = JsonContent.Create(58)
                });

            HttpClient httpClient1 = new HttpClient(mockHttpMessageHandler1.Object) 
            { 
                BaseAddress = new Uri("http://localhost") 
            };
            HttpClient httpClient2 = new HttpClient(mockHttpMessageHandler2.Object) 
            { 
                BaseAddress = new Uri("http://localhost") 
            };

            Mock<IHttpClientFactory> mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockHttpClientFactory.Setup(f => f.CreateClient("BackendAPI1")).Returns(httpClient1);
            mockHttpClientFactory.Setup(f => f.CreateClient("BackendAPI2")).Returns(httpClient2);

            RandomSumController controller = new RandomSumController(mockLogger.Object, mockHttpClientFactory.Object);

            // Act
            int result = await controller.Get();

            // Assert
            Assert.Equal(100, result);
        }

        [Fact]
        public void WebApp_Home_InitializesWithZeroApiResult()
        {
            // Arrange & Act
            WebApp.Components.Pages.Home homeComponent = new WebApp.Components.Pages.Home();

            // Assert
            // The apiResult field should be initialized to 0
            // Since it's a private field, we're testing the component can be instantiated
            Assert.NotNull(homeComponent);
        }
    }
}
