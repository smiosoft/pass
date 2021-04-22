using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace Smiosoft.PASS.ServiceBus.Queue
{
	public abstract class QueuePublisher<TMessage> : IQueuePublisher<TMessage>
		where TMessage : class
	{
		public IQueueClient Client { get; }

		public QueuePublisher(IQueueClient client)
		{
			Client = client;
		}

		public QueuePublisher(string connectionString, string queueName)
			: this(new QueueClient(connectionString, queueName))
		{ }

		public virtual Task PublishAsync(TMessage message)
		{
			return Client.SendAsync(new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message))));
		}
	}
}
