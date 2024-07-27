using Sensors.Domain.Entities.Base;

namespace Sensors.Domain.Entities
{
    public class SensorState : BaseEntity
    {
        public int SensorId { get; set; }
        public virtual Sensor Sensor { get; set; }
        public DateTime TimeStamp { get; set; }
        public double WaterTemperature { get; set; }
        public virtual ICollection<FishCount> FishCounts { get; set; }
    }
}