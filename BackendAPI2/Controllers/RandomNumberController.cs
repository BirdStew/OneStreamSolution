using Microsoft.AspNetCore.Mvc;

namespace BackendAPI1.Controllers
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
        public int Get()
        {
            int randomNumber = Random.Shared.Next(1, 100);

            return randomNumber;
        }
    }
}
