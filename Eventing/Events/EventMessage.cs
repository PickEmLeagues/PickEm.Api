using PickEm.EventProcessor.Events.Enums;

namespace PickEm.EventProcessor.Events;
public interface IEventMessage
{
    public EventType EventType { get; set; }
    public DateTime CreatedAt { get; set; }
}