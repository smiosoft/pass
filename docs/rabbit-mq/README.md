# RabbitMQ

`Smiosoft.PASS.RabbitMQ` intends to be a simple, unambitious wrapper around RabbitMQ.

These flows have been implemented from the official RabbitMQ tutorials:

- [Work Queues](https://www.rabbitmq.com/tutorials/tutorial-two-dotnet.html)
- [Topics](https://www.rabbitmq.com/tutorials/tutorial-five-dotnet.html)

## Subscribers

### Subscription configuration

Configure a subscription by creating a class that inherits either `Smiosoft.PASS.RabbitMQ.Queue.QueueSubscriber<>` or `Smiosoft.PASS.RabbitMQ.Topic.TopicSubscriber<>`, and ensure to provide it with the message object type that you are expecting to recieve. The configuration is passed through the base constructor.

```csharp
internal class ExampleQueueSubscription : QueueSubscriber<MyMessage>
{
	public ExampleQueueSubscription() : base("localhost", "queue_name")
	{ }

	public override Task OnMessageRecievedAsync(MyMessage message, CancellationToken cancellationToken)
	{
		return Task.CompletedTask;
	}
}
```

### Subscription registration

Register all your subscribers by including them in the service configuration options.

```csharp
public void ConfigureServices(IServiceCollection services)
{
	services.AddPassRabbitMq(options =>
	{
		options.AddQueueSubscriber<ExampleQueueSubscriber>();
		options.AddTopicSubscriber<ExampleTopicSubscriber>();
	});
}
```

### Recieve messages

That is it, the configured subscribers will be registered and listening on a background service when you app is running.

## Publishers

### Publisher configuration

Configure a publisher by creating a class that inherits either `Smiosoft.PASS.RabbitMQ.Queue.QueuePublisher<>` or `Smiosoft.PASS.RabbitMQ.Topic.TopicPublisher<>`, and ensure to provide it with the message object type, this would be used to link back to the specific publisher when publishing. The configuration is passed through the base constructor.

```csharp
internal class ExampleQueuePublisher : QueuePublisher<MyMessage>
{
	public ExampleQueuePublisher() : base("localhost", "queue_name")
	{ }
}
```

### Publisher registration

Register all your publishers by including them in the service configuration options.

```csharp
public void ConfigureServices(IServiceCollection services)
{
	services.AddPassRabbitMq(options =>
	{
		options.AddQueuePublisher<ExampleQueuePublisher>();
		options.AddTopicPublisher<ExampleTopicPublisher>();
	});
}
```

### Publish messages

Inject `IPublishersService` and use it to publish your messages. The library will match the given message type with a configured publisher to publish your message!

```csharp
internal class Sandbox
{
	private readonly IPublishersService _publisher;

	public Sandbox(IPublishersService publisher)
	{
		_publisher = IPublishersService_publisher;
	}

	private async Task ImportantTask()
	{
		// TODO: Implement important task

		// Notify the rest of the system important task is complete
		await _publisher.PublishAsync<MyMessage>(new MyMessage("That thing you asked for is done."))
	}
}
```
