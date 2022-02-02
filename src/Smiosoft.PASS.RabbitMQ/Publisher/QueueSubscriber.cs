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
	public abstract class QueueSubscriber<TMessage> : IQueueSubscriber<TMessage>, IDisposable
		where TMessage : class
	{
		private bool _disposedValue;

		protected IConnectionFactory Factory { get; }
		protected IConnection Connection { get; }
		protected IModel Channel { get; }
		protected string QueueName { get; }
		protected string RoutingKey { get; }

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
			Connection = Factory.CreateConnection();
			Channel = Connection.CreateModel();
			QueueName = queueName;
			RoutingKey = routingKey;
		}

		protected QueueSubscriber(string hostName, string queueName, string routingKey)
			: this(new ConnectionFactory() { HostName = hostName }, queueName, routingKey)
		{ }

		public abstract Task OnMessageRecievedAsync(TMessage message, CancellationToken cancellationToken);

		public virtual Task OnExceptionAsync(Exception exception)
		{
			return Task.CompletedTask;
		}

		public virtual void Register()
		{
			Channel.QueueDeclare(
				queue: QueueName,
				durable: false,
				exclusive: false,
				autoDelete: false,
				arguments: null);

			var consumer = new EventingBasicConsumer(Channel);
			consumer.Received += async (model, args) =>
			{
				try
				{
					var deserialised = JsonConvert.DeserializeObject<TMessage>(Encoding.UTF8.GetString(args.Body.ToArray()))
						?? throw new DeserialisationException(typeof(TMessage));
					await OnMessageRecievedAsync(deserialised, CancellationToken.None);
				}
				catch (Exception exception)
				{
					await OnExceptionAsync(exception);
				}
			};

			Channel.BasicConsume(
				queue: QueueName,
				autoAck: true,
				consumer: consumer);
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (!_disposedValue)
			{
				if (disposing)
				{
					Connection.Dispose();
					Channel.Dispose();
				}

				_disposedValue = true;
			}
		}
	}
}
