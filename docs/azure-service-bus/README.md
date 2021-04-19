# Azure Service Bus

## Queues + Topics

### Subscribers

#### Client configuration

Create a client by creating a class that inherits either `QueueSubscriber<>` or `TopicSubscriber<>`, and ensure to provide it with the message object type that you are expecting to recieve. The client configuration is passed through the base constructor.

```csharp
internal class ExampleQueueSubscription : QueueSubscriber<MyMessage>
{
	public ExampleQueueSubscription() : base("connection_string", "queue_name")
	{ }

	public override Task OnMessageRecievedAsync(MyMessage message, CancellationToken cancellationToken)
	{
		return Task.CompletedTask;
	}
}
```

#### Register the client configurations

Register all your subscribers by including them in the service configuration options.

```csharp
public void ConfigureServices(IServiceCollection services)
{
	services.AddPassServiceBus(options =>
	{
		options.AddQueueSubscriber<ExampleQueueSubscriber>();
		options.AddTopicSubscriber<ExampleTopicSubscriber>();
	});
}
```

#### Done

That is it, the configured subscribers will be registered and listening on a background service.

### Publishers

#### Client configuration

Create a client by creating a class that inherits either `QueuePublisher<>` or `TopicPublisher<>`, and ensure to provide it with the message object type, this would be used to link back to the client when publishing. The client configuration is passed through the base constructor.

```csharp
internal class ExampleQueuePublisher : QueuePublisher<MyMessage>
{
	public ExampleQueuePublisher() : base("connection_string", "queue_name")
	{ }
}
```

#### Register the client configurations

Register all your subscribers by including them in the service configuration options.

```csharp
public void ConfigureServices(IServiceCollection services)
{
	services.AddPassServiceBus(options =>
	{
		options.AddQueuePublisher<ExampleQueuePublisher>();
		options.AddTopicPublisher<ExampleTopicPublisher>();
	});
}
```

#### Publish messages

Use `IPublishersService` to publish your messages. [[See example](../publishing)]

