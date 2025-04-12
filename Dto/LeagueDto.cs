using PickEm.Api.Domain.Enums;

namespace PickEm.Api.Dto;

public class LeagueDto
{
    public long? Id { get; set; }
    public long OwnerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool? IsPublic { get; set; } = false;
    public bool? IsDeleted { get; set; } = false;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public SportType Sport { get; set; }
    public IEnumerable<GameLeagueDto>? Schedule { get; set; }
}