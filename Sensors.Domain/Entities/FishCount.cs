using Sensors.Domain.Entities.Base;

namespace Sensors.Domain.Entities
{
    public class FishCount : BaseEntity
    {
        public int SensorStateId { get; set; }
        public virtual SensorState SensorState { get; set; }
        public string Species { get; set; }
        public int Count { get; set; }
    }
}