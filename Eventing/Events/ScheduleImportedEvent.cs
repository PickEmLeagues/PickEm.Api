using PickEm.EventProcessor.Events;

namespace PickEm.Api.Eventing.Events;

public class ScheduleImportedEvent : EventMessage
{
    public string FilePath { get; set; } = string.Empty;
}