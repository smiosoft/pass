using System;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Smiosoft.PASS.Publisher;
using Smiosoft.PASS.RabbitMQ.Configuration;

namespace Smiosoft.PASS.RabbitMQ.Publisher
{
	public abstract class PublisherBase<TPayload> : IPublishingHandler<TPayload>
		where TPayload : IPayload
	{
		private readonly RabbitMqOptions _options;
		private IConnectionFactory? _factory;

		protected IConnectionFactory Factory { get => _factory ??= CreateConnectionFactory(); }

		protected PublisherBase(RabbitMqOptions options)
		{
			_options = options ?? throw new ArgumentNullException(nameof(options));
		}

		public abstract Task OnExceptionAsync(Exception exception);
		public abstract Task OnPublishAsync(TPayload payload, CancellationToken cancellationToken);

		public async Task HandleAsync(TPayload payload, CancellationToken cancellationToken)
		{
			try
			{
				await OnPublishAsync(payload, cancellationToken);
			}
			catch (Exception exception)
			{
				await OnExceptionAsync(exception);
			}
		}

		protected virtual IConnectionFactory CreateConnectionFactory()
		{
			return new ConnectionFactory() { HostName = _options.HostName };
		}
	}
}
