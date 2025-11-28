using Microsoft.EntityFrameworkCore;
using PlayerManagementService.Model;

namespace PlayerManagementService.Data
{
    public class PlayerContext : DbContext
    {
        public PlayerContext(DbContextOptions<PlayerContext> options) : base(options) { }

        public DbSet<Player> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().HasData(
                new Player
                {
                    PlayerId = 1,
                    PlayerName = "Test Smith",
                    IsDrafted = false
                },
                new Player
                {
                    PlayerId = 2,
                    PlayerName = "John Test",
                    IsDrafted = false
                },
                new Player
                {
                    PlayerId = 3,
                    PlayerName = "Test Doe",
                    IsDrafted = false
                });
        }
    }
}
