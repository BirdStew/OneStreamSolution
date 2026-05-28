using Microsoft.AspNetCore.Mvc;

namespace FrontendAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RandomSumController : ControllerBase
    {
        private readonly ILogger<RandomSumController> _logger;
        private readonly HttpClient _httpClient1;
        private readonly HttpClient _httpClient2;

        public RandomSumController(ILogger<RandomSumController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient1 = httpClientFactory.CreateClient("BackendAPI1");
            _httpClient2 = httpClientFactory.CreateClient("BackendAPI2");
        }

        [HttpGet(Name = "GetRandomSum")]
        public async Task<int> Get()
        {
            _logger.LogInformation("GetRandomSum called - starting async calls to backend APIs");

             // Call both backend APIs asynchronously
            Task<int> task1 = _httpClient1.GetFromJsonAsync<int>("/RandomNumber");
            Task<int> task2 = _httpClient2.GetFromJsonAsync<int>("/RandomNumber");

            // Wait for both tasks to complete
            await Task.WhenAll(task1, task2);

            int randomSum = task1.Result + task2.Result;

            _logger.LogInformation("BackendAPI1 returned: {Value1}, BackendAPI2 returned: {Value2}, Sum: {Sum}",
                task1.Result, task2.Result, randomSum);

            return randomSum;
        }
    }
}
