using PickEm.Api.Domain.Enums;
using PickEm.Api.Dto;
using PickEm.EventProcessor.Events;

namespace PickEm.Api.Eventing.Events;

public class ScheduleImportedEvent : EventMessage
{
    public SportType Sport { get; set; }
    public ScheduledGameDto Game { get; set; } = new();
}