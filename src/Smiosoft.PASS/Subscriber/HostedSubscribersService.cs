using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Smiosoft.PASS.Subscriber
{
	internal class HostedSubscribersService : BackgroundService
	{
		private readonly IServiceProvider _provider;

		public HostedSubscribersService(IServiceProvider provider)
		{
			_provider = provider ?? throw new ArgumentNullException(nameof(provider));
		}

		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			var subscribers = _provider.GetServices<IBaseSubscriber>();
			return Task.WhenAll(subscribers.Select(subscriber => subscriber.RegisterAsync()));
		}
	}
}
