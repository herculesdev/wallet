using Wallet.Domain.Enumerations;

namespace Wallet.Domain.Interfaces.Integrations.Messaging;

public interface IQueueHandler
{
    void Publish(object message, QueueExchange queueExchange, string routingKey = "");
}