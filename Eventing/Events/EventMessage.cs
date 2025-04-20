using PickEm.EventProcessor.Events.Enums;

namespace PickEm.EventProcessor.Events;
public class EventMessage
{
    public EventType EventType { get; set; }
    public DateTime CreatedAt { get; set; }
}