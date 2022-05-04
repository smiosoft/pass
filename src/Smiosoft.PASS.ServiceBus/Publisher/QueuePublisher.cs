using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Smiosoft.PASS.Payload;

namespace Smiosoft.PASS.ServiceBus.Publisher
{
	public abstract class QueuePublisher<TPayload> : PublisherBase<TPayload>
		where TPayload : IPayload
	{
		protected QueuePublisherOptions Options { get; }

		protected QueuePublisher(QueuePublisherOptions options)
			: base(options)
		{
			Options = options ?? throw new ArgumentNullException(nameof(options));
		}

		protected QueuePublisher(string connectionString, string queueName)
			: this(new QueuePublisherOptions() { ConnectionString = connectionString, QueueName = queueName })
		{ }

		public override async Task OnPublishAsync(TPayload payload, CancellationToken cancellationToken)
		{
			await using var client = CreateClient();
			var sender = client.CreateSender(Options.QueueName);
			await sender.SendMessageAsync(new ServiceBusMessage(payload.Serialise()));
		}
	}
}
