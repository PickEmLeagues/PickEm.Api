using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PickEm.Api.DataAccess;
using PickEm.Api.Domain;
using PickEm.Api.Dto;

namespace PickEm.Api.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class SportsController : ControllerBase
{
    private readonly ILogger<SportsController> _logger;
    private readonly DataContext _context;

    public SportsController(ILogger<SportsController> logger, DataContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetSports()
    {
        _logger.LogInformation("Fetching all sports");
        var sports = await _context.Sports.ToListAsync();
        return Ok(sports);
    }

    [HttpPost]
    public async Task<IActionResult> AddSport(SportInfoDto sportInfo)
    {
        _logger.LogInformation("Adding new sport: {Sport}", sportInfo);
        if (sportInfo == null || string.IsNullOrEmpty(sportInfo.Name) || string.IsNullOrEmpty(sportInfo.Season))
        {
            return BadRequest("Sport data cannot be null.");
        }

        var sport = new Sport
        {
            Name = sportInfo.Name,
            Season = sportInfo.Season,
            StartDate = sportInfo.StartDate,
            EndDate = sportInfo.EndDate,
            Week = sportInfo.Week ?? 0,
        };

        _context.Sports.Add(sport);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Sport added successfully." });
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSport(long id, SportInfoDto sportInfo)
    {
        _logger.LogInformation("Updating sport with ID: {Id}", id);
        if (sportInfo == null || string.IsNullOrEmpty(sportInfo.Name) || string.IsNullOrEmpty(sportInfo.Season))
        {
            return BadRequest("Sport data cannot be null.");
        }

        var sport = await _context.Sports.FindAsync(id);
        if (sport == null)
        {
            return NotFound("Sport not found.");
        }

        sport.Name = sportInfo.Name;
        sport.Season = sportInfo.Season;
        sport.StartDate = sportInfo.StartDate;
        sport.EndDate = sportInfo.EndDate;
        sport.Week = sportInfo.Week ?? 0;

        await _context.SaveChangesAsync();

        return Ok(new { Message = "Sport updated successfully." });
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSport(long id)
    {
        _logger.LogInformation("Deleting sport with ID: {Id}", id);
        var sport = await _context.Sports.FindAsync(id);
        if (sport == null)
        {
            return NotFound("Sport not found.");
        }

        _context.Sports.Remove(sport);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Sport deleted successfully." });
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSport(long id)
    {
        _logger.LogInformation("Fetching sport with ID: {Id}", id);
        var sport = await _context.Sports.FindAsync(id);
        if (sport == null)
        {
            return NotFound("Sport not found.");
        }

        return Ok(sport);
    }
}