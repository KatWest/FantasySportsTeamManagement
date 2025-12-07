using TeamManagementService.Model;
using Microsoft.EntityFrameworkCore;

namespace TeamManagementService.Data
{
    public class TeamContext : DbContext
    {
        // constructor
        public TeamContext(DbContextOptions<TeamContext> options) : base(options) 
        {
            // empty
        }

        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>().Property(t => t.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");                        // SQL Server default for UTC now

            modelBuilder.Entity <Team>().HasData(
                new Team
                {
                    TeamId = 1,
                    TeamName = "Blinders",
                    CreatedAt = DateTime.UtcNow
                },
                new Team
                {
                    TeamId = 2,
                    TeamName = "Sockers",
                    CreatedAt = DateTime.UtcNow
                },
                new Team
                {
                    TeamId = 3,
                    TeamName = "Lighters",
                    CreatedAt = DateTime.UtcNow
                },
                new Team
                {
                    TeamId = 4,
                    TeamName = "Rainers",
                    CreatedAt = DateTime.UtcNow
                },
                new Team
                {
                    TeamId = 5,
                    TeamName = "Timers",
                    CreatedAt = DateTime.UtcNow
                }


                );
        }

    }
}
