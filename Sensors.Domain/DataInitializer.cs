using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Sensors.Domain.Configs;
using Sensors.Domain.Entities;

namespace Sensors.Domain
{
    public class DataInitializer
    {
        private readonly AppDbContext _context;
        private readonly SensorConfiguration _sensorConfiguration;
        private readonly Random _random;

        public DataInitializer(AppDbContext context, IOptions<SensorConfiguration> sensorConfiguration)
        {
            _context = context;
            _sensorConfiguration = sensorConfiguration.Value;
            _random = new Random();
        }

        public async Task InitializeAsync()
        {
            if (!_context.Sensors.Any())
            {
                var groups = new[]
                                {
                    new GroupConfig { Name = "alpha", MinX = 0.0, MaxX = 10.0, MinY = 0.0, MaxY = 10.0, MinZ = 0.0, MaxZ = 10.0 },
                    new GroupConfig { Name = "beta", MinX = 10.1, MaxX = 20.0, MinY = 0.0, MaxY = 10.0, MinZ = 0.0, MaxZ = 10.0 },
                    new GroupConfig { Name = "gamma", MinX = 20.1, MaxX = 30.0, MinY = 0.0, MaxY = 10.0, MinZ = 0.0, MaxZ = 10.0 },
                    new GroupConfig { Name = "delta", MinX = 30.1, MaxX = 40.0, MinY = 0.0, MaxY = 10.0, MinZ = 0.0, MaxZ = 10.0 },
                    new GroupConfig { Name = "epsilon", MinX = 40.1, MaxX = 50.0, MinY = 0.0, MaxY = 10.0, MinZ = 0.0, MaxZ = 10.0 },
                    new GroupConfig { Name = "zeta", MinX = 50.1, MaxX = 60.0, MinY = 0.0, MaxY = 10.0, MinZ = 0.0, MaxZ = 10.0 },
                    new GroupConfig { Name = "eta", MinX = 60.1, MaxX = 70.0, MinY = 0.0, MaxY = 10.0, MinZ = 0.0, MaxZ = 10.0 },
                    new GroupConfig { Name = "theta", MinX = 70.1, MaxX = 80.0, MinY = 0.0, MaxY = 10.0, MinZ = 0.0, MaxZ = 10.0 },
                    new GroupConfig { Name = "iota", MinX = 80.1, MaxX = 90.0, MinY = 0.0, MaxY = 10.0, MinZ = 0.0, MaxZ = 10.0 }
                };

                foreach (var group in groups)
                {
                    for (int sensorNumber = 1; sensorNumber <= 9; sensorNumber++)
                    {
                        var sensor = new Sensor
                        {
                            GroupName = group.Name,
                            SensorNumber = sensorNumber,
                            PositionX = Math.Round(_random.NextDouble() * (group.MaxX - group.MinX) + group.MinX, 2),
                            PositionY = Math.Round(_random.NextDouble() * (group.MaxY - group.MinY) + group.MinY, 2),
                            PositionZ = Math.Round(_random.NextDouble() * (group.MaxZ - group.MinZ) + group.MinZ, 2),
                            WaterTemperature = Math.Round(_random.NextDouble() * (_sensorConfiguration.WaterTemperatureMax - _sensorConfiguration.WaterTemperatureMin) + _sensorConfiguration.WaterTemperatureMin, 2)
                        };

                        _context.Sensors.Add(sensor);
                    }
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
