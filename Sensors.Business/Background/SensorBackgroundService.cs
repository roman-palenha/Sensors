using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Sensors.Business.Dto;
using Sensors.Business.Hubs;
using Sensors.Domain;
using Sensors.Domain.Configs;
using Sensors.Domain.Entities;
using Sensors.Domain.Enum;

namespace Sensors.Business.Background
{
    public class SensorBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IHubContext<SensorHub> _hubContext;
        private readonly Random _random;
        private readonly SensorConfiguration _sensorConfiguration;

        public SensorBackgroundService(
            IServiceScopeFactory serviceScopeFactory,
            IHubContext<SensorHub> hubContext,
            IOptions<SensorConfiguration> sensorConfiguration)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _hubContext = hubContext;
            _random = new Random();
            _sensorConfiguration = sensorConfiguration.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    var sensors = context.Sensors.ToList();
                    var dataForSignal = new List<SensorSignalDto>();
                    foreach (var sensor in sensors)
                    {
                        var temperatureChange = Math.Round((_random.NextDouble() - 0.5) * 0.1, 2);
                        sensor.WaterTemperature = Math.Max(_sensorConfiguration.WaterTemperatureMin, Math.Min(sensor.WaterTemperature + temperatureChange, _sensorConfiguration.WaterTemperatureMax));
                        
                        var sensorState = new SensorState
                        {
                            SensorId = sensor.Id,
                            TimeStamp = DateTime.UtcNow,
                            WaterTemperature = sensor.WaterTemperature,
                            FishCounts = GenerateRandomFishCounts()
                        };

                        context.SensorStates.Add(sensorState);
                        dataForSignal.Add(new SensorSignalDto
                        {
                            GroupName = $"{sensor.GroupName}-{sensor.SensorNumber}",
                            WaterTemperature = sensorState.WaterTemperature,
                            FishSpecies = sensorState.FishCounts.Select(o => new FishSpeciesDto
                            {
                                Count = o.Count,
                                SpeciesName = o.Species
                            }).ToList()
                        });
                    }

                    await _hubContext.Clients.All.SendAsync("ReceiveSensorData", dataForSignal);

                    await context.SaveChangesAsync();

                    await Task.Delay(_sensorConfiguration.TickingPeriod, stoppingToken);
                }
            }
        }

        private List<FishCount> GenerateRandomFishCounts()
        {
            var fishSpeciesValues = Enum.GetValues(typeof(FishEnum));
            int speciesCount = _random.Next(0, 4);

            return Enumerable.Range(0, speciesCount)
                             .Select(_ => new FishCount
                             {
                                 Species = ((FishEnum)fishSpeciesValues.GetValue(_random.Next(fishSpeciesValues.Length))).ToString(),
                                 Count = _random.Next(1, _sensorConfiguration.FishCountMax)
                             }).ToList();
        }
    }
}
