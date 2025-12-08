using Microsoft.EntityFrameworkCore;
using PerformanceTrackingService.Model;

namespace PerformanceTrackingService.Data
{
    public class PerformanceContext : DbContext
    {
        public PerformanceContext(DbContextOptions<PerformanceContext> options)
            : base(options)
        {

        }

        public DbSet<PlayerStat> PlayerStats { get; set; }

    }
}
