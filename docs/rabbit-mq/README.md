# RabbitMQ

`Smiosoft.PASS.RabbitMQ` intends to be a simple, unambitious wrapper around RabbitMQ.

## Prerequisites

- [RabbitMQ](https://www.rabbitmq.com/download.html)

## Flows

These flows have been implemented from the official RabbitMQ tutorials:

- [Work Queues](https://www.rabbitmq.com/tutorials/tutorial-two-dotnet.html)
- [Topics](https://www.rabbitmq.com/tutorials/tutorial-five-dotnet.html)

## Setup

Using the PASS dependency injection `IServiceCollection.AddPass(...)` extension method, will register all publishers and subscribers in the provided assemblies.

Additionally, the `WithRabbitMq()` extension method on the options can be used to scope the registration to RabbitMQ related types.

```csharp
public void ConfigureServices(IServiceCollection services)
{
  services.AddPass((options) => { options.WithRabbitMq(); }, Assembly.GetExecutingAssembly())
}
```

## Publishers

Configure a publisher by creating a class that inherits from one of the following flows:

- `Smiosoft.PASS.RabbitMQ.Publisher.QueuePublisher<IPayload>`
- `Smiosoft.PASS.RabbitMQ.Publisher.TopicPublisher<IPayload>`

Ensure to provide a `Smiosoft.PASS.Payload.IPayload` object type that is intended to be published, this type will also be used to link back to the specific publisher when publishing.

```csharp
internal class ExamplePayload : IPayload
{ }

internal class ExampleQueuePublisher : QueuePublisher<ExamplePayload>
{
	public ExampleQueuePublisher() : base("localhost", "queue_name")
	{ }

	public override Task OnExceptionAsync(Exception exception)
    {
        return Task.CompletedTask;
    }
}
```

### Publishing

Use `Smiosoft.PASS.IPass` to publish payloads. PASS will match the given payload type with a configured publisher to handle the publishing.

```csharp
internal class Sandbox
{
	private readonly IPass _pass;

	public Sandbox(IPass pass)
	{
		_pass = pass;
	}

	public async Task ImportantTask()
	{
		// TODO: Implement important task

		// Publish a payload
		await _pass.PublishAsync(new ExamplePayload());
	}
}
```

## Subscribers

Configure a subscription by creating a class that inherits from one of the following flows:

- `Smiosoft.PASS.RabbitMQ.Subscriber.QueueSubscriber<IPayload>`
- `Smiosoft.PASS.RabbitMQ.Subscriber.TopicSubscriber<IPayload>`

Ensure to provide a `Smiosoft.PASS.Payload.IPayload` object type that is intended to be received.

```csharp
internal class ExamplePayload : IPayload
{ }

internal class ExampleQueueSubscription : QueueSubscriber<ExamplePayload>
{
	public ExampleQueueSubscription() : base("localhost", "queue_name")
	{ }

	public override Task OnExceptionAsync(Exception exception)
    {
        return Task.CompletedTask;
    }

    public override Task OnReceivedAsync(ExamplePayload payload, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
```
