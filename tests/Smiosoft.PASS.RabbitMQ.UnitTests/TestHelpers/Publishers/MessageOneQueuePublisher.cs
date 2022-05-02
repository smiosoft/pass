using System;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Smiosoft.PASS.RabbitMQ.Publisher;
using Smiosoft.PASS.UnitTests.TestHelpers.Messages;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.TestHelpers.Publishers
{
	public class MessageOneQueuePublisher : RabbitMqQueuePublisher<DummyTestMessageOne>
	{
		private readonly IConnectionFactory? _factory;

		public MessageOneQueuePublisher(string hostName, string queueName, IConnectionFactory factory) : base(hostName, queueName)
		{
			_factory = factory;
		}

		public MessageOneQueuePublisher(string hostName, string queueName) : base(hostName, queueName)
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
