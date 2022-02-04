using System;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Smiosoft.PASS.RabbitMQ.Publisher
{
	public abstract class RabbitMqPublisherBase<TMessage> : IRabbitMqPublisher<TMessage>
		where TMessage : class
	{
		protected IConnectionFactory Factory { get; }

		protected RabbitMqPublisherBase(IConnectionFactory factory)
		{
			Factory = factory ?? throw new ArgumentNullException(nameof(factory));
		}

		protected RabbitMqPublisherBase(string hostName)
		{
			if (string.IsNullOrWhiteSpace(hostName))
			{
				throw new ArgumentNullException(nameof(hostName));
			}

			Factory = new ConnectionFactory() { HostName = hostName };
		}

		public abstract Task PublishAsync(TMessage message);
	}
}
