using System;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Smiosoft.PASS.Payload;
using Smiosoft.PASS.Publisher;

namespace Smiosoft.PASS.RabbitMQ.Publisher
{
	public abstract class PublisherBase<TPayload> : IPublishingHandler<TPayload>, IRabbitMq
		where TPayload : IPayload
	{
		private readonly PublisherOptions _options;

		protected PublisherBase(PublisherOptions options)
		{
			_options = options ?? throw new ArgumentNullException(nameof(options));
		}

		public async Task HandleAsync(TPayload payload, CancellationToken cancellationToken)
		{
			try
			{
				await OnPublishAsync(payload, cancellationToken);
			}
			catch (Exception exception)
			{
				await OnExceptionAsync(exception);
				throw;
			}
		}

		public abstract Task OnExceptionAsync(Exception exception);
		public abstract Task OnPublishAsync(TPayload payload, CancellationToken cancellationToken);

		protected virtual IConnectionFactory CreateConnectionFactory()
		{
			return new ConnectionFactory() { HostName = _options.HostName };
		}
	}
}
