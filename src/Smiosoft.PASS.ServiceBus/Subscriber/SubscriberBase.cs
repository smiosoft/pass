using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Smiosoft.PASS.Payload;
using Smiosoft.PASS.Subscriber;

namespace Smiosoft.PASS.ServiceBus.Subscriber
{
	public abstract class SubscriberBase<TPayload> : ISubscriptionHandler<TPayload>, IServiceBus, IDisposable
		where TPayload : IPayload
	{
		private readonly SubscriberOptions _options;
		private bool _disposedValue;
		private ServiceBusClient? _client;
		private ServiceBusProcessor? _processor;

		protected ServiceBusClient Client { get => _client ??= CreateClient(); }
		protected ServiceBusProcessor Processor { get => _processor ??= CreateProcessor(); }

		protected SubscriberBase(SubscriberOptions options)
		{
			_options = options ?? throw new ArgumentNullException(nameof(options));
		}

		public async Task RegisterAsync()
		{
			try
			{
				await OnRegistrationAsync();
			}
			catch (Exception exception)
			{
				await OnExceptionAsync(exception);
			}
		}

		public abstract Task OnExceptionAsync(Exception exception);

		public abstract Task OnRecivedAsync(TPayload payload, CancellationToken cancellationToken = default);

		public virtual async Task OnRegistrationAsync()
		{
			Processor.ProcessMessageAsync += Processor_ProcessMessageAsync;
			Processor.ProcessErrorAsync += Processor_ProcessErrorAsync;
			await Processor.StartProcessingAsync();
		}

		protected virtual ServiceBusClient CreateClient()
		{
			return new ServiceBusClient(_options.ConnectionString);
		}

		protected abstract ServiceBusProcessor CreateProcessor();

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposedValue)
			{
				if (disposing)
				{
					if (_client != null)
					{
						_client.DisposeAsync().GetAwaiter().GetResult();
						_client = null;
					}

					if (_processor != null)
					{
						_processor.DisposeAsync().GetAwaiter().GetResult();
						_processor = null;
					}
				}

				_disposedValue = true;
			}
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		private async Task Processor_ProcessMessageAsync(ProcessMessageEventArgs args)
		{
			try
			{
				await OnRecivedAsync(args.Message.Body.ToArray().Deserialise<TPayload>(), args.CancellationToken);
				await args.CompleteMessageAsync(args.Message, args.CancellationToken);
			}
			catch (Exception exception)
			{
				await OnExceptionAsync(exception);
			}
		}

		private Task Processor_ProcessErrorAsync(ProcessErrorEventArgs args)
		{
			return OnExceptionAsync(args.Exception);
		}
	}
}
