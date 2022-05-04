using System;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Smiosoft.PASS.Payload;

namespace Smiosoft.PASS.RabbitMQ.Subscriber
{
	public abstract class QueueSubscriber<TPayload> : SubscriberBase<TPayload>
		where TPayload : IPayload
	{
		public QueueSubscriberOptions Options { get; }

		protected QueueSubscriber(QueueSubscriberOptions options)
			: base(options)
		{
			Options = options ?? throw new ArgumentNullException(nameof(options));
		}

		protected QueueSubscriber(string hostName, string queueName)
			: this(new QueueSubscriberOptions() { HostName = hostName, QueueName = queueName })
		{ }

		public override Task OnRegistrationAsync()
		{
			return Task.Run(() =>
			{
				Channel.QueueDeclare(
					queue: Options.QueueName,
					durable: true,
					exclusive: false,
					autoDelete: false,
					arguments: null);
				Channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

				var consumer = new EventingBasicConsumer(Channel);
				consumer.Received += Consumer_Received;

				Channel.BasicConsume(queue: Options.QueueName, autoAck: false, consumer: consumer);
			});
		}

		private async void Consumer_Received(object sender, BasicDeliverEventArgs args)
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
