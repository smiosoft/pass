using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace Smiosoft.PASS.ServiceBuss.Topic
{
	public abstract class TopicPublisher<TMessage> : ITopicPublisher<TMessage>
		where TMessage : class
	{
		public ITopicClient Client { get; }

		public TopicPublisher(ITopicClient client)
		{
			Client = client;
		}

		public TopicPublisher(string connectionString, string topicPath)
			: this(new TopicClient(connectionString, topicPath))
		{ }

		public virtual Task PublishAsync(TMessage message)
		{
			return Client.SendAsync(new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message))));
		}
	}
}
