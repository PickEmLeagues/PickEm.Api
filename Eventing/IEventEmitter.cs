using PickEm.EventProcessor.Events;

namespace PickEm.Api.Eventing;

public interface IEventEmitter
{
    Task ConnectAsync(string uri);
    Task EmitAsync(IEventMessage eventData);
}
