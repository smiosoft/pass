using System;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Smiosoft.PASS.RabbitMQ.Publisher;
using Smiosoft.PASS.UnitTests.TestHelpers.Messages;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.TestHelpers.Publishers
{
	public class MessageOneTopicPublisher : RabbitMqTopicPublisher<DummyTestMessageOne>
	{
		private readonly IConnectionFactory? _factory;

		public MessageOneTopicPublisher(string hostName, string exchangeName, string routingKey, IConnectionFactory factory) : base(hostName, exchangeName, routingKey)
		{
			_factory = factory;
		}

		public MessageOneTopicPublisher(string hostName, string exchangeName, string routingKey) : base(hostName, exchangeName, routingKey)
		{ }

		public override Task OnExceptionAsync(Exception exception)
		{
			return Task.CompletedTask;
		}

		protected override IConnectionFactory CreateConnectionFactory()
		{
			return _factory ?? base.CreateConnectionFactory();
		}
	}
}
