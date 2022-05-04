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
			Options = options;
		}

		protected QueueSubscriber(string hostName, string queueName)
			: this(new QueueSubscriberOptions() { HostName = hostName, QueueName = queueName })
		{ }

		public override Task RegisterAsync()
		{
			return Task.Run(() =>
			{
				var factory = CreateConnectionFactory();
				using var connection = Factory.CreateConnection();
				using var channel = connection.CreateModel();
				channel.QueueDeclare(
					queue: Options.QueueName,
					durable: true,
					exclusive: false,
					autoDelete: false,
					arguments: null);
				channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

				var consumer = new EventingBasicConsumer(channel);
				consumer.Received += async (sender, args) =>
				{
					await HandleAsync(args.Body.ToArray().Deserialise<TPayload>(), default);
					channel.BasicAck(deliveryTag: args.DeliveryTag, multiple: false);
				};

				channel.BasicConsume(queue: Options.QueueName, autoAck: false, consumer: consumer);
			});
		}
	}
}
