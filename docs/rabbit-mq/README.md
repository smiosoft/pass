# RabbitMQ

`Smiosoft.PASS.RabbitMQ` intends to be a simple, unambitious wrapper around RabbitMQ.

## Prerequisites

- [RabbitMQ](https://www.rabbitmq.com/download.html)

## Flows

These flows have been implemented from the official RabbitMQ tutorials:

- [Work Queues](https://www.rabbitmq.com/tutorials/tutorial-two-dotnet.html)
- [Topics](https://www.rabbitmq.com/tutorials/tutorial-five-dotnet.html)

## Subscribers

### Subscription configuration

Configure a subscription by creating a class that inherits from one of the following flows:

- `Smiosoft.PASS.RabbitMQ.Subscriber.RabbitMqQueueSubscriber<>`
- `Smiosoft.PASS.RabbitMQ.Subscriber.RabbitMqTopicSubscriber<>`

Ensure to provide a message object type that is intended to be received.

```csharp
internal class ExampleQueueSubscription : RabbitMqQueueSubscriber<MyMessage>
{
	public ExampleQueueSubscription() : base("localhost", "queue_name")
	{ }

	public override Task OnExceptionAsync(Exception exception)
    {
        return Task.CompletedTask;
    }

    public override Task OnMessageReceivedAsync(MyMessage message, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
```

For users who need more fine grained control, or a custom flow can do so by inheriting `Smiosoft.PASS.RabbitMQ.Subscriber.RabbitMqSubscriberBase<>`.

### Subscription registration

Register all your subscribers by including them in the service configuration options.

```csharp
public void ConfigureServices(IServiceCollection services)
{
	services.AddPassRabbitMq(options =>
	{
		options.AddSubscriber<ExampleQueueSubscription>();
		options.AddSubscriber<ExampleTopicSubscription>();
	});
}
```

### Receive messages

That is it, the configured subscribers will be registered and listening on a background service.

## Publishers

### Publisher configuration

Configure a publisher by creating a class that inherits from one of the following flows:

- `Smiosoft.PASS.RabbitMQ.Publisher.RabbitMqQueuePublisher<>`
- `Smiosoft.PASS.RabbitMQ.Publisher.RabbitMqTopicPublisher<>`

Ensure to provide a message object type that is intended to be published, this type will also be used to link back to the specific publisher when publishing.

```csharp
internal class ExampleQueuePublisher : RabbitMqQueuePublisher<MyMessage>
{
	public ExampleQueuePublisher() : base("localhost", "queue_name")
	{ }
}
```

For users who need more fine grained control, or a custom flow can do so by inheriting `Smiosoft.PASS.RabbitMQ.Publisher.RabbitMqPublisherBase<>`.

### Publisher registration

Register all your publishers by including them in the service configuration options.

```csharp
public void ConfigureServices(IServiceCollection services)
{
	services.AddPassRabbitMq(options =>
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
