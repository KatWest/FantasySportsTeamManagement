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

        // DTO for the simulate request
        public class SimulationRequest
        {
            public List<int> PlayerIds { get; set; } = new();
        }

        [HttpPost("simulate")]
        public async Task<ActionResult<IEnumerable<PlayerStat>>> Simulate([FromBody] SimulationRequest request)
        {
            if (request.PlayerIds == null || request.PlayerIds.Count == 0)
            {
                return BadRequest("At least one playerId is required.");
            }

            try
            {
                var newStats = new List<PlayerStat>();

                foreach (var playerId in request.PlayerIds)
                {
                    var stat = GenerateRandomFootballStats(playerId);
                    stat.FantasyPoints = CalculateFantasyPoints(stat);

                    _context.PlayerStats.Add(stat);
                    newStats.Add(stat);
                }

                await _context.SaveChangesAsync();
                return Ok(newStats);
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException?.Message ?? ex.Message;
                return StatusCode(500, $"Server error: {inner}");
            }

        }


        private PlayerStat GenerateRandomFootballStats(int playerId)
        {
            int passingYards = _random.Next(0, 401);
            int passingTouchdowns = _random.Next(0, 5);
            int interceptions = _random.Next(0, 3);

            int rushingYards = _random.Next(0, 151);
            int rushingTouchdowns = _random.Next(0, 3);

            int receptions = _random.Next(0, 11);
            int receivingYards = _random.Next(0, 151);
            int receivingTouchdowns = _random.Next(0, 3);

            int fumblesLost = _random.Next(0, 2);

            return new PlayerStat
            {
                PlayerId = playerId,
                GameDate = DateTime.UtcNow,

                PassingYards = passingYards,
                PassingTouchdowns = passingTouchdowns,
                InterceptionsThrown = interceptions,

                RushingYards = rushingYards,
                RushingTouchdowns = rushingTouchdowns,

                Receptions = receptions,
                ReceivingYards = receivingYards,
                ReceivingTouchdowns = receivingTouchdowns,

                FumblesLost = fumblesLost
            };
        }

        private int CalculateFantasyPoints(PlayerStat s)
        {
            double points = 0;

            points += s.PassingYards / 25.0;
            points += s.PassingTouchdowns * 4;
            points -= s.InterceptionsThrown * 2;

            points += s.RushingYards / 10.0;
            points += s.RushingTouchdowns * 6;

            points += s.ReceivingYards / 10.0;
            points += s.ReceivingTouchdowns * 6;
            points += s.Receptions * 1;
            points -= s.FumblesLost * 2;

            return (int)Math.Round(points);
        }
    }
}
