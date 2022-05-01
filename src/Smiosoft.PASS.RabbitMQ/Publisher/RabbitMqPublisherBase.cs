using System;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Smiosoft.PASS.RabbitMQ.Configuration;

namespace Smiosoft.PASS.RabbitMQ.Publisher
{
	public abstract class RabbitMqPublisherBase<TMessage> : IRabbitMqPublisher<TMessage>
		where TMessage : class
	{
		private readonly RabbitMqPublisherOptions _options;
		private IConnectionFactory? _factory;

		protected IConnectionFactory Factory { get => _factory ??= CreateConnectionFactory(); }

		protected RabbitMqPublisherBase(RabbitMqPublisherOptions options)
		{
			_options = options ?? throw new ArgumentNullException(nameof(options));
		}

		public abstract Task OnExceptionAsync(Exception exception);

		public abstract Task PublishAsync(TMessage message);

		protected virtual IConnectionFactory CreateConnectionFactory()
		{
			return new ConnectionFactory() { HostName = _options.HostName };
		}
	}
}
