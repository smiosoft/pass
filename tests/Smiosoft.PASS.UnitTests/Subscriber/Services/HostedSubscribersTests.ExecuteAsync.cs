using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using Smiosoft.PASS.Subscriber;
using Xunit;

namespace Smiosoft.PASS.UnitTests.Subscriber.Services
{
    public partial class HostedSubscribersTests
    {
        public class ExecuteAsync : HostedSubscribersTests
        {
            private readonly IListener _mockSubscriberOne;
            private readonly IListener _mockSubscriberTwo;
            private readonly IListener _mockSubscriberThree;

            public ExecuteAsync()
            {
                _mockSubscriberOne = Substitute.For<IListener>();
                _mockSubscriberTwo = Substitute.For<IListener>();
                _mockSubscriberThree = Substitute.For<IListener>();
            }

            [Fact]
            public async Task GivenMultipleConfiguredSubscribers_WhenExected_ThenRegisterAllSubscribers()
            {
                _mockServiceFactory(typeof(IEnumerable<IListener>))
                    .Returns(new IListener[]
                    {
                        _mockSubscriberOne,
                        _mockSubscriberTwo,
                        _mockSubscriberThree
                    });

                await _sut.StartAsync(CancellationToken.None);

                await _mockSubscriberOne.Received(1).RegisterAsync(Arg.Any<CancellationToken>());
                await _mockSubscriberTwo.Received(1).RegisterAsync(Arg.Any<CancellationToken>());
                await _mockSubscriberThree.Received(1).RegisterAsync(Arg.Any<CancellationToken>());
            }
        }
    }
}
