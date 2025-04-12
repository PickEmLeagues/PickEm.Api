using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PickEm.Api.DataAccess;
using PickEm.Api.Domain;
using PickEm.Api.Dto;
using PickEm.Api.Mappers;

namespace PickEm.Api.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class LeaguesController : ControllerBase
{
    private readonly ILogger<LeaguesController> _logger;
    private readonly DataContext _context;

    public LeaguesController(ILogger<LeaguesController> logger, DataContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    [Route("v1/leagues")]
    public async Task<IActionResult> GetLeagues()
    {
        _logger.LogInformation("Fetching all leagues");
        var leagues = await _context.Leagues.Where(l => l.IsPublic && !l.IsDeleted).ToListAsync();
        return Ok(leagues.MapToDto());
    }
    [HttpGet]
    [Route("v1/leagues/{id}")]
    public async Task<IActionResult> GetLeague(long id)
    {
        _logger.LogInformation($"Fetching league with ID: {id}");
        if (id <= 0)
        {
            return BadRequest("Invalid league ID");
        }
        // Fetch the league with its members and schedule
        var league = await _context.Leagues
            .Include(l => l.Members)
            .Include(l => l.Schedule)
            .ThenInclude(s => s.Game)
            .FirstOrDefaultAsync(l => l.Id == id && !l.IsDeleted);

        if (league == null)
        {
            return NotFound();
        }

        return Ok(league.MapToDto());
    }
    [HttpGet]
    [Route("v1/leagues/{id}/game/{gameId}")]
    public async Task<IActionResult> GetLeagueGame(long id, long gameId)
    {
        _logger.LogInformation($"Fetching game with ID: {gameId} for league with ID: {id}");
        var game = await _context.Schedules
            .Include(s => s.Game)
            .Include(s => s.Picks)
            .FirstOrDefaultAsync(s => s.LeagueId == id && s.GameId == gameId);

        if (game == null)
        {
            return NotFound();
        }

        return Ok(game.MapToDto());
    }

    [HttpPost]
    [Route("v1/leagues")]
    public async Task<IActionResult> CreateLeague([FromBody] LeagueDto league)
    {
        _logger.LogInformation("Creating a new league");
        if (league == null)
        {
            return BadRequest("League cannot be null");
        }

        var newLeague = new League
        {
            Name = league.Name,
            IsPublic = league.IsPublic,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Sport = league.Sport,
            OwnerId = league.OwnerId,
            IsDeleted = false,
        };

        await _context.Leagues.AddAsync(newLeague);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetLeague), new { id = newLeague.Id }, newLeague.MapToDto());
    }

    [HttpPut]
    [Route("v1/leagues/{id}")]
    public async Task<IActionResult> UpdateLeague(long id, [FromBody] LeagueDto league)
    {
        _logger.LogInformation($"Updating league with ID: {id}");
        if (league == null || id != league.Id)
        {
            return BadRequest("League cannot be null and ID must match");
        }

        var existingLeague = await _context.Leagues.FindAsync(id);
        if (existingLeague == null)
        {
            return NotFound();
        }

        existingLeague.Name = league.Name;
        existingLeague.IsPublic = league.IsPublic;
        existingLeague.UpdatedAt = DateTime.UtcNow;

        _context.Leagues.Update(existingLeague);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete]
    [Route("v1/leagues/{id}")]
    public async Task<IActionResult> DeleteLeague(long id)
    {
        _logger.LogInformation($"Deleting league with ID: {id}");
        if (id <= 0)
        {
            return BadRequest("Invalid league ID");
        }
        var league = await _context.Leagues.FindAsync(id);
        if (league == null)
        {
            return NotFound();
        }

        league.IsDeleted = true;
        _context.Leagues.Update(league);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}