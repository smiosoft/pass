using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace Smiosoft.PASS.ServiceBus.Topic
{
	public abstract class TopicPublisher<TMessage> : ITopicPublisher<TMessage>
		where TMessage : class
	{
		public ITopicClient Client { get; }

		protected TopicPublisher(ITopicClient client)
		{
			Client = client ?? throw new ArgumentNullException(nameof(client));
		}

		protected TopicPublisher(string connectionString, string topicPath)
		{
			if (string.IsNullOrWhiteSpace(connectionString))
			{
				throw new ArgumentNullException(nameof(connectionString));
			}

			if (string.IsNullOrWhiteSpace(topicPath))
			{
				throw new ArgumentNullException(nameof(topicPath));
			}


			Client = new TopicClient(connectionString, topicPath);
		}

		public virtual Task PublishAsync(TMessage message)
		{
			return Client.SendAsync(new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message))));
		}
	}
}
