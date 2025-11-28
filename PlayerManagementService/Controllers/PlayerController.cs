using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlayerManagementService.Data;
using PlayerManagementService.Model;

namespace PlayerManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly PlayerContext context;

        public PlayerController(PlayerContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> Get()
        {
            return await context.Players.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> Get(int id)
        {
            var player = await context.Players.FindAsync(id);
            if (player == null)
                return NotFound();
            return player;
        }
        [HttpPost("draft")]
        public async Task<IActionResult> Draft(int id)
        {
            var player = await context.Players.FindAsync(id);
            if (player == null)
                return NotFound();
            if (player.IsDrafted)
                return BadRequest($"Player {player.PlayerName} is already drafted");
            player.IsDrafted = true;
            await context.SaveChangesAsync();
            return Ok(player);
        }
        [HttpPost("release")]
        public async Task<IActionResult> Release(int id)
        {
            var player = await context.Players.FindAsync(id);
            if (player == null)
                return NotFound();
            if (!player.IsDrafted)
                return BadRequest($"Player {player.PlayerName} is not drafted");
            player.IsDrafted = false;
            await context.SaveChangesAsync();
            return Ok(player);
        }
    }
}
