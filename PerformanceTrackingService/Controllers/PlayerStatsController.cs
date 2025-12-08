using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerformanceTrackingService.Data;
using PerformanceTrackingService.Model;

namespace PerformanceTrackingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerStatsController : ControllerBase
    {
        private readonly PerformanceContext _context;
        private readonly Random _random = new();

        public PlayerStatsController(PerformanceContext context)
        {
            _context = context;
        }

        // GET: api/PlayerStats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerStat>>> GetAll()
        {
            return await _context.PlayerStats.ToListAsync();
        }

        // GET Player Stats by Player ID
        [HttpGet("player/{playerId}")]
        public async Task<ActionResult<IEnumerable<PlayerStat>>> GetByPlayer(int playerId)
        {
            var stats = await _context.PlayerStats
                .Where(s => s.PlayerId == playerId)
                .OrderByDescending(s => s.GameDate)
                .ToListAsync();

            if (!stats.Any())
            { 
                return NotFound();
            }

            else 
            {
                return stats; 
            }
            
        }

        public class SimulationRequest
        {
            public List<int> PlayerIds { get; set; } = new();
        }

        // POST: api/PlayerStats/simulate
        [HttpPost("simulate")]
        public async Task<ActionResult<IEnumerable<PlayerStat>>> Simulate([FromBody] SimulationRequest request)
        {
            if (request.PlayerIds == null || request.PlayerIds.Count == 0)
            {
                return BadRequest("At least one playerId is required.");
            }

            var newStats = new List<PlayerStat>();

            foreach (var playerId in request.PlayerIds)
            {
                var stat = GenerateRandomFootballStats(playerId);
                stat.FantasyPoints = CalculateFantasyPoints(stat);

                _context.PlayerStats.Add(stat);
                newStats.Add(stat);
            }

            await _context.SaveChangesAsync();

            // TODO: Notify Leaderboard service when it exists.

            return Ok(newStats);
        }

       
        // Made a method to generate random football stats
        private PlayerStat GenerateRandomFootballStats(int playerId)
        {
            // random stats for football to be generated for each player
            int passingYards = _random.Next(0, 401); 
            int passingTd = _random.Next(0, 5);
            int interceptions = _random.Next(0, 3);
            int rushingYards = _random.Next(0, 151);
            int rushingTd = _random.Next(0, 3);
            int receptions = _random.Next(0, 11);
            int receivingYds = _random.Next(0, 151);
            int receivingTd = _random.Next(0, 3);
            int fumblesLost = _random.Next(0, 2);

            return new PlayerStat
            {
                PlayerId = playerId,
                GameDate = DateTime.UtcNow,
                PassingYards = passingYards,
                PassingTouchdowns = passingTd,
                InterceptionsThrown = interceptions,
                RushingYards = rushingYards,
                RushingTouchdowns = rushingTd,
                Receptions = receptions,
                ReceivingYards = receivingYds,
                ReceivingTouchdowns = receivingTd,
                FumblesLost = fumblesLost
            };
        }

        // How the scoring will be done in our fantasy program.
        // 1 pt / 25 passing yds, 4 per pass TD, -2 per INT
        // 1 pt / 10 rush/rec yds, 6 per rush/rec TD, 1 per reception, -2 per fumble lost
        // Since I don't play fantasy football, I did get some help from the internet to help with scoring.
        private int CalculateFantasyPoints(PlayerStat s)
        {
            // Starting points with 0.
            var points = 0.0;

            //Calculating the points for each player.
            points += s.PassingYards / 25.0;
            points += s.PassingTouchdowns * 4;
            points -= s.InterceptionsThrown * 2;
            points += s.RushingYards / 10.0;
            points += s.RushingTouchdowns * 6;
            points += s.ReceivingYards / 10.0;
            points += s.ReceivingTouchdowns * 6;
            points += s.Receptions * 1;    // PPR
            points -= s.FumblesLost * 2;

            // returning the points for each player.
            return (int)Math.Round(points);
        }
    }
}
