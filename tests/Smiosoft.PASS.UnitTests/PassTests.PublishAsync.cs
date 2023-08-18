using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Smiosoft.PASS.Publisher;
using Smiosoft.PASS.UnitTests.TestHelpers;
using Xunit;

namespace Smiosoft.PASS.UnitTests
{
    public partial class PassTests
    {
        public class PublishAsync : PassTests
        {
            private readonly IPublishingHandler<Payloads.DummyPayloadOne> _mockPublisherOne;
            private readonly IPublishingHandler<Payloads.DummyPayloadTwo> _mockPublisherTwo;
            private readonly IPublishingHandler<Payloads.DummyPayloadThree> _mockPublisherThree;

            public PublishAsync()
            {
                _mockPublisherOne = Substitute.For<IPublishingHandler<Payloads.DummyPayloadOne>>();
                _mockPublisherTwo = Substitute.For<IPublishingHandler <Payloads.DummyPayloadTwo>>();
                _mockPublisherThree = Substitute.For<IPublishingHandler<Payloads.DummyPayloadThree>>();
            }

            [Fact]
            public async Task GiventMultipleConfiguredPublishers_WhenPublishingARegisteredPayload_ThenHandledWithTheAppropriatePublisher()
            {
                _mockServiceFactory(Arg.Is<Type>(type => type == typeof(IPublishingHandler<Payloads.DummyPayloadOne>)))
                    .Returns(_mockPublisherOne);
                _mockServiceFactory(Arg.Is<Type>(type => type == typeof(IPublishingHandler<Payloads.DummyPayloadTwo>)))
                    .Returns(_mockPublisherTwo);
                _mockServiceFactory(Arg.Is<Type>(type => type == typeof(IPublishingHandler<Payloads.DummyPayloadThree>)))
                    .Returns(_mockPublisherThree);

                await _sut.PublishAsync(new Payloads.DummyPayloadTwo());

                await _mockPublisherOne.DidNotReceive().HandleAsync(Arg.Any<Payloads.DummyPayloadOne>(), Arg.Any<CancellationToken>());
                await _mockPublisherTwo.Received(1).HandleAsync(Arg.Any<Payloads.DummyPayloadTwo>(), Arg.Any<CancellationToken>());
                await _mockPublisherThree.DidNotReceive().HandleAsync(Arg.Any<Payloads.DummyPayloadThree>(), Arg.Any<CancellationToken>());
            }

            [Fact]
            public void GiventMultipleConfiguredPublishers_WhenPublishingAnUnregisteredPayload_ThenInvalidOperationExceptionIsThrown()
            {
                _mockServiceFactory(Arg.Is<Type>(type => type == typeof(IPublishingHandler<Payloads.DummyPayloadOne>)))
                    .Returns(_mockPublisherOne);
                _mockServiceFactory(Arg.Is<Type>(type => type == typeof(IPublishingHandler<Payloads.DummyPayloadTwo>)))
                    .Returns(_mockPublisherTwo);
                _mockServiceFactory(Arg.Is<Type>(type => type == typeof(IPublishingHandler<Payloads.DummyPayloadThree>)))
                    .Returns(_mockPublisherThree);

                Func<Task> act = async () => await _sut.PublishAsync(new Payloads.DummyPayloadFive());

                act.Should().ThrowAsync<InvalidOperationException>();
            }

            [Fact]
            public void GiventMultipleConfiguredPublishers_WhenPublishingForAnErroneousPublisher_ThenExceptionIsThrown()
            {
                _mockServiceFactory(Arg.Is<Type>(type => type == typeof(IPublishingHandler<Payloads.DummyPayloadOne>)))
                    .Returns(_mockPublisherOne);
                _mockServiceFactory(Arg.Is<Type>(type => type == typeof(IPublishingHandler<Payloads.DummyPayloadTwo>)))
                    .Returns(_mockPublisherTwo);
                _mockServiceFactory(Arg.Is<Type>(type => type == typeof(IPublishingHandler<Payloads.DummyPayloadThree>)))
                    .Returns(_mockPublisherThree);

                _mockPublisherTwo
                    .HandleAsync(Arg.Any<Payloads.DummyPayloadTwo>(), Arg.Any<CancellationToken>())
                    .Throws(new Exception("PUBLISHER IS IN THE BIN"));

                Func<Task> act = async () => await _sut.PublishAsync(new Payloads.DummyPayloadTwo());

                act.Should().ThrowAsync<Exception>();
            }
        }
    }
}
