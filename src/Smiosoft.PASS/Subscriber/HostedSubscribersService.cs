using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Smiosoft.PASS.Subscriber
{
	internal class HostedSubscribersService : BackgroundService
	{
		private readonly IServiceProvider _provider;

		public HostedSubscribersService(IServiceProvider provider)
		{
			_provider = provider;
		}

		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			var subscribers = _provider.GetServices<IBaseSubscriber>();
			foreach (var subscriber in subscribers)
			{
				subscriber.Register();
			}

			return Task.CompletedTask;
		}
	}
}
