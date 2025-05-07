namespace PickEm.Api.Domain;
public class Sport
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Season { get; set; } = string.Empty;
    public int Week { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public virtual IEnumerable<Game> Games { get; set; }
}
