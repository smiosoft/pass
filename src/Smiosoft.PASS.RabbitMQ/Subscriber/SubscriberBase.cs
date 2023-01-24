using System;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Smiosoft.PASS.Payload;
using Smiosoft.PASS.Subscriber;

namespace Smiosoft.PASS.RabbitMQ.Subscriber
{
    public abstract class SubscriberBase<TPayload> : ISubscriptionHandler<TPayload>, IRabbitMq, IDisposable
        where TPayload : IPayload
    {
        private readonly SubscriberOptions _options;
        private bool _disposedValue;
        private IConnectionFactory? _factory;
        private IConnection? _connection;
        private IModel? _channel;

        protected IConnectionFactory Factory { get => _factory ??= CreateConnectionFactory(); }
        protected IConnection Connection { get => _connection ??= CreateConnection(); }
        protected IModel Channel { get => _channel ??= CreateChannel(); }

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

        public abstract Task OnReceivedAsync(TPayload payload, CancellationToken cancellationToken = default);

        public abstract Task OnRegistrationAsync();

        protected virtual IConnectionFactory CreateConnectionFactory()
        {
            return new ConnectionFactory() { HostName = _options.HostName };
        }

        protected virtual IConnection CreateConnection()
        {
            return Factory.CreateConnection();
        }

        protected virtual IModel CreateChannel()
        {
            return Connection.CreateModel();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }

                    if (_channel != null)
                    {
                        _channel.Dispose();
                        _channel = null;
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
    }
}
