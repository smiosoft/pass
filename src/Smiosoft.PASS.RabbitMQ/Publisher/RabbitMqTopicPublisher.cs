using System;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Smiosoft.PASS.Extensions;
using Smiosoft.PASS.RabbitMQ.Configuration;

namespace Smiosoft.PASS.RabbitMQ.Publisher
{
	public abstract class RabbitMqTopicPublisher<TMessage> : RabbitMqPublisherBase<TMessage>
		where TMessage : class
	{
		protected RabbitMqTopicPublisherOptions Options { get; }

		protected RabbitMqTopicPublisher(RabbitMqTopicPublisherOptions topicPublisherOptions)
			: base(topicPublisherOptions)
		{
			Options = topicPublisherOptions ?? throw new ArgumentNullException(nameof(topicPublisherOptions));
		}

		public override async Task PublishAsync(TMessage message)
		{
			try
			{
				using var connection = Factory.CreateConnection();
				using var channel = connection.CreateModel();
				channel.ExchangeDeclare(exchange: Options.ExchangeName, type: ExchangeType.Topic);

				channel.BasicPublish(
					exchange: Options.ExchangeName,
					routingKey: Options.RoutingKey,
					basicProperties: null,
					body: message.Serialise());
			}
			catch (Exception exception)
			{
				await OnExceptionAsync(exception);
			}
		}
	}
}
