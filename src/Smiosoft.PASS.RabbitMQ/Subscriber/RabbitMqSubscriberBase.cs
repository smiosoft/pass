using System;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Smiosoft.PASS.RabbitMQ.Subscriber
{
	public abstract class RabbitMqSubscriberBase<TMessage> : IRabbitMqSubscriber<TMessage>, IDisposable
		where TMessage : class
	{
		private bool _disposedValue;
		private IConnectionFactory? _factory;
		private IConnection? _connection;
		private IModel? _channel;

		protected string HostName { get; }
		protected IConnectionFactory Factory { get => _factory ??= CreateConnectionFactory(); }
		protected IConnection Connection { get => _connection ??= CreateConnection(); }
		protected IModel Channel { get => _channel ??= CreateChannel(); }

		protected RabbitMqSubscriberBase(string hostName)
		{
			if (string.IsNullOrWhiteSpace(hostName))
			{
				throw new ArgumentNullException(nameof(hostName));
			}

			HostName = hostName;
		}

		public abstract Task OnExceptionAsync(Exception exception);

		public abstract Task OnMessageRecievedAsync(TMessage message, CancellationToken cancellationToken);

		public abstract Task RegisterAsync();

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposedValue)
			{
				if (disposing)
				{
					if (Connection != null)
					{
						Connection.Dispose();
					}
					if (Channel != null)
					{
						Channel.Dispose();
					}
				}

				_disposedValue = true;
			}
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		protected virtual IConnectionFactory CreateConnectionFactory()
		{
			return new ConnectionFactory() { HostName = HostName };
		}

		protected virtual IConnection CreateConnection()
		{
			return Factory.CreateConnection();
		}

		protected virtual IModel CreateChannel()
		{
			return Connection.CreateModel();
		}
	}
}
