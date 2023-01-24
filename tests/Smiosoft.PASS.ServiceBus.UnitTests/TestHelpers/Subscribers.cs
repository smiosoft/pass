using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Smiosoft.PASS.ServiceBus.Subscriber;
using Smiosoft.PASS.UnitTests.TestHelpers;

namespace Smiosoft.PASS.ServiceBus.UnitTests.TestHelpers
{
    public static class Subscribers
    {
        public class QueueSubscriberOne : QueueSubscriber<Payloads.DummyPayloadOne>
        {
            private readonly ServiceBusProcessor? _processor;

            public QueueSubscriberOne(string connectionString, string queueName, ServiceBusProcessor? processor)
                : base(connectionString, queueName)
            {
                _processor = processor;
            }

            public override Task OnExceptionAsync(Exception exception)
            {
                return Task.CompletedTask;
            }

            public override Task OnReceivedAsync(Payloads.DummyPayloadOne payload, CancellationToken cancellationToken = default)
            {
                return Task.CompletedTask;
            }

            protected override ServiceBusProcessor CreateProcessor()
            {
                return _processor ?? base.CreateProcessor();
            }
        }

        public class TopicSubscriberOne : TopicSubscriber<Payloads.DummyPayloadOne>
        {
            private readonly ServiceBusProcessor? _processor;

            public TopicSubscriberOne(string connectionString, string topicName, string subscriptionName, ServiceBusProcessor? processor)
                : base(connectionString, topicName, subscriptionName)
            {
                _processor = processor;
            }
            public override Task OnExceptionAsync(Exception exception)
            {
                return Task.CompletedTask;
            }

            public override Task OnReceivedAsync(Payloads.DummyPayloadOne payload, CancellationToken cancellationToken = default)
            {
                return Task.CompletedTask;
            }

            protected override ServiceBusProcessor CreateProcessor()
            {
                return _processor ?? base.CreateProcessor();
            }
        }
    }
}
