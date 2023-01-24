using System;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Smiosoft.PASS.Payload;

namespace Smiosoft.PASS.RabbitMQ.Subscriber
{
    public abstract class TopicSubscriber<TPayload> : SubscriberBase<TPayload>
        where TPayload : IPayload
    {
        protected TopicSubscriberOptions Options { get; }

        protected TopicSubscriber(TopicSubscriberOptions options)
            : base(options)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
        }

        protected TopicSubscriber(string hostName, string exchangeName, string routingKey)
            : this(new TopicSubscriberOptions() { HostName = hostName, ExchangeName = exchangeName, RoutingKey = routingKey })
        { }

        public override Task OnRegistrationAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Channel.ExchangeDeclare(exchange: Options.ExchangeName, type: ExchangeType.Topic);
                var queueName = Channel.QueueDeclare().QueueName;
                Channel.QueueBind(queue: queueName, exchange: Options.ExchangeName, routingKey: Options.RoutingKey);

                var consumer = new EventingBasicConsumer(Channel);
                consumer.Received += Consumer_ReceivedAsync;

                Channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
            });
        }

        private async void Consumer_ReceivedAsync(object sender, BasicDeliverEventArgs args)
        {
            try
            {
                await OnReceivedAsync(args.Body.ToArray().Deserialise<TPayload>());
            }
            catch (Exception exception)
            {
                await OnExceptionAsync(exception);
            }
        }
    }
}
