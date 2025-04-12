using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PickEm.Api.DataAccess;
using PickEm.Api.Domain;
using PickEm.Api.Dto;
using PickEm.Api.Mappers;

namespace PickEm.Api.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly DataContext _context;

    public UsersController(ILogger<UsersController> logger, DataContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        _logger.LogInformation("Fetching all users");
        var users = await _context.Users.Where(u => !u.IsDeleted).ToListAsync();
        return Ok(users.MapToDto());
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetUser(long id)
    {
        _logger.LogInformation($"Fetching user with ID: {id}");
        if (id <= 0)
        {
            return BadRequest("Invalid user ID");
        }

        var user = await _context.Users
            .Include(u => u.OwnedLeagues)
            .Include(u => u.MemberLeagues)
            .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user.MapToDto());
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
    {
        _logger.LogInformation("Creating a new user");
        if (userDto == null)
        {
            return BadRequest("User data is required");
        }

        var user = new User
        {
            Username = userDto.Username,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Email = userDto.Email,
            PhoneNumber = userDto.PhoneNumber,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsDeleted = false
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user.MapToDto());
    }
}
