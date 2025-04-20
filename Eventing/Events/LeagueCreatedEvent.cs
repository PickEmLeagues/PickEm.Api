using PickEm.EventProcessor.Events.Enums;

namespace PickEm.EventProcessor.Events;

public class LeagueCreatedEvent : EventMessage
{
    public long LeagueId { get; set; }
}