using MessageBrokerClient.Models.Exchanges;

namespace MessageBrokerClient.MessageReceiverServices
{
    public interface IMessageReceiverService : IDisposable
    {
        void SubscribeToQueue(string routingKey, Action<byte[]> onReceiveDo, AmqpExchange exchange);
    }
}
