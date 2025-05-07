namespace PickEm.Api.Domain;

public class LeagueGame
{
    public long Id { get; set; }
    public long GameId { get; set; }
    public long LeagueId { get; set; }
    public bool PicksClosed { get; set; }
    public virtual Game Game { get; set; }
    public virtual League League { get; set; }
    public virtual IEnumerable<Pick> Picks { get; set; }
}