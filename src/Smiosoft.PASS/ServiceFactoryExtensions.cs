using System.Collections.Generic;

namespace Smiosoft.PASS
{
	internal static class ServiceFactoryExtensions
	{
		public static TInstance GetInstance<TInstance>(this ServiceFactory factory)
		{
			return (TInstance)factory(typeof(TInstance));
		}

		public static IEnumerable<TInstance> GetInstances<TInstance>(this ServiceFactory factory)
		{
			return (IEnumerable<TInstance>)factory(typeof(IEnumerable<TInstance>));
		}
	}
}
