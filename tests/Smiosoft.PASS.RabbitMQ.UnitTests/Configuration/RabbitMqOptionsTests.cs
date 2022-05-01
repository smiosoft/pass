using Microsoft.Extensions.DependencyInjection;
using Moq;
using Smiosoft.PASS.RabbitMQ.Configuration;

namespace Smiosoft.PASS.RabbitMQ.UnitTests.Configuration
{
	public partial class RabbitMqOptionsTests
	{
		private readonly Mock<IServiceCollection> _mockServiceCollection;
		private readonly ConfigureRabbitMqOptions _sut;

		public RabbitMqOptionsTests()
		{
			_mockServiceCollection = new Mock<IServiceCollection>();

			_sut = new ConfigureRabbitMqOptions(_mockServiceCollection.Object);
		}
	}
}
