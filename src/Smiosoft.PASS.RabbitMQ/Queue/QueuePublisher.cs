using System;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Smiosoft.PASS.Extensions;

namespace Smiosoft.PASS.RabbitMQ.Queue
{
	public abstract class QueuePublisher<TMessage> : IQueuePublisher<TMessage>
		where TMessage : class
	{
		protected IConnectionFactory Factory { get; }
		protected string QueueName { get; }

		protected QueuePublisher(IConnectionFactory factory, string queueName)
		{
			if (string.IsNullOrWhiteSpace(queueName))
			{
				throw new ArgumentNullException(nameof(queueName));
			}

			Factory = factory ?? throw new ArgumentNullException(nameof(factory));
			QueueName = queueName;
		}

		protected QueuePublisher(string hostName, string queueName)
			: this(new ConnectionFactory() { HostName = hostName }, queueName)
		{ }

		public Task PublishAsync(TMessage message)
		{
			return Task.Run(() =>
			{
				using var connection = Factory.CreateConnection();
				using var channel = connection.CreateModel();
				channel.QueueDeclare(queue: QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

				var properties = channel.CreateBasicProperties();
				properties.Persistent = true;

				channel.BasicPublish(exchange: "", routingKey: QueueName, basicProperties: properties, body: message.Serialise());
			});
		}
	}
}
