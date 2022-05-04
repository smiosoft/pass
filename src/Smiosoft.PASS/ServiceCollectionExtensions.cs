using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Smiosoft.PASS.Publisher;
using Smiosoft.PASS.Publisher.Handler;

namespace Smiosoft.PASS
{
	/// <summary>
	/// Extensions to register everything PASS.
	/// <br />- Registers <see cref="Smiosoft.PASS.Publisher.Handler.ServiceFactory"/> and <see cref="Smiosoft.PASS.IPass"/> as transient instances.
	/// <br />- Scans for any handler interface implementations and registers them as transient instances.
	/// <br />After calling AddPass you can use the container to resolve an <see cref="Smiosoft.PASS.IPass"/> instance.
	/// </summary>
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Registers handlers and PASS types from the specified assemblies
		/// </summary>
		/// <param name="services">Service collection</param>
		/// <param name="assemblies">Assemblies to scan</param>
		/// <returns>Service collection</returns>
		public static IServiceCollection AddPass(this IServiceCollection services, params Assembly[] assemblies)
		{
			return services.AddPass(setup: null, assemblies.AsEnumerable());
		}

		/// <summary>
		/// Registers handlers and PASS types from the specified assemblies
		/// </summary>
		/// <param name="services">Service collection</param>
		/// <param name="configuration">The action used to configure the options</param>
		/// <param name="assemblies">Assemblies to scan</param>
		/// <returns>Service collection</returns>
		public static IServiceCollection AddPass(this IServiceCollection services, Action<PassServiceConfiguration>? setup, params Assembly[] assemblies)
		{
			return services.AddPass(setup, assemblies.AsEnumerable());
		}

		/// <summary>
		/// Registers handlers and PASS types from the specified assemblies
		/// </summary>
		/// <param name="services">Service collection</param>
		/// <param name="configuration">The action used to configure the options</param>
		/// <param name="assemblies">Assemblies to scan</param>
		/// <returns>Service collection</returns>
		/// <exception cref="ArgumentException"></exception>
		public static IServiceCollection AddPass(this IServiceCollection services, Action<PassServiceConfiguration>? setup, IEnumerable<Assembly> assemblies)
		{
			if (!assemblies.Any())
			{
				throw new ArgumentException("No assemblies found to scan. Supply at least one assembly to scan for handlers.");
			}

			var configuration = new PassServiceConfiguration();
			setup?.Invoke(configuration);

			AddRequiredServices(services);
			AddHandlers(services, configuration, assemblies);

			return services;
		}

		private static void AddRequiredServices(IServiceCollection services)
		{
			services.AddTransient<ServiceFactory>(provider => provider.GetRequiredService);
			services.AddTransient<IPass, Pass>();
		}

		private static void AddHandlers(IServiceCollection services, PassServiceConfiguration configuration, IEnumerable<Assembly> assemblies)
		{
			var types = assemblies
				.Distinct()
				.SelectMany(assembly => assembly.DefinedTypes)
				.Where(configuration.TypeEvaluator);
			foreach (var type in types)
			{
				var interfaces = type.GetInterfaces();
				if (!interfaces.Any()) continue;

				var handlerInterface = interfaces.FirstOrDefault(@interface => @interface.IsGenericType && typeof(IPublishingHandler<>) == @interface.GetGenericTypeDefinition());
				if (handlerInterface == null) continue;

				services.AddTransient(handlerInterface, type);
			}
		}
	}
}
