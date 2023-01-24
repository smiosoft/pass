using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Smiosoft.PASS.Payload;

namespace Smiosoft.PASS.ServiceBus.Publisher
{
    public abstract class TopicPublisher<TPayload> : PublisherBase<TPayload>
        where TPayload : IPayload
    {
        protected TopicPublisherOptions Options { get; }

        protected TopicPublisher(TopicPublisherOptions options)
            : base(options)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
        }

        protected TopicPublisher(string connectionString, string topicName)
            : this(new TopicPublisherOptions() { ConnectionString = connectionString, TopicName = topicName })
        { }

        public override async Task OnPublishAsync(TPayload payload, CancellationToken cancellationToken)
        {
            await using var client = CreateClient();
            var sender = client.CreateSender(Options.TopicName);
            await sender.SendMessageAsync(new ServiceBusMessage(payload.Serialise()), cancellationToken);
        }
    }
}
