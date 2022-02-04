using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Smiosoft.PASS.Extensions;

namespace Smiosoft.PASS.ServiceBus.Subscriber
{
	public abstract class ServiceBusSubscriberBase<TMessage> : IServiceBusSubscriber<TMessage>, IDisposable
		where TMessage : class
	{
		private bool _disposedValue;
		private ServiceBusProcessor? _processor;

		protected string ConnectionString { get; }
		protected ServiceBusClient Client { get; }
		protected ServiceBusProcessor Processor { get => _processor ??= CreateProcessor(); }

		protected ServiceBusSubscriberBase(string connectionString)
		{
			if (string.IsNullOrWhiteSpace(connectionString))
			{
				throw new ArgumentNullException(nameof(connectionString));
			}

			ConnectionString = connectionString;
			Client = new ServiceBusClient(ConnectionString);
		}

		public abstract Task OnExceptionAsync(Exception exception);

		public abstract Task OnMessageRecievedAsync(TMessage message, CancellationToken cancellationToken);

		public abstract Task RegisterAsync();

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposedValue)
			{
				if (disposing)
				{
					if (Client != null)
					{
						Client.DisposeAsync().GetAwaiter().GetResult();
					}
					if (Processor != null)
					{
						Processor.DisposeAsync().GetAwaiter().GetResult();
					}
				}

				_disposedValue = true;
			}
		}

		protected abstract ServiceBusProcessor CreateProcessor();

		protected async Task SetupProcessorAsync()
		{
			Processor.ProcessMessageAsync += async (args) =>
			{
				await OnMessageRecievedAsync(args.Message.Body.ToArray().Deserialise<TMessage>(), args.CancellationToken);
				await args.CompleteMessageAsync(args.Message, args.CancellationToken);
			};
			Processor.ProcessErrorAsync += async (args) =>
			{
				await OnExceptionAsync(args.Exception);
			};

			await Processor.StartProcessingAsync();
		}
	}
}
