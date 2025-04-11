using PickEm.Api.Domain.Enums;

namespace PickEm.Api.Dto;

public class PickDto
{
    public long? Id { get; set; }
    public long UserId { get; set; }
    public long GameLeagueId { get; set; }
    public decimal Wager { get; set; }
    public TeamType TeamType { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
}