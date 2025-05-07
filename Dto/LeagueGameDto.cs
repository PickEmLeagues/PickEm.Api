namespace PickEm.Api.Dto;

public class LeagueGameDto
{
    public long Id { get; set; }
    public bool PicksClosed { get; set; }
    public GameDto Game { get; set; } = null!;
    public IEnumerable<PickDto> Picks { get; set; }
}