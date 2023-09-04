using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using FluentAssertions;
using NSubstitute;
using Smiosoft.PASS.UnitTests.TestHelpers;
using Xunit;

namespace Smiosoft.PASS.ServiceBus.UnitTests.Publisher
{
    public partial class QueuePublisherTests
    {
        public class OnPublishAsync : QueuePublisherTests
        {
            [Fact]
            public async Task GivenConfiguredPublisher_WhenExected_ThenNoExceptionsAreThrown()
            {
                Func<Task> act = async () => await _sut.OnPublishAsync(new Payloads.DummyPayloadOne(), CancellationToken.None);

                await act.Should().NotThrowAsync();
            }

            [Fact]
            public async Task GivenConfiguredPublisher_WhenExected_ThenSenderCreatedOnce()
            {
                await _sut.OnPublishAsync(new Payloads.DummyPayloadOne(), CancellationToken.None);

                _mockServiceBusClient.Received(1).CreateSender(Arg.Any<string>());
            }

            [Fact]
            public async Task GivenConfiguredPublisher_WhenExected_ThenSendMessageOnce()
            {
                await _sut.OnPublishAsync(new Payloads.DummyPayloadOne(), CancellationToken.None);

                await _mockServiceBusSender.Received(1).SendMessageAsync(Arg.Any<ServiceBusMessage>(), Arg.Any<CancellationToken>());
            }
        }
    }
}
