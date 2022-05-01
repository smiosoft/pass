using System;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Smiosoft.PASS.Extensions;
using Smiosoft.PASS.RabbitMQ.Configuration;

namespace Smiosoft.PASS.RabbitMQ.Subscriber
{
	public abstract class RabbitMqTopicSubscriber<TMessage> : RabbitMqSubscriberBase<TMessage>
		where TMessage : class
	{
		protected RabbitMqTopicSubscriberOptions TopicSubscriberOptions { get; }

		protected RabbitMqTopicSubscriber(RabbitMqTopicSubscriberOptions topicSubscriberOptions)
			: base(topicSubscriberOptions)
		{
			TopicSubscriberOptions = topicSubscriberOptions ?? throw new ArgumentNullException(nameof(topicSubscriberOptions));
		}

		public override async Task RegisterAsync()
		{
			try
			{
				Channel.ExchangeDeclare(exchange: TopicSubscriberOptions.ExchangeName, type: ExchangeType.Topic);

				var queueName = Channel.QueueDeclare().QueueName;
				Channel.QueueBind(queue: queueName, exchange: TopicSubscriberOptions.ExchangeName, routingKey: TopicSubscriberOptions.RoutingKey);

				var consumer = new EventingBasicConsumer(Channel);
				consumer.Received += Consumer_ReceivedAsync;

				Channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
			}
			catch (Exception exception)
			{
				await OnExceptionAsync(exception);
			}
		}

		private async void Consumer_ReceivedAsync(object sender, BasicDeliverEventArgs args)
		{
			try
			{
				await OnMessageRecievedAsync(args.Body.ToArray().Deserialise<TMessage>(), default);
			}
			catch (Exception exception)
			{
				await OnExceptionAsync(exception);
			}
		}
	}
}
