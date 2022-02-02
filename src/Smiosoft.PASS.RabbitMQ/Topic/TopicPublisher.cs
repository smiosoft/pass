using System;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Smiosoft.PASS.Extensions;

namespace Smiosoft.PASS.RabbitMQ.Topic
{
	public abstract class TopicPublisher<TMessage> : ITopicPublisher<TMessage>
		where TMessage : class
	{
		protected IConnectionFactory Factory { get; }
		protected string ExchangeName { get; }
		protected string RoutingKey { get; }

		protected TopicPublisher(IConnectionFactory factory, string exchangeName, string routingKey)
		{
			if (string.IsNullOrWhiteSpace(exchangeName))
			{
				throw new ArgumentNullException(nameof(exchangeName));
			}

			if (string.IsNullOrWhiteSpace(routingKey))
			{
				throw new ArgumentNullException(nameof(routingKey));
			}

			Factory = factory ?? throw new ArgumentNullException(nameof(factory));
			ExchangeName = exchangeName;
			RoutingKey = routingKey;
		}

		protected TopicPublisher(string hostName, string exchangeName, string routingKey)
			: this(new ConnectionFactory() { HostName = hostName }, exchangeName, routingKey)
		{ }

		public Task PublishAsync(TMessage message)
		{
			return Task.Run(() =>
			{
				using var connection = Factory.CreateConnection();
				using var channel = connection.CreateModel();
				channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Topic);

				channel.BasicPublish(exchange: ExchangeName, routingKey: RoutingKey, basicProperties: null, body: message.Serialise());
			});
		}
	}
}
