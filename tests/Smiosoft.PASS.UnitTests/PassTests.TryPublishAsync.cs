using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Smiosoft.PASS.Publisher;
using Smiosoft.PASS.UnitTests.TestHelpers;
using Xunit;

namespace Smiosoft.PASS.UnitTests
{
	public partial class PassTests
	{
		public class TryPublishAsync : PassTests
		{
			private readonly Mock<IPublishingHandler<Payloads.DummyPayloadOne>> _mockPublisherOne;
			private readonly Mock<IPublishingHandler<Payloads.DummyPayloadTwo>> _mockPublisherTwo;
			private readonly Mock<IPublishingHandler<Payloads.DummyPayloadThree>> _mockPublisherThree;

			public TryPublishAsync()
			{
				_mockPublisherOne = new Mock<IPublishingHandler<Payloads.DummyPayloadOne>>();
				_mockPublisherTwo = new Mock<IPublishingHandler<Payloads.DummyPayloadTwo>>();
				_mockPublisherThree = new Mock<IPublishingHandler<Payloads.DummyPayloadThree>>();
			}

			[Fact]
			public async Task GiventMultipleConfiguredPublishers_WhenPublishingARegisteredPayload_ThenHandledWithTheAppropriatePublisher()
			{
				_mockServiceFactory
					.Setup(_ => _(It.Is<Type>(type => type == typeof(IPublishingHandler<Payloads.DummyPayloadOne>))))
					.Returns(_mockPublisherOne.Object);
				_mockServiceFactory
					.Setup(_ => _(It.Is<Type>(type => type == typeof(IPublishingHandler<Payloads.DummyPayloadTwo>))))
					.Returns(_mockPublisherTwo.Object);
				_mockServiceFactory
					.Setup(_ => _(It.Is<Type>(type => type == typeof(IPublishingHandler<Payloads.DummyPayloadThree>))))
					.Returns(_mockPublisherThree.Object);

				await _sut.PublishAsync(new Payloads.DummyPayloadTwo());

				_mockPublisherOne.Verify(_ => _.HandleAsync(It.IsAny<Payloads.DummyPayloadOne>(), It.IsAny<CancellationToken>()), Times.Never);
				_mockPublisherTwo.Verify(_ => _.HandleAsync(It.IsAny<Payloads.DummyPayloadTwo>(), It.IsAny<CancellationToken>()), Times.Once);
				_mockPublisherThree.Verify(_ => _.HandleAsync(It.IsAny<Payloads.DummyPayloadThree>(), It.IsAny<CancellationToken>()), Times.Never);
			}

			[Fact]
			public async Task GiventMultipleConfiguredPublishers_WhenAPayloadHasBeenSuccessfullyPublished_ReturnTrue()
			{
				_mockServiceFactory
					.Setup(_ => _(It.Is<Type>(type => type == typeof(IPublishingHandler<Payloads.DummyPayloadOne>))))
					.Returns(_mockPublisherOne.Object);
				_mockServiceFactory
					.Setup(_ => _(It.Is<Type>(type => type == typeof(IPublishingHandler<Payloads.DummyPayloadTwo>))))
					.Returns(_mockPublisherTwo.Object);
				_mockServiceFactory
					.Setup(_ => _(It.Is<Type>(type => type == typeof(IPublishingHandler<Payloads.DummyPayloadThree>))))
					.Returns(_mockPublisherThree.Object);

				var result = await _sut.TryPublishAsync(new Payloads.DummyPayloadTwo());

				result.Should().BeTrue();
			}

