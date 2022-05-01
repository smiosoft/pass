using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Smiosoft.PASS.Extensions;
using Smiosoft.PASS.ServiceBus.Configuration;

namespace Smiosoft.PASS.ServiceBus.Publisher
{
	public abstract class ServiceBusPublisherBase<TMessage> : IServiceBusPublisher<TMessage>
		where TMessage : class
	{
		private readonly ServiceBusPublisherOptions _options;

		protected ServiceBusPublisherBase(ServiceBusPublisherOptions options)
		{
			_options = options ?? throw new ArgumentNullException(nameof(options));
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

				await using var client = new ServiceBusClient(_options.ConnectionString);
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
