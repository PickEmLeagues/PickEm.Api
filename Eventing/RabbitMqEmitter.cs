using System.Text;
using System.Text.Json;
using PickEm.EventProcessor.Events;
using RabbitMQ.Client;

namespace PickEm.Api.Eventing;

public class RabbitMqEmitter : IEventEmitter
{
    private IConnection? _connection;
    private IChannel? _channel;
    private readonly string _queueName;

    public RabbitMqEmitter(string queueName)
    {
        _queueName = queueName;
    }

    public async Task ConnectAsync(string uri)
    {
        try
        {
            var factory = new ConnectionFactory() { Uri = new Uri(uri) };
            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();
            await _channel.QueueDeclareAsync(queue: _queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }
        catch
        {
            return;
        }
    }

    public async Task EmitAsync(IEventMessage eventData)
    {
        if (_channel == null)
        {
            return;
        }

        var messageBody = JsonSerializer.Serialize(eventData);
        var body = Encoding.UTF8.GetBytes(messageBody);

        var properties = new BasicProperties
        {
            Persistent = false,
        };

        await _channel.BasicPublishAsync(exchange: string.Empty,
            routingKey: _queueName,
            mandatory: true,
            basicProperties: properties,
            body: body);
    }
}