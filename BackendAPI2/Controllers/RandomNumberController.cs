using Microsoft.AspNetCore.Mvc;

namespace BackendAPI2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RandomNumberController : ControllerBase
    {
        private readonly ILogger<RandomNumberController> _logger;

        public RandomNumberController(ILogger<RandomNumberController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetRandomNumber")]
        public async Task<int> Get()
        {
            int randomNumber = Random.Shared.Next(1, 100);
            
            // Simulate delay
            await Task.Delay(1000);

            return randomNumber;
        }
    }
}
