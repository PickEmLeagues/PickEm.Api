using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PickEm.Api.Eventing;
using PickEm.Api.Eventing.Events;
using PickEm.Api.Services;
using PickEm.EventProcessor.Events.Enums;

namespace PickEm.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SchedulesController : ControllerBase
{
    private readonly ILogger<SchedulesController> _logger;
    private readonly IFileService _fileService;
    private readonly IEventEmitter _eventEmitter;


    public SchedulesController(ILogger<SchedulesController> logger, IFileService fileService, IEventEmitter eventEmitter)
    {
        _logger = logger;
        _fileService = fileService;
        _eventEmitter = eventEmitter;
    }

    [HttpPost]
    public async Task<IActionResult> ImportSchedule([FromForm] IFormFile file)
    {
        _logger.LogInformation("Importing schedule file: {FileName}", file?.FileName);
        if (file == null || file.Length == 0)
        {
            return BadRequest("File cannot be null or empty.");
        }

        var fileName = await _fileService.UploadFileAsync(file);
        if (string.IsNullOrEmpty(fileName))
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to upload file.");
        }

        await _eventEmitter.EmitAsync(new ScheduleImportedEvent
        {
            FilePath = fileName,
            CreatedAt = DateTime.UtcNow,
            EventType = EventType.ScheduleImported
        });

        _logger.LogInformation("Schedule file imported successfully: {FileName}", fileName);
        return Ok(new { Message = "Schedule imported successfully." });
    }
}