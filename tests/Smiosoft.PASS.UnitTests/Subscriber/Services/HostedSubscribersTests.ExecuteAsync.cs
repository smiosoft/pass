using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Smiosoft.PASS.Subscriber;
using Xunit;

namespace Smiosoft.PASS.UnitTests.Subscriber.Services
{
    public partial class HostedSubscribersTests
    {
        public class ExecuteAsync : HostedSubscribersTests
        {
            private readonly Mock<IListener> _mockSubscriberOne;
            private readonly Mock<IListener> _mockSubscriberTwo;
            private readonly Mock<IListener> _mockSubscriberThree;

            public ExecuteAsync()
            {
                _mockSubscriberOne = new Mock<IListener>();
                _mockSubscriberTwo = new Mock<IListener>();
                _mockSubscriberThree = new Mock<IListener>();
            }

            [Fact]
            public async Task GivenMultipleConfiguredSubscribers_WhenExected_ThenRegisterAllSubscribers()
            {
                _mockServiceFactory
                    .Setup(_ => _.Invoke(typeof(IEnumerable<IListener>)))
                    .Returns(new IListener[]
                    {
                        _mockSubscriberOne.Object,
                        _mockSubscriberTwo.Object,
                        _mockSubscriberThree.Object
                    });

                await _sut.StartAsync(CancellationToken.None);

                _mockSubscriberOne.Verify(_ => _.RegisterAsync(), Times.Once);
                _mockSubscriberTwo.Verify(_ => _.RegisterAsync(), Times.Once);
                _mockSubscriberThree.Verify(_ => _.RegisterAsync(), Times.Once);
            }
        }
    }
}
