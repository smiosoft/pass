# Publishing

Inject `IPublishersService` and use it to publish your messages. The library will match the given message type with a configured client, and publish your message!

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
