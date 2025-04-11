namespace PickEm.Api.Dto;

public class GameLeagueDto
{
    public long Id { get; set; }
    public bool PicksClosed { get; set; }
    public GameDto Game { get; set; } = null!;
    public IEnumerable<PickDto> Picks { get; set; }
}