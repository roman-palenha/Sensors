using Microsoft.EntityFrameworkCore;
using Sensors.Domain.Entities;

namespace Sensors.Domain
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) 
        {
        }

        public virtual DbSet<Sensor> Sensors { get; set; }
        public virtual DbSet<SensorState> SensorStates { get; set; }
        public virtual DbSet<FishCount> FishCounts { get; set; }
    }
}
