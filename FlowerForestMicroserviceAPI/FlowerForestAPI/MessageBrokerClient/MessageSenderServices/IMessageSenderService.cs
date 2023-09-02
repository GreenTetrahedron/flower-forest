using MessageBrokerClient.Models.Exchanges;

namespace MessageBrokerClient.MessageSenderServices
{
    public interface IMessageSenderService : IDisposable
    {
        void SendData(object data, string routingKey, AmqpExchange exchange);
    }
}
