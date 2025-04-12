namespace PickEm.Api.Dto;

public class UserDto
{
    public long? Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool? IsDeleted { get; set; }

    public IEnumerable<LeagueDto>? OwnedLeagues { get; set; }
    public IEnumerable<LeagueDto>? MemberLeagues { get; set; }
}