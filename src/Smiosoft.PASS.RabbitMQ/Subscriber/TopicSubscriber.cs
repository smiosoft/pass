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

        protected TopicSubscriber(TopicSubscriberOptions options, IConnectionFactory factory)
            : base(options, factory)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
        }

        protected TopicSubscriber(string hostName, string exchangeName, string queueName, string routingKey)
            : this(new TopicSubscriberOptions() { HostName = hostName, ExchangeName = exchangeName, QueueName = queueName, RoutingKey = routingKey })
        { }

        public override Task OnRegistrationAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Channel.ExchangeDeclare(exchange: Options.ExchangeName, type: ExchangeType.Topic);
                Channel.QueueDeclare(queue: Options.QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
                Channel.QueueBind(queue: Options.QueueName, exchange: Options.ExchangeName, routingKey: Options.RoutingKey);

                var consumer = new EventingBasicConsumer(Channel);
                consumer.Received += Consumer_ReceivedAsync;

                Channel.BasicConsume(queue: Options.QueueName, autoAck: false, consumer: consumer);
            });
        }

        private async void Consumer_ReceivedAsync(object sender, BasicDeliverEventArgs args)
        {
            try
            {
                await OnReceivedAsync(args.Body.ToArray().Deserialise<TPayload>());
                Channel.BasicAck(deliveryTag: args.DeliveryTag, multiple: false);
            }
            catch (Exception exception)
            {
                await OnExceptionAsync(exception);
            }
        }
    }
}
