using PickEm.Api.Domain.Enums;

namespace PickEm.Api.Domain;

public class Game
{
    public long Id { get; set; }
    public long SportId { get; set; }
    public int Week { get; set; }
    public DateTime StartTime { get; set; }
    public string HomeTeam { get; set; } = string.Empty;
    public string AwayTeam { get; set; } = string.Empty;
    public int? HomeScore { get; set; }
    public int? AwayScore { get; set; }
    public bool IsFinal { get; set; }
    public bool OddsClosed { get; set; }
    public decimal HomeOdds { get; set; }
    public decimal AwayOdds { get; set; }
    public decimal DrawOdds { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public virtual Sport Sport { get; set; }
}