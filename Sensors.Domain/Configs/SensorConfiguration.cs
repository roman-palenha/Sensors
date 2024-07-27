namespace Sensors.Domain.Configs
{
    public class SensorConfiguration
    {
        public double WaterTemperatureMin { get; set; }
        public double WaterTemperatureMax { get; set; }
        public int FishCountMax { get; set; }
        public TimeSpan TickingPeriod { get; set; }
    }
}
