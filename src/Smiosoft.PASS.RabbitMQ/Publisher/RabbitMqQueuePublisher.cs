using System;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Smiosoft.PASS.Extensions;

namespace Smiosoft.PASS.RabbitMQ.Publisher
{
	public abstract class RabbitMqQueuePublisher<TMessage> : RabbitMqPublisherBase<TMessage>
		where TMessage : class
	{
		protected string QueueName { get; }

		protected RabbitMqQueuePublisher(string hostName, string queueName)
			: base(hostName)
		{
			if (string.IsNullOrWhiteSpace(queueName))
			{
				throw new ArgumentNullException(nameof(queueName));
			}

			QueueName = queueName;
		}

		public override async Task PublishAsync(TMessage message)
		{
			try
			{
				using var connection = Factory.CreateConnection();
				using var channel = connection.CreateModel();
				channel.QueueDeclare(queue: QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

				var properties = channel.CreateBasicProperties();
				properties.Persistent = true;

				channel.BasicPublish(exchange: "", routingKey: QueueName, basicProperties: properties, body: message.Serialise());
			}
			catch (Exception exception)
			{
				await OnExceptionAsync(exception);
			}
		}
	}
}
