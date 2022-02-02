using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Smiosoft.PASS.Exceptions;

namespace Smiosoft.PASS.RabbitMQ.Publisher
{
	public abstract class QueueSubscriber<TMessage> : IQueueSubscriber<TMessage>
		where TMessage : class
	{
		protected IConnectionFactory Factory { get; }
		protected string QueueName { get; }
		protected string RoutingKey { get; }

		private readonly IConnection _connection;
		private readonly IModel _channel;

		protected QueueSubscriber(IConnectionFactory factory, string queueName, string routingKey)
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

			_connection = Factory.CreateConnection();
			_channel = _connection.CreateModel();
		}

		protected QueueSubscriber(string hostName, string queueName, string routingKey)
			: this(new ConnectionFactory() { HostName = hostName }, queueName, routingKey)
		{ }

		public abstract Task OnMessageRecievedAsync(TMessage message, CancellationToken cancellationToken);

		public virtual Task OnExceptionAsync(Exception exception)
		{
			return Task.CompletedTask;
		}

		public void Register()
		{

			_channel.QueueDeclare(
				queue: QueueName,
				durable: false,
				exclusive: false,
				autoDelete: false,
				arguments: null);

			var consumer = new EventingBasicConsumer(_channel);
			consumer.Received += async (model, message) =>
			{
				try
				{
					var deserialised = JsonConvert.DeserializeObject<TMessage>(Encoding.UTF8.GetString(message.Body.ToArray()))
					?? throw new DeserialisationException(typeof(TMessage));
					await OnMessageRecievedAsync(deserialised, CancellationToken.None);
				}
				catch (Exception exception)
				{
					await OnExceptionAsync(exception);
				}
			};

			_channel.BasicConsume(
				queue: QueueName,
				autoAck: true,
				consumer: consumer);
		}
	}
}
