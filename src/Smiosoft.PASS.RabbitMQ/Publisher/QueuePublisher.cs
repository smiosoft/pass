using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Smiosoft.PASS.RabbitMQ.Publisher
{
	public abstract class QueuePublisher<TMessage> : IQueuePublisher<TMessage>
		where TMessage : class
	{
		protected ConnectionFactory Factory { get; }
		protected string QueueName { get; }
		protected string RoutingKey { get; }

		protected QueuePublisher(ConnectionFactory factory, string queueName, string routingKey)
		{
			if (string.IsNullOrWhiteSpace(queueName))
			{
				throw new ArgumentNullException(nameof(queueName));
			}

			if (string.IsNullOrWhiteSpace(routingKey))
			{
				throw new ArgumentNullException(nameof(routingKey));
			}

			Factory = factory ?? throw new ArgumentNullException(nameof(factory));
			QueueName = queueName;
			RoutingKey = routingKey;
		}

		protected QueuePublisher(string hostName, string queueName, string routingKey)
			: this(new ConnectionFactory() { HostName = hostName }, queueName, routingKey)
		{ }

		public Task PublishAsync(TMessage message)
		{
			return Task.Run(() =>
			{
				using var connection = Factory.CreateConnection();
				using var channel = connection.CreateModel();
				channel.QueueDeclare(queue: QueueName,
									 durable: false,
									 exclusive: false,
									 autoDelete: false,
									 arguments: null);
				channel.BasicPublish(exchange: "",
									 routingKey: RoutingKey,
									 basicProperties: null,
									 body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)));
			});
		}
	}
}
