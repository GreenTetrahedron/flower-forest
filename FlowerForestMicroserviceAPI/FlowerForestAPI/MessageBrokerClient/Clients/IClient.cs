using MessageBrokerClient.Models.Exchanges.ExchangeTypes;

namespace MessageBrokerClient.Clients
{
    public interface IClient : IDisposable
    {
        string InterpretAmqpExchangeType(AmqpExchangeTypes exchangeType);
    }
}
