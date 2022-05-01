using System;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Smiosoft.PASS.Extensions;
using Smiosoft.PASS.RabbitMQ.Configuration;

namespace Smiosoft.PASS.RabbitMQ.Publisher
{
	public abstract class RabbitMqQueuePublisher<TMessage> : RabbitMqPublisherBase<TMessage>
		where TMessage : class
	{
		protected RabbitMqQueuePublisherOptions Options { get; }

		protected RabbitMqQueuePublisher(RabbitMqQueuePublisherOptions queuePublisherOptions)
			: base(queuePublisherOptions)
		{
			Options = queuePublisherOptions ?? throw new ArgumentNullException(nameof(queuePublisherOptions));
		}

		protected RabbitMqQueuePublisher(string hostName, string queueName)
			: this(new RabbitMqQueuePublisherOptions(hostName, queueName))
		{ }

		public override async Task PublishAsync(TMessage message)
		{
			try
			{
				using var connection = Factory.CreateConnection();
				using var channel = connection.CreateModel();
				channel.QueueDeclare(queue: Options.QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

				var properties = channel.CreateBasicProperties();
				properties.Persistent = true;

				channel.BasicPublish(exchange: "", routingKey: Options.QueueName, basicProperties: properties, body: message.Serialise());
			}
			catch (Exception exception)
			{
				await OnExceptionAsync(exception);
			}
		}
	}
}
