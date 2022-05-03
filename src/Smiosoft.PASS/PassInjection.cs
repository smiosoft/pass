using Microsoft.Extensions.DependencyInjection;

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
			services.AddTransient<ServiceFactory>(provider => provider.GetRequiredService);

			return services;
		}
	}
}
