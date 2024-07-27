using Microsoft.AspNetCore.Mvc;
using Sensors.Business.Services.Interfaces;

namespace Sensors.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TotalController : ControllerBase
    {
        private readonly ISensorService _service;

        public TotalController(ISensorService service)
        {
            _service = service;
        }

        [HttpGet("temperature/average")]
        public IActionResult GetAverageTemperature(DateTime? startDate, DateTime? endDate)
        {
            var result = _service.GetAverageWaterTemp(startDate, endDate);
            return Ok(result);
        }

        [HttpGet("fish/top/{n}")]
        public IActionResult GetTopFish(int n, DateTime? startDate, DateTime? endDate)
        {
            var result = _service.GetTopFishes(n, startDate, endDate);
            return Ok(result);
        }
    }
}
