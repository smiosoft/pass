using System;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Smiosoft.PASS.Payload;

namespace Smiosoft.PASS.RabbitMQ.Publisher
{
	public abstract class TopicPublisher<TPayload> : PublisherBase<TPayload>
		where TPayload : IPayload
	{
		protected TopicPublisherOptions Options { get; }

		protected TopicPublisher(TopicPublisherOptions options)
			: base(options)
		{
			Options = options ?? throw new ArgumentNullException(nameof(options));
		}

		protected TopicPublisher(string hostName, string exchangeName, string routingKey)
			: this(new TopicPublisherOptions() { HostName = hostName, ExchangeName = exchangeName, RoutingKey = routingKey })
		{ }

		public override Task OnPublishAsync(TPayload payload, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var factory = CreateConnectionFactory();
				using var connection = factory.CreateConnection();
				using var channel = connection.CreateModel();
				channel.ExchangeDeclare(exchange: Options.ExchangeName, type: ExchangeType.Topic);
				channel.BasicPublish(
					exchange: Options.ExchangeName,
					routingKey: Options.RoutingKey,
					basicProperties: null,
					body: payload.Serialise());
			});
		}
	}
}
