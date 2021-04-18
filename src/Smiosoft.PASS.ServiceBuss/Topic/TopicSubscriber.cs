using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Smiosoft.PASS.ServiceBuss.Topic
{
	public abstract class TopicSubscriber<TMessage> : ITopicSubscriber<TMessage>
		where TMessage : class
	{
		public ISubscriptionClient Client { get; }

		public TopicSubscriber(ISubscriptionClient client)
		{
			Client = client;
		}

		public TopicSubscriber(string connectionString, string topicPath, string subscriptionName)
			: this(new SubscriptionClient(connectionString, topicPath, subscriptionName))
		{ }

		public abstract Task OnMessageRecievedAsync(TMessage message, CancellationToken cancellationToken);

		public virtual Task OnExceptionAsync(Exception exception)
		{
			return Task.CompletedTask;
		}

		public virtual void Register()
		{
			Client.RegisterMessageHandler((message, cancellationToken) =>
			{
				var deserialised = JsonConvert.DeserializeObject<TMessage>(Encoding.UTF8.GetString(message.Body));
				return OnMessageRecievedAsync(deserialised, cancellationToken);
			},
			new MessageHandlerOptions((args) => OnExceptionAsync(args.Exception)));
		}
	}
}
