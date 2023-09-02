namespace MessageBrokerClient.Models.Exchanges
{
    public static class MessageBrokerExchanges
    {
        public static Dictionary<MessageBrokerExchangeNames, AmqpExchange> Exchanges = new Dictionary<MessageBrokerExchangeNames, AmqpExchange>
        {
            {
                MessageBrokerExchangeNames.User,
                new AmqpExchange { ExchangeName = "user", ExchangeType = ExchangeTypes.AmqpExchangeTypes.Topic }
            },
            {
                MessageBrokerExchangeNames.Catalogue,
                new AmqpExchange { ExchangeName = "catalogue", ExchangeType = ExchangeTypes.AmqpExchangeTypes.Topic }
            }
        };
    }
}
