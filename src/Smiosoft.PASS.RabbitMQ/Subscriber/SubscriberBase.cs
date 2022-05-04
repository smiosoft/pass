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

		protected IConnectionFactory Factory { get => _factory ??= CreateConnectionFactory(); }

		protected SubscriberBase(SubscriberOptions options)
		{
			_options = options ?? throw new ArgumentNullException(nameof(options));
		}

		public async Task HandleAsync(TPayload payload, CancellationToken cancellationToken)
		{
			try
			{
				await OnRecivedAsync(payload, cancellationToken);
			}
			catch (Exception exception)
			{
				await OnExceptionAsync(exception);
			}
		}

		public abstract Task RegisterAsync();
		public abstract Task OnExceptionAsync(Exception exception);
		public abstract Task OnRecivedAsync(TPayload payload, CancellationToken cancellationToken);

		protected virtual IConnectionFactory CreateConnectionFactory()
		{
			return new ConnectionFactory() { HostName = _options.HostName };
		}
	}
}
