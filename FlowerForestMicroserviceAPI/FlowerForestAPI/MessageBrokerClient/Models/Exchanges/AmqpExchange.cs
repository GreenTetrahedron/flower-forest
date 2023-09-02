using MessageBrokerClient.Models.Exchanges.ExchangeTypes;

namespace MessageBrokerClient.Models.Exchanges
{
    public class AmqpExchange
    {
        public string ExchangeName { get; set; }

        public AmqpExchangeTypes ExchangeType { get; set; }
    }
}
