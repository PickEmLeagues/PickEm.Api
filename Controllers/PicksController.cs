using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PickEm.Api.DataAccess;
using PickEm.Api.Domain;
using PickEm.Api.Dto;
using PickEm.Api.Mappers;

namespace PickEm.Api.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class PicksController : ControllerBase
{
    private readonly ILogger<PicksController> _logger;
    private readonly DataContext _context;

    public PicksController(ILogger<PicksController> logger, DataContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetPicks(long leagueId, long userId)
    {
        _logger.LogInformation($"Fetching picks for  game league ID: {leagueId} and user ID: {userId}");
        var picks = await _context.Leagues
            .Include(l => l.Schedule)
            .ThenInclude(s => s.Picks.Where(p => p.UserId == userId))
            .Where(l => l.Id == leagueId && l.Schedule.Any(s => s.Picks.Any(p => p.UserId == userId)))
            .ToListAsync();

        if (picks == null || !picks.Any())
        {
            return NotFound();
        }

        return Ok(picks.MapToDto());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPick(long id)
    {
        _logger.LogInformation($"Fetching pick with ID: {id}");
        if (id <= 0)
        {
            return BadRequest("Invalid pick ID");
        }

        var pick = await _context.Picks.FindAsync(id);

        if (pick == null)
        {
            return NotFound();
        }

        return Ok(pick.MapToDto());
    }

    [HttpPost]
    public async Task<IActionResult> CreatePick([FromBody] PickDto pickDto)
    {
        if (pickDto == null)
        {
            return BadRequest("Invalid pick data");
        }
        if (pickDto.GameLeagueId <= 0)
        {
            return BadRequest("Invalid game league ID");
        }
        _logger.LogInformation($"Creating pick for game league ID: {pickDto.GameLeagueId}");
        // Check if the game league exists
        var gameLeague = await _context.Schedules.AnyAsync(gl => gl.Id == pickDto.GameLeagueId);
        if (!gameLeague)
        {
            return NotFound("Game league not found");
        }

        var pick = new Pick
        {
            GameLeagueId = pickDto.GameLeagueId,
            UserId = pickDto.UserId,
            Wager = pickDto.Wager,
            TeamType = pickDto.TeamType,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Picks.Add(pick);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPick), new { pick.Id }, pick.MapToDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePick(long id, [FromBody] PickDto pickDto)
    {
        if (pickDto == null || id != pickDto.Id)
        {
            return BadRequest("Invalid pick data");
        }

        _logger.LogInformation($"Updating pick with ID: {id}");
        var existingPick = await _context.Picks.FindAsync(id);
        if (existingPick == null)
        {
            return NotFound();
        }

        existingPick.Wager = pickDto.Wager;
        existingPick.TeamType = pickDto.TeamType;
        existingPick.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return NoContent();
    }
}