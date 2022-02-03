using System;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Smiosoft.PASS.Extensions;

namespace Smiosoft.PASS.RabbitMQ.Topic
{
	public abstract class TopicSubscriber<TMessage> : ITopicSubscriber<TMessage>, IDisposable
		where TMessage : class
	{
		private bool _disposedValue;

		protected IConnectionFactory Factory { get; }
		protected IConnection Connection { get; }
		protected IModel Channel { get; }
		protected string ExchangeName { get; }
		protected string RoutingKey { get; }

		protected TopicSubscriber(IConnectionFactory factory, string exchangeName, string routingKey)
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
			Connection = Factory.CreateConnection();
			Channel = Connection.CreateModel();
			ExchangeName = exchangeName;
			RoutingKey = routingKey;
		}

		protected TopicSubscriber(string hostName, string exchangeName, string routingKey)
			: this(new ConnectionFactory() { HostName = hostName }, exchangeName, routingKey)
		{ }

		public abstract Task OnMessageRecievedAsync(TMessage message, CancellationToken cancellationToken);

		public virtual Task OnExceptionAsync(Exception exception)
		{
			return Task.CompletedTask;
		}

		public virtual void Register()
		{
			Channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Topic);

			var queueName = Channel.QueueDeclare().QueueName;
			Channel.QueueBind(queue: queueName, exchange: ExchangeName, routingKey: RoutingKey);

			var consumer = new EventingBasicConsumer(Channel);
			consumer.Received += async (sender, args) =>
			{
				try
				{
					await OnMessageRecievedAsync(args.Body.ToArray().Deserialise<TMessage>(), CancellationToken.None);
				}
				catch (Exception exception)
				{
					await OnExceptionAsync(exception);
				}
			};

			Channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
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
