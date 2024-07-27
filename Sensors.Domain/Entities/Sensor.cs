using Sensors.Domain.Entities.Base;

namespace Sensors.Domain.Entities
{
    public class Sensor : BaseEntity
    {
        public string GroupName { get; set; }
        public int SensorNumber { get; set; }
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public double PositionZ { get; set; }
        public double WaterTemperature { get; set; }
        public virtual ICollection<SensorState> SensorStates { get; set; }
    }
}
