using Microsoft.AspNetCore.Mvc;

namespace FrontendAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RandomSumController : ControllerBase
    {
        private readonly ILogger<RandomSumController> _logger;

        public RandomSumController(ILogger<RandomSumController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetRandomSum")]
        public int Get()
        {

            int randomSum = 0;



            return randomSum;
        }
    }
}
