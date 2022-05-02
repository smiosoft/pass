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
		protected RabbitMqTopicSubscriberOptions Options { get; }

		protected RabbitMqTopicSubscriber(RabbitMqTopicSubscriberOptions topicSubscriberOptions)
			: base(topicSubscriberOptions)
		{
			Options = topicSubscriberOptions ?? throw new ArgumentNullException(nameof(topicSubscriberOptions));
		}

		protected RabbitMqTopicSubscriber(string hostName, string exchangeName, string routingKey)
			: this(new RabbitMqTopicSubscriberOptions(hostName, exchangeName, routingKey))
		{ }

		public override async Task RegisterAsync()
		{
			try
			{
				Channel.ExchangeDeclare(exchange: Options.ExchangeName, type: ExchangeType.Topic);

				var queueName = Channel.QueueDeclare().QueueName;
				Channel.QueueBind(queue: queueName, exchange: Options.ExchangeName, routingKey: Options.RoutingKey);

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
