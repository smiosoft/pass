# Azure Service Bus

`Smiosoft.PASS.ServiceBus` intends to be a simple, unambitious wrapper around Azure Service Bus.

## Subscribers

### Subscription configuration

Configure a subscription by creating a class that inherits from one of the following flows:

- `Smiosoft.PASS.ServiceBus.Subscriber.ServiceBusQueueSubscriber<>`
- `Smiosoft.PASS.ServiceBus.Subscriber.ServiceBusTopicSubscriber<>`

Ensure to provide a message object type that is intended to be recieved.

```csharp
internal class ExampleQueueSubscription : ServiceBusQueueSubscriber<MyMessage>
{
	public ExampleQueueSubscription() : base("connection_string", "queue_name")
	{ }

	public override Task OnExceptionAsync(Exception exception)
    {
        return Task.CompletedTask;
    }

    public override Task OnMessageRecievedAsync(MyMessage message, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
```

For users who need more fine grained control, or a custom flow can do so by inheriting `Smiosoft.PASS.ServiceBus.Subscriber.ServiceBusSubscriberBase<>`.

### Subscription registration

Register all your subscribers by including them in the service configuration options.

```csharp
public void ConfigureServices(IServiceCollection services)
{
	services.AddPassServiceBus(options =>
	{
		options.AddSubscriber<ExampleQueueSubscription>();
		options.AddSubscriber<ExampleTopicSubscription>();
	});
}
```

### Recieve messages

That is it, the configured subscribers will be registered and listening on a background service.

## Publishers

### Publisher configuration

Configure a publisher by creating a class that inherits from one of the following flows:

- `Smiosoft.PASS.ServiceBus.Subscriber.ServiceBusQueuePublisher<>`
- `Smiosoft.PASS.ServiceBus.Subscriber.ServiceBusTopicPublisher<>`

Ensure to provide a message object type that is intended to be published, this type will also be used to link back to the specific publisher when publishing.

```csharp
internal class ExampleQueuePublisher : ServiceBusQueuePublisher<MyMessage>
{
	public ExampleQueuePublisher() : base("connection_string", "queue_name")
	{ }
}
```

For users who need more fine grained control, or a custom flow can do so by inheriting `Smiosoft.PASS.ServiceBus.Subscriber.ServiceBusPublisherBase<>`.

### Publisher registration

Register all your publishers by including them in the service configuration options.

```csharp
public void ConfigureServices(IServiceCollection services)
{
	services.AddPassServiceBus(options =>
	{
		options.AddPublisher<ExampleQueuePublisher>();
		options.AddPublisher<ExampleTopicPublisher>();
	});
}
```

### Publish messages

Inject `IPublishingService` and use it to publish your messages. The library will match the given message object type with a configured publisher to publish your message!

```csharp
internal class Sandbox
{
	private readonly IPublishingService _publishingService;

	public Sandbox(IPublishingService publishingService)
	{
		_publishingService = publishingService;
	}

	private async Task ImportantTask()
	{
		// TODO: Implement important task

		// Notify the rest of the system important task is complete
		await _publishingService.PublishAsync<MyMessage>(new MyMessage("That thing you asked for is done."))
	}
}
```
