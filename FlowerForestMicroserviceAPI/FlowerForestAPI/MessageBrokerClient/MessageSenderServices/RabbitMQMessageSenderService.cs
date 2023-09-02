using MessageBrokerClient.Clients;
using MessageBrokerClient.Models.Exchanges;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace MessageBrokerClient.MessageSenderServices
{
    public class RabbitMQMessageSenderService : IMessageSenderService
    {
        private RabbitMQClient client;
        private IModel channel;

        public RabbitMQMessageSenderService()
        {
            client = new RabbitMQClient(hostName: "localhost");
            channel = client.Connection.CreateModel();
        }

        public void Dispose()
        {
            channel.Dispose();
            client.Dispose();
        }

        public void SendData(object data, string routingKey, AmqpExchange exchange)
        {
            channel.ExchangeDeclare(
                    exchange.ExchangeName,
                    client.InterpretAmqpExchangeType(exchange.ExchangeType)
                );

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));

            channel.BasicPublish(
                    exchange: exchange.ExchangeName,
                    routingKey: routingKey,
                    basicProperties: null,
                    body: body
                );
        }
    }
}
