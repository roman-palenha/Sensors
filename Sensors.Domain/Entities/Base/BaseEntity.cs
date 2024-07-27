using System.ComponentModel.DataAnnotations;

namespace Sensors.Domain.Entities.Base
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
