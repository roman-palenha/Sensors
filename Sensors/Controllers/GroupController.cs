using Microsoft.AspNetCore.Mvc;
using Sensors.Business.Services.Interfaces;

namespace Sensors.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly ISensorService _service;

        public GroupController(ISensorService service)
        {
            _service = service;
        }

        [HttpGet("{groupName}/temperature/average")]
        public IActionResult GetAverageTemperature(string groupName, DateTime? startDate, DateTime? endDate)
        {
            var result = _service.GetAverageWaterTemp(startDate, endDate, groupName);
            return Ok(result);
        }

        [HttpGet("{groupName}/fish/top/{n}")]
        public IActionResult GetTopFish(string groupName, int n, DateTime? startDate, DateTime? endDate)
        {
            var result = _service.GetTopFishes(n, startDate, endDate, groupName);
            return Ok(result);
        }
    }
}
