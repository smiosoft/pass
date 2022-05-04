using System;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Smiosoft.PASS.Payload;
using Smiosoft.PASS.Subscriber;

namespace Smiosoft.PASS.RabbitMQ.Subscriber
{
	public abstract class SubscriberBase<TPayload> : ISubscriptionHandler<TPayload>
		where TPayload : IPayload
	{
		private readonly SubscriberOptions _options;
		private IConnectionFactory? _factory;
		private IConnection? _connection;
		private IModel? _channel;

		protected IConnectionFactory Factory { get => _factory ??= CreateConnectionFactory(); }
		protected IConnection Connection { get => _connection ??= CreateConnection(); }
		protected IModel Channel { get => _channel ??= CreateChannel(); }

		protected SubscriberBase(SubscriberOptions options)
		{
			_options = options ?? throw new ArgumentNullException(nameof(options));
		}

		public async Task RegisterAsync()
		{
			try
			{
				await OnRegistration();
			}
			catch (Exception exception)
			{
				await OnExceptionAsync(exception);
			}
		}

		public abstract Task OnExceptionAsync(Exception exception);

		public abstract Task OnRecivedAsync(TPayload payload, CancellationToken cancellationToken = default);

		public abstract Task OnRegistration();

		protected virtual IConnectionFactory CreateConnectionFactory()
		{
			return new ConnectionFactory() { HostName = _options.HostName };
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
