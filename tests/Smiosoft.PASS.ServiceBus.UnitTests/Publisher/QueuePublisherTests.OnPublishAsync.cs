﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using FluentAssertions;
using Moq;
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

                _mockServiceBusClient.Verify(
                    _ => _.CreateSender(It.IsAny<string>()),
                    Times.Once);
            }

            [Fact]
            public async Task GivenConfiguredPublisher_WhenExected_ThenSendMessageOnce()
            {
                await _sut.OnPublishAsync(new Payloads.DummyPayloadOne(), CancellationToken.None);

                _mockServiceBusSender.Verify(
                    _ => _.SendMessageAsync(It.IsAny<ServiceBusMessage>(), It.IsAny<CancellationToken>()),
                    Times.Once);
            }
        }
    }
}
