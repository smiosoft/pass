using System;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Smiosoft.PASS.RabbitMQ.Publisher
{
	public abstract class RabbitMqPublisherBase<TMessage> : IRabbitMqPublisher<TMessage>
		where TMessage : class
	{
		private IConnectionFactory? _factory;

		protected string HostName { get; }
		protected IConnectionFactory Factory { get => _factory ??= CreateConnectionFactory(); }

		protected RabbitMqPublisherBase(string hostName)
		{
			if (string.IsNullOrWhiteSpace(hostName))
			{
				throw new ArgumentNullException(nameof(hostName));
			}

			HostName = hostName;
		}

		public abstract Task OnExceptionAsync(Exception exception);

		public abstract Task PublishAsync(TMessage message);

		protected virtual IConnectionFactory CreateConnectionFactory()
		{
			return new ConnectionFactory() { HostName = HostName };
		}
	}
}
