using Microsoft.Extensions.DependencyInjection;
using Smiosoft.PASS.Publisher.Handler;

namespace Smiosoft.PASS
{
	public static class PassInjection
	{
		public static IServiceCollection AddPass(this IServiceCollection services)
		{
			services.AddRequiredServices();

			return services;
		}

		private static IServiceCollection AddRequiredServices(this IServiceCollection services)
		{
			services
				.AddTransient<ServiceFactory>(provider => provider.GetRequiredService)
				.AddSingleton<IPass, Pass>();

			return services;
		}
	}
}
