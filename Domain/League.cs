using PickEm.Api.Domain.Enums;

namespace PickEm.Api.Domain;

public class League
{
    public long Id { get; set; }
    public long OwnerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsPublic { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public SportType Sport { get; set; }

    public virtual User Owner { get; set; }
    public virtual IEnumerable<User> Members { get; set; }
    public virtual IEnumerable<GameLeague> Schedule { get; set; }
}