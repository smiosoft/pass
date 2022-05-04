using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Smiosoft.PASS.Provider;

namespace Smiosoft.PASS.Subscriber.Services
{
	internal class HostedSubscribers : BackgroundService
	{
		private readonly ServiceFactory _serviceFactory;

		public HostedSubscribers(ServiceFactory serviceFactory)
		{
			_serviceFactory = serviceFactory;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			var listeners = _serviceFactory.GetInstances<IListener>();
			foreach (var listener in listeners)
			{
				await listener.RegisterAsync();
			}
			//return Task.CompletedTask;
		}
	}
}
