using System;
using System.Linq;
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
			_serviceFactory = serviceFactory ?? throw new ArgumentNullException(nameof(serviceFactory));
		}

		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			var listeners = _serviceFactory.GetInstances<IListener>();
			return Task.WhenAll(listeners.Select(listener => listener.RegisterAsync()));
		}
	}
}
