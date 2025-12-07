using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamManagementService.Data;
using TeamManagementService.Model;

namespace TeamManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly TeamContext context;

        public TeamController(TeamContext context)
        {
            this.context = context;
        }

        // Lists all teams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> Get()
        {
            return await context.Teams.ToListAsync();
        }

        // Gets a single team by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> Get(int id)
        {
            var team = await context.Teams.FindAsync(id);

            if (team == null)
            {
                return NotFound();
            }
            return team;
        }

        // CREATE a new team
        [HttpPost]
        public async Task<ActionResult<Team>> Post(Team team)
        {
            context.Teams.Add(team);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = team.TeamId }, team);
        }

        // UPDATE an existing team
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Team team)
        {
            if (id != team.TeamId) return BadRequest();

            context.Entry(team).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.Teams.Any(t => t.TeamId == id)) return NotFound();
                throw;
            }

            return NoContent();
        }
    }
}
