using System;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Smiosoft.PASS.Extensions;

namespace Smiosoft.PASS.RabbitMQ.Subscriber
{
	public abstract class RabbitMqQueueSubscriber<TMessage> : RabbitMqSubscriberBase<TMessage>
		where TMessage : class
	{
		protected string QueueName { get; }

		protected RabbitMqQueueSubscriber(IConnectionFactory factory, string queueName)
			: base(factory)
		{
			if (string.IsNullOrWhiteSpace(queueName))
			{
				throw new ArgumentNullException(nameof(queueName));
			}

			QueueName = queueName;
		}

		protected RabbitMqQueueSubscriber(string hostName, string queueName)
			: base(hostName)
		{
			if (string.IsNullOrWhiteSpace(queueName))
			{
				throw new ArgumentNullException(nameof(queueName));
			}

			QueueName = queueName;
		}

		public override Task RegisterAsync()
		{
			return Task.Run(() =>
			{
				Channel.QueueDeclare(queue: QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
				Channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

				var consumer = new EventingBasicConsumer(Channel);
				consumer.Received += async (sender, args) =>
				{
					try
					{
						await OnMessageRecievedAsync(args.Body.ToArray().Deserialise<TMessage>(), default);
						Channel.BasicAck(deliveryTag: args.DeliveryTag, multiple: false);
					}
					catch (Exception exception)
					{
						await OnExceptionAsync(exception);
					}
				};

				Channel.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);
			});
		}
	}
}
