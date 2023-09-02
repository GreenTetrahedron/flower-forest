using MessageBrokerClient.Clients;
using MessageBrokerClient.Models.Exchanges;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MessageBrokerClient.MessageReceiverServices
{
    public class RabbitMQMessageReceiverService : IMessageReceiverService
    {
        private RabbitMQClient client;
        private IModel channel;

        public RabbitMQMessageReceiverService()
        {
            client = new RabbitMQClient(hostName: "localhost");
            channel = client.Connection.CreateModel();
        }

        public void Dispose()
        {
            channel.Dispose();
            client.Dispose();
        }

        public void SubscribeToQueue(string routingKey, Action<byte[]> onReceiveDo, AmqpExchange exchange)
        {
            channel.ExchangeDeclare(
                    exchange: exchange.ExchangeName,
                    type: client.InterpretAmqpExchangeType(exchange.ExchangeType)
                );

            var queue = channel.QueueDeclare(
                    queue: string.Empty
                );

            channel.QueueBind(
                    exchange: exchange.ExchangeName,
                    queue: queue,
                    routingKey: routingKey,
                    arguments: null
                );

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (ch, ea) =>
            {
                var body = ea.Body.ToArray();

                onReceiveDo.Invoke(body);
            };

            channel.BasicConsume(
                    consumer: consumer,
                    queue: queue,
                    autoAck: true
                );
        }
    }
}
