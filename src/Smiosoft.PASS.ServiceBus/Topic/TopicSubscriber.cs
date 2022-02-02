using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Smiosoft.PASS.Extensions;

namespace Smiosoft.PASS.ServiceBus.Topic
{
	public abstract class TopicSubscriber<TMessage> : ITopicSubscriber<TMessage>
		where TMessage : class
	{
		public ISubscriptionClient Client { get; }

		protected TopicSubscriber(ISubscriptionClient client)
		{
			Client = client ?? throw new ArgumentNullException(nameof(client));
		}

		protected TopicSubscriber(string connectionString, string topicPath, string subscriptionName)
		{
			if (string.IsNullOrWhiteSpace(connectionString))
			{
				throw new ArgumentNullException(nameof(connectionString));
			}

			if (string.IsNullOrWhiteSpace(topicPath))
			{
				throw new ArgumentNullException(nameof(topicPath));
			}

			if (string.IsNullOrWhiteSpace(subscriptionName))
			{
				throw new ArgumentNullException(nameof(subscriptionName));
			}

			Client = new SubscriptionClient(connectionString, topicPath, subscriptionName);
		}

		public abstract Task OnMessageRecievedAsync(TMessage message, CancellationToken cancellationToken);

		public virtual Task OnExceptionAsync(Exception exception)
		{
			return Task.CompletedTask;
		}

		public virtual void Register()
		{
			Client.RegisterMessageHandler((message, cancellationToken) =>
			{
				return OnMessageRecievedAsync(message.Body.Deserialise<TMessage>(), cancellationToken);
			},
			new MessageHandlerOptions((args) => OnExceptionAsync(args.Exception)));
		}
	}
}
