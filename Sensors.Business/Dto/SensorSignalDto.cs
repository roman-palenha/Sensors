namespace Sensors.Business.Dto
{
    public class SensorSignalDto
    {
        public string GroupName { get; set; }
        public double WaterTemperature { get; set; }
        public List<FishSpeciesDto> FishSpecies { get; set; }
    }
}
