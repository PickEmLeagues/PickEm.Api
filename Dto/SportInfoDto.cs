namespace PickEm.Api.Dto;

public class SportInfoDto
{
    public long? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Season { get; set; } = string.Empty;
    public int? Week { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}