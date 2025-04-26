using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PickEm.Api.DataAccess;
using PickEm.Api.Domain;
using PickEm.Api.Domain.Enums;
using PickEm.Api.Dto;
using PickEm.Api.Eventing;
using PickEm.Api.Eventing.Events;
using PickEm.Api.Mappers;
using PickEm.Api.Services;
using PickEm.EventProcessor.Events.Enums;

namespace PickEm.Api.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class SchedulesController : ControllerBase
{
    private readonly ILogger<SchedulesController> _logger;
    private readonly IFileService _fileService;
    private readonly IEventEmitter _eventEmitter;
    private readonly DataContext _context;


    public SchedulesController(ILogger<SchedulesController> logger, IFileService fileService, IEventEmitter eventEmitter, DataContext dataContext)
    {
        _context = dataContext;
        _logger = logger;
        _fileService = fileService;
        _eventEmitter = eventEmitter;
    }

    [HttpGet]
    public async Task<IActionResult> GetSchedules(SportType? sportType = null)
    {
        _logger.LogInformation("Fetching schedules for sport type: {SportType}", sportType);

        if (sportType != null && !Enum.IsDefined(typeof(SportType), sportType))
        {
            return BadRequest("Invalid sport type.");
        }
        IEnumerable<Game> schedules;

        if (sportType == null)
        {
            schedules = await _context.Games.ToListAsync();
        }
        else
        {
            schedules = await _context.Games.Where(g => g.Sport == sportType).ToListAsync();
        }
        if (schedules == null || !schedules.Any())
        {
            return NotFound("No schedules found.");
        }
        return Ok(schedules.MapToDto());
    }

    [HttpPost("import")]
    public async Task<IActionResult> ImportSchedule([FromForm] IFormFile file)
    {
        _logger.LogInformation("Importing schedule file: {FileName}", file?.FileName);
        if (file == null || file.Length == 0)
        {
            return BadRequest("File cannot be null or empty.");
        }

        // Read the contents of the file and emit an event for each game
        using var reader = new StreamReader(file.OpenReadStream());
        var jsonString = await reader.ReadToEndAsync();
        var schedules = JsonSerializer.Deserialize<List<ScheduleDto>>(jsonString);
        _logger.LogInformation("Deserialized schedule data {data}", jsonString);

        if (schedules == null || schedules.Count == 0)
        {
            return BadRequest("Invalid schedule data.");
        }

        foreach (var schedule in schedules)
        {
            foreach (var game in schedule.ScheduledGames)
            {
                _logger.LogInformation("Processing game: {Game}", game);
                await _eventEmitter.EmitAsync(new ScheduleImportedEvent
                {
                    Sport = schedule.SportType,
                    Game = game,
                    CreatedAt = DateTime.UtcNow,
                    EventType = EventType.ScheduleImported
                });
            }
        }

        return Ok(new { Message = "Schedule imported successfully." });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSchedule(long id)
    {
        _logger.LogInformation("Deleting schedule with ID: {Id}", id);
        var game = await _context.Games.FindAsync(id);
        if (game == null)
        {
            return NotFound("Schedule not found.");
        }

        _context.Games.Remove(game);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Schedule deleted successfully." });
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSchedule(long id, [FromBody] GameDto gameDto)
    {
        _logger.LogInformation("Updating schedule with ID: {Id}", id);
        if (gameDto == null)
        {
            return BadRequest("Game data cannot be null.");
        }

        var game = await _context.Games.FindAsync(id);
        if (game == null)
        {
            return NotFound("Schedule not found.");
        }

        game.HomeScore = gameDto.HomeScore ?? game.HomeScore;
        game.AwayScore = gameDto.AwayScore ?? game.AwayScore;
        game.IsFinal = gameDto.IsFinal ?? game.IsFinal;
        game.OddsClosed = gameDto.OddsClosed ?? game.OddsClosed;
        game.HomeOdds = gameDto.HomeOdds ?? game.HomeOdds;
        game.AwayOdds = gameDto.AwayOdds ?? game.AwayOdds;
        game.DrawOdds = gameDto.DrawOdds ?? game.DrawOdds;
        game.StartTime = gameDto.StartTime ?? game.StartTime;
        game.Season = gameDto.Season ?? game.Season;
        game.Week = gameDto.Week ?? game.Week;
        game.UpdatedAt = DateTime.UtcNow;

        _context.Games.Update(game);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Schedule updated successfully." });
    }
}