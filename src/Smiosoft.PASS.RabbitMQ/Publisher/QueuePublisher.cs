using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Smiosoft.PASS.RabbitMQ.Publisher
{
	public abstract class QueuePublisher<TPayload> : PublisherBase<TPayload>
		where TPayload : IPayload
	{
		protected QueuePublisherOptions Options { get; }

		protected QueuePublisher(QueuePublisherOptions options) : base(options)
		{
			Options = options;
		}

		public override Task OnPublishAsync(TPayload payload, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				using var connection = Factory.CreateConnection();
				using var channel = connection.CreateModel();
				channel.QueueDeclare(queue: Options.QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

				var properties = channel.CreateBasicProperties();
				properties.Persistent = true;

				channel.BasicPublish(exchange: "", routingKey: Options.QueueName, basicProperties: properties, body: payload.Serialise());
			});
		}
	}
}