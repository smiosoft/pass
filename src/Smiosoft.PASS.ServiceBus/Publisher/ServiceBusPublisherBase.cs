using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Smiosoft.PASS.Extensions;

namespace Smiosoft.PASS.ServiceBus.Publisher
{
	public abstract class ServiceBusPublisherBase<TMessage> : IServiceBusPublisher<TMessage>
		where TMessage : class
	{
		protected string ConnectionString { get; }

		protected ServiceBusPublisherBase(string connectionString)
		{
			if (string.IsNullOrWhiteSpace(connectionString))
			{
				throw new ArgumentNullException(nameof(connectionString));
			}

			ConnectionString = connectionString;
		}

		public abstract Task OnExceptionAsync(Exception exception);

		public abstract Task PublishAsync(TMessage message);

		protected async Task SendMessageAsync(string queueOrTopicName, TMessage message)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(queueOrTopicName))
				{
					throw new ArgumentNullException(nameof(queueOrTopicName));
				}

				if (message == null)
				{
					throw new ArgumentNullException(nameof(message));
				}

				await using var client = new ServiceBusClient(ConnectionString);
				var sender = client.CreateSender(queueOrTopicName);
				await sender.SendMessageAsync(new ServiceBusMessage(message.Serialise()));
			}
			catch (Exception exception)
			{
				await OnExceptionAsync(exception);
			}
		}
	}
}
