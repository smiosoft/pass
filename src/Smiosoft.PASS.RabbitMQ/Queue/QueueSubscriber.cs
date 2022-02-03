using System;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Smiosoft.PASS.Extensions;

namespace Smiosoft.PASS.RabbitMQ.Queue
{
	public abstract class QueueSubscriber<TMessage> : IQueueSubscriber<TMessage>, IDisposable
		where TMessage : class
	{
		private bool _disposedValue;

		protected IConnectionFactory Factory { get; }
		protected IConnection Connection { get; }
		protected IModel Channel { get; }
		protected string QueueName { get; }

		protected QueueSubscriber(IConnectionFactory factory, string queueName)
		{
			if (string.IsNullOrWhiteSpace(queueName))
			{
				throw new ArgumentNullException(nameof(queueName));
			}

			Factory = factory ?? throw new ArgumentNullException(nameof(factory));
			Connection = Factory.CreateConnection();
			Channel = Connection.CreateModel();
			QueueName = queueName;
		}

		protected QueueSubscriber(string hostName, string queueName)
			: this(new ConnectionFactory() { HostName = hostName }, queueName)
		{ }

		public abstract Task OnMessageRecievedAsync(TMessage message, CancellationToken cancellationToken);

		public virtual Task OnExceptionAsync(Exception exception)
		{
			return Task.CompletedTask;
		}

		public virtual void Register()
		{
			Channel.QueueDeclare(queue: QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
			Channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

			var consumer = new EventingBasicConsumer(Channel);
			consumer.Received += async (sender, args) =>
			{
				try
				{
					await OnMessageRecievedAsync(args.Body.ToArray().Deserialise<TMessage>(), CancellationToken.None);
					Channel.BasicAck(deliveryTag: args.DeliveryTag, multiple: false);
				}
				catch (Exception exception)
				{
					await OnExceptionAsync(exception);
				}
			};

			Channel.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);
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
