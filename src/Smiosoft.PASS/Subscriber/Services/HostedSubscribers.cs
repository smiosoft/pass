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
        private readonly ServiceFactory _services;

        public HostedSubscribers(ServiceFactory serviceFactory)
        {
            _services = serviceFactory ?? throw new ArgumentNullException(nameof(serviceFactory));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var listeners = _services.GetInstances<IListener>() ?? Enumerable.Empty<IListener>();
            await Task.WhenAll(listeners.Select(listener => listener.RegisterAsync(stoppingToken)));
        }
    }
}
