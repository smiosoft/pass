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

        protected IConnectionFactory Factory { get => _factory ??= CreateDefaultConnectionFactory(); }
        protected IConnection Connection { get => _connection ??= CreateDefaultConnection(); }
        protected IModel Channel { get => _channel ??= CreateDefaultChannel(); }

        protected SubscriberBase(SubscriberOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        protected SubscriberBase(SubscriberOptions options, IConnectionFactory factory)
            : this(options)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public async Task RegisterAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await OnRegistrationAsync(cancellationToken);
            }
            catch (Exception exception)
            {
                await OnExceptionAsync(exception);
            }
        }

        public abstract Task OnExceptionAsync(Exception exception);

        public abstract Task OnReceivedAsync(TPayload payload, CancellationToken cancellationToken = default);

        public abstract Task OnRegistrationAsync(CancellationToken cancellationToken);

        protected virtual IConnectionFactory CreateDefaultConnectionFactory()
        {
            return new ConnectionFactory() { HostName = _options.HostName };
        }

        protected virtual IConnection CreateDefaultConnection()
        {
            return Factory.CreateConnection();
        }

        protected virtual IModel CreateDefaultChannel()
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
