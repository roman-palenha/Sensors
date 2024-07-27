using Sensors.Business.Dto;
using Sensors.Business.Services.Interfaces;
using Sensors.Domain;

namespace Sensors.Business.Services
{
    public class SensorService : ISensorService
    {
        public readonly AppDbContext _dbContext;

        public SensorService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public AverageWaterTempDto GetAverageWaterTemp(DateTime? startDate, DateTime? endDate, string? groupName = "")
        {
            var query = _dbContext.SensorStates.AsQueryable();
            if(startDate.HasValue && endDate.HasValue)
            {
                query = query.Where(o => o.TimeStamp >= startDate.Value && o.TimeStamp <= endDate.Value);
            }

            if (!string.IsNullOrEmpty(groupName))
            {
                query = query.Where(o => groupName.Equals(o.Sensor.GroupName));
            }

            var avgTemp = query.Select(o => o.WaterTemperature).ToList().DefaultIfEmpty(0).Average();
            return new AverageWaterTempDto { AverageWaterTemperature = avgTemp };
        }

        public List<FishSpeciesDto> GetTopFishes(int count, DateTime? startDate, DateTime? endDate, string? groupName = "")
        {
            var query = _dbContext.FishCounts.AsQueryable();
            if (startDate.HasValue && endDate.HasValue)
            {
                query = query.Where(o => o.SensorState.TimeStamp >= startDate.Value && o.SensorState.TimeStamp <= endDate.Value);
            }

            if(!string.IsNullOrEmpty(groupName))
            {
                query = query.Where(o => groupName.Equals(o.SensorState.Sensor.GroupName));
            }

            var result = query.GroupBy(o => o.Species).Select(o => new FishSpeciesDto
            {
                Count = o.Sum(o => o.Count),
                SpeciesName = o.First().Species
            })
                .OrderByDescending(o => o.Count)
                .Take(count)
                .ToList();

            return result;
        }
    }
}
