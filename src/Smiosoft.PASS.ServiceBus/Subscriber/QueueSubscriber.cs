using System;
using Azure.Messaging.ServiceBus;
using Smiosoft.PASS.Payload;

namespace Smiosoft.PASS.ServiceBus.Subscriber
{
	public abstract class QueueSubscriber<TPayload> : SubscriberBase<TPayload>
		where TPayload : IPayload
	{
		public QueueSubscriberOptions Options { get; }

		protected QueueSubscriber(QueueSubscriberOptions options) : base(options)
		{
			Options = options ?? throw new ArgumentNullException(nameof(options));
		}

		protected QueueSubscriber(string connectionString, string queueName)
			: this(new QueueSubscriberOptions() { ConnectionString = connectionString, QueueName = queueName })
		{ }

		protected override ServiceBusProcessor CreateProcessor()
		{
			return Client.CreateProcessor(queueName: Options.QueueName);
		}
	}
}