			[Fact]
			public void GiventMultipleConfiguredPublishers_WhenPublishingAnUnregisteredPayload_ThenNoExceptionsAreThrown()
			{
				_mockServiceFactory
					.Setup(_ => _(It.Is<Type>(type => type == typeof(IPublishingHandler<Payloads.DummyPayloadOne>))))
					.Returns(_mockPublisherOne.Object);
				_mockServiceFactory
					.Setup(_ => _(It.Is<Type>(type => type == typeof(IPublishingHandler<Payloads.DummyPayloadTwo>))))
					.Returns(_mockPublisherTwo.Object);
				_mockServiceFactory
					.Setup(_ => _(It.Is<Type>(type => type == typeof(IPublishingHandler<Payloads.DummyPayloadThree>))))
					.Returns(_mockPublisherThree.Object);

				Func<Task> act = async () => await _sut.TryPublishAsync(new Payloads.DummyPayloadFive());

				act.Should().NotThrowAsync();
			}

			[Fact]
			public async Task GiventMultipleConfiguredPublishers_WhenPublishingAnUnregisteredPayload_ThenReturnFalse()
			{
				_mockServiceFactory
					.Setup(_ => _(It.Is<Type>(type => type == typeof(IPublishingHandler<Payloads.DummyPayloadOne>))))
					.Returns(_mockPublisherOne.Object);
				_mockServiceFactory
					.Setup(_ => _(It.Is<Type>(type => type == typeof(IPublishingHandler<Payloads.DummyPayloadTwo>))))
					.Returns(_mockPublisherTwo.Object);
				_mockServiceFactory
					.Setup(_ => _(It.Is<Type>(type => type == typeof(IPublishingHandler<Payloads.DummyPayloadThree>))))
					.Returns(_mockPublisherThree.Object);

				var result = await _sut.TryPublishAsync(new Payloads.DummyPayloadFive());

				result.Should().BeFalse();
			}

			[Fact]
			public void GiventMultipleConfiguredPublishers_WhenPublishingForAnErroneousPublisher_ThenNoExceptionsAreThrown()
			{
				_mockServiceFactory
					.Setup(_ => _(It.Is<Type>(type => type == typeof(IPublishingHandler<Payloads.DummyPayloadOne>))))
					.Returns(_mockPublisherOne.Object);
				_mockServiceFactory
					.Setup(_ => _(It.Is<Type>(type => type == typeof(IPublishingHandler<Payloads.DummyPayloadTwo>))))
					.Returns(_mockPublisherTwo.Object);
				_mockServiceFactory
					.Setup(_ => _(It.Is<Type>(type => type == typeof(IPublishingHandler<Payloads.DummyPayloadThree>))))
					.Returns(_mockPublisherThree.Object);

				_mockPublisherTwo
					.Setup(_ => _.HandleAsync(It.IsAny<Payloads.DummyPayloadTwo>(), It.IsAny<CancellationToken>()))
					.ThrowsAsync(new Exception("PUBLISHER IS IN THE BIN"));

				Func<Task> act = async () => await _sut.TryPublishAsync(new Payloads.DummyPayloadTwo());

				act.Should().NotThrowAsync();
			}

			[Fact]
			public async Task GiventMultipleConfiguredPublishers_WhenPublishingForAnErroneousPublisher_ThenReturnFalse()
			{
				_mockServiceFactory
					.Setup(_ => _(It.Is<Type>(type => type == typeof(IPublishingHandler<Payloads.DummyPayloadOne>))))
					.Returns(_mockPublisherOne.Object);
				_mockServiceFactory
					.Setup(_ => _(It.Is<Type>(type => type == typeof(IPublishingHandler<Payloads.DummyPayloadTwo>))))
					.Returns(_mockPublisherTwo.Object);
				_mockServiceFactory
					.Setup(_ => _(It.Is<Type>(type => type == typeof(IPublishingHandler<Payloads.DummyPayloadThree>))))
					.Returns(_mockPublisherThree.Object);

				_mockPublisherTwo
					.Setup(_ => _.HandleAsync(It.IsAny<Payloads.DummyPayloadTwo>(), It.IsAny<CancellationToken>()))
					.ThrowsAsync(new Exception("PUBLISHER IS IN THE BIN"));

				var result = await _sut.TryPublishAsync(new Payloads.DummyPayloadTwo());

				result.Should().BeFalse();
			}
		}
	}
}