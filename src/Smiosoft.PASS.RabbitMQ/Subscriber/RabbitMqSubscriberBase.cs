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

		protected IConnectionFactory Factory { get; }
		protected IConnection Connection { get; }
		protected IModel Channel { get; }

		protected RabbitMqSubscriberBase(IConnectionFactory factory)
		{
			Factory = factory ?? throw new ArgumentNullException(nameof(factory));
			Connection = Factory.CreateConnection();
			Channel = Connection.CreateModel();
		}

		protected RabbitMqSubscriberBase(string hostName)
		{
			if (string.IsNullOrWhiteSpace(hostName))
			{
				throw new ArgumentNullException(nameof(hostName));
			}

			Factory = new ConnectionFactory() { HostName = hostName };
			Connection = Factory.CreateConnection();
			Channel = Connection.CreateModel();
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
	}
}
