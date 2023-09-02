using MessageBrokerClient.Models.Exchanges.ExchangeTypes;
using RabbitMQ.Client;

namespace MessageBrokerClient.Clients
{
    public class RabbitMQClient : IClient
    {
        public ConnectionFactory Factory;
        public IConnection Connection;

        public RabbitMQClient(string hostName = "")
        {
            Factory = new ConnectionFactory { HostName = hostName };
            Connection = Factory.CreateConnection();
        }


        public void Dispose()
        {
            Connection.Dispose();
        }

        public string InterpretAmqpExchangeType(AmqpExchangeTypes exchangeType)
        {
            string result = ExchangeType.Fanout;

            switch (exchangeType)
            {
                case AmqpExchangeTypes.Topic:
                    result = ExchangeType.Topic;
                    break;

                case AmqpExchangeTypes.Fanout:
                    result = ExchangeType.Fanout;
                    break;

                case AmqpExchangeTypes.Direct:
                    result = ExchangeType.Direct;
                    break;

                case AmqpExchangeTypes.Headers:
                    result = ExchangeType.Headers;
                    break;

                default:
                    break;
            }

            return result;
        }
    }
}
