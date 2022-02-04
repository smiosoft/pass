using System.Threading.Tasks;

namespace Smiosoft.PASS.ServiceBus.Publisher
{
	public class ServiceBusTopicPublisher<TMessage> : ServiceBusPublisherBase<TMessage>
		where TMessage : class
	{
		protected string TopicName { get; }

		public ServiceBusTopicPublisher(string connectionString, string topicName)
			: base(connectionString)
		{
			TopicName = topicName;
		}

		public override Task PublishAsync(TMessage message)
		{
			return SendMessageAsync(TopicName, message);
		}
	}
}
