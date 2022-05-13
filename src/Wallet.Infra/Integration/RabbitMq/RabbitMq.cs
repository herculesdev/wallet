using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using Wallet.Domain.Enumerations;
using Wallet.Domain.Interfaces.Integrations.Messaging;
using Wallet.Infra.Serialization.Converters;
using Wallet.Shared.Helpers.Extensions;

namespace Wallet.Infra.Integration.RabbitMq;

public class RabbitMq : IQueueHandler
{
    private readonly ConnectionFactory _factory;

    public RabbitMq()
    {
        _factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "admin",
            Password = "admin",
        };
    }

    public void Publish(object message, QueueExchange queueExchange, string routingKey = "")
    {
        using var connection = _factory.CreateConnection();
        using var channel = connection.CreateModel();
        var exchange = queueExchange.GetValueName();
        
        channel.ExchangeDeclare(exchange, ExchangeType.Fanout);
        
        channel.BasicPublish(
            exchange: exchange,
            routingKey: routingKey,
            basicProperties: null,
            body: Serialize(message));
    }

    private byte[] Serialize(object message)
    {
        var options = new JsonSerializerOptions();
        options.Converters.Add(new DocumentJsonConverter());
        options.Converters.Add(new PasswordJsonConverter());
        
        return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message, options));
    }
}
