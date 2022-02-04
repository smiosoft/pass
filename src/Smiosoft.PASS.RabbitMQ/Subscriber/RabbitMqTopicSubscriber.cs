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

		protected RabbitMqTopicSubscriber(IConnectionFactory factory, string exchangeName, string routingKey)
			: base(factory)
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

		public override Task RegisterAsync()
		{
			return Task.Run(() =>
			{
				Channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Topic);

				var queueName = Channel.QueueDeclare().QueueName;
				Channel.QueueBind(queue: queueName, exchange: ExchangeName, routingKey: RoutingKey);

				var consumer = new EventingBasicConsumer(Channel);
				consumer.Received += async (sender, args) =>
				{
					try
					{
						await OnMessageRecievedAsync(args.Body.ToArray().Deserialise<TMessage>(), default);
					}
					catch (Exception exception)
					{
						await OnExceptionAsync(exception);
					}
				};

				Channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
			});
		}
	}
}
