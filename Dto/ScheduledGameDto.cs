using PickEm.Api.Domain.Enums;

namespace PickEm.Api.Dto;

public class ScheduledGameDto
{
    public string Season { get; set; } = string.Empty;
    public int Week { get; set; }
    public string HomeTeam { get; set; } = string.Empty;
    public string AwayTeam { get; set; } = string.Empty;
    public DateTimeOffset StartTime { get; set; }
    public decimal HomeOdds { get; set; }
    public decimal AwayOdds { get; set; }
    public decimal DrawOdds { get; set; }
}