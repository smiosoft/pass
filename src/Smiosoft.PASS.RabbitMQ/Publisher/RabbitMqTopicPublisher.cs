using System;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Smiosoft.PASS.Extensions;

namespace Smiosoft.PASS.RabbitMQ.Publisher
{
	public abstract class RabbitMqTopicPublisher<TMessage> : RabbitMqPublisherBase<TMessage>
		where TMessage : class
	{
		protected string ExchangeName { get; }
		protected string RoutingKey { get; }

		protected RabbitMqTopicPublisher(string hostName, string exchangeName, string routingKey)
			: base(hostName)
		{
			if (string.IsNullOrWhiteSpace(exchangeName))
			{
				throw new ArgumentNullException(nameof(exchangeName));
			}

			if (string.IsNullOrWhiteSpace(routingKey))
			{
				throw new ArgumentNullException(nameof(routingKey));
			}

			ExchangeName = exchangeName;
			RoutingKey = routingKey;
		}

		public override async Task PublishAsync(TMessage message)
		{
			try
			{
				using var connection = Factory.CreateConnection();
				using var channel = connection.CreateModel();
				channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Topic);

				channel.BasicPublish(exchange: ExchangeName, routingKey: RoutingKey, basicProperties: null, body: message.Serialise());
			}
			catch (Exception exception)
			{
				await OnExceptionAsync(exception);
			}
		}
	}
}
