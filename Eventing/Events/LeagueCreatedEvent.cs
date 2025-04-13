using PickEm.EventProcessor.Events.Enums;

namespace PickEm.EventProcessor.Events;

public class LeagueCreatedEvent : IEventMessage
{
    public long LeagueId { get; set; }
    public DateTime CreatedAt { get; set; }
    public EventType EventType { get; set; } = EventType.LeagueCreated;
}