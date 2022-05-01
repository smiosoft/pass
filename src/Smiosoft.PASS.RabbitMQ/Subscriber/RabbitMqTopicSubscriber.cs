using System;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Smiosoft.PASS.Extensions;

namespace Smiosoft.PASS.RabbitMQ.Subscriber
{
	public abstract class RabbitMqTopicSubscriber<TMessage> : RabbitMqSubscriberBase<TMessage>
		where TMessage : class
	{
		protected string ExchangeName { get; }
		protected string RoutingKey { get; }

		protected RabbitMqTopicSubscriber(string hostName, string exchangeName, string routingKey)
			: base(hostName)
		{
			if (string.IsNullOrWhiteSpace(exchangeName))
			{
				throw new ArgumentNullException(nameof(exchangeName));
			}

			if (string.IsNullOrWhiteSpace(routingKey))
			{
				throw new ArgumentNullException(nameof(routingKey));
			}

			ExchangeName = exchangeName;
			RoutingKey = routingKey;
		}

		public override async Task RegisterAsync()
		{
			try
			{
				Channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Topic);

				var queueName = Channel.QueueDeclare().QueueName;
				Channel.QueueBind(queue: queueName, exchange: ExchangeName, routingKey: RoutingKey);

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
