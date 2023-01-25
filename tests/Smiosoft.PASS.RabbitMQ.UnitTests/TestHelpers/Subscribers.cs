using System;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Smiosoft.PASS.RabbitMQ.Subscriber;
using Smiosoft.PASS.UnitTests.TestHelpers;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.TestHelpers
{
    public static class Subscribers
    {
        public class QueueSubscriberOne : QueueSubscriber<Payloads.DummyPayloadOne>
        {
            public QueueSubscriberOne(QueueSubscriberOptions options, IConnectionFactory factory)
                : base(options, factory)
            { }

            public QueueSubscriberOne(string hostName, string queueName, IConnectionFactory factory)
                : this(new QueueSubscriberOptions() { HostName = hostName, QueueName = queueName }, factory)
            { }

            public override Task OnExceptionAsync(Exception exception)
            {
                return Task.CompletedTask;
            }

            public override Task OnReceivedAsync(Payloads.DummyPayloadOne payload, CancellationToken cancellationToken = default)
            {
                return Task.CompletedTask;
            }
        }

        public class TopicSubscriberOne : TopicSubscriber<Payloads.DummyPayloadOne>
        {
            public TopicSubscriberOne(TopicSubscriberOptions options, IConnectionFactory factory)
                : base(options, factory)
            { }

            public TopicSubscriberOne(string hostName, string exchangeName, string queueName, string routingKey, IConnectionFactory factory)
                : this(new TopicSubscriberOptions() { HostName = hostName, ExchangeName = exchangeName, QueueName = queueName, RoutingKey = routingKey }, factory)
            { }

            public override Task OnExceptionAsync(Exception exception)
            {
                return Task.CompletedTask;
            }

            public override Task OnReceivedAsync(Payloads.DummyPayloadOne payload, CancellationToken cancellationToken = default)
            {
                return Task.CompletedTask;
            }
        }
    }
}
