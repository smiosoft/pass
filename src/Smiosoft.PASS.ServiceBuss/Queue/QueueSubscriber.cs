using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using Smiosoft.PASS.ServiceBuss.Exceptions;

namespace Smiosoft.PASS.ServiceBuss.Queue
{
	public abstract class QueueSubscriber<TMessage> : IQueueSubscriber<TMessage>
		where TMessage : class
	{
		public IQueueClient Client { get; }

		public QueueSubscriber(IQueueClient client)
		{
			Client = client;
		}

		public QueueSubscriber(string connectionString, string queueName)
			: this(new QueueClient(connectionString, queueName))
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
				var deserialised = JsonConvert.DeserializeObject<TMessage>(Encoding.UTF8.GetString(message.Body))
					?? throw new DeserialisationException(typeof(TMessage));
				return OnMessageRecievedAsync(deserialised, cancellationToken);
			},
			new MessageHandlerOptions((args) => OnExceptionAsync(args.Exception)));
		}
	}
}
