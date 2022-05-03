using System;

namespace Smiosoft.PASS
{
	/// <summary>
	/// Factory method used to resolve all services. For multiple instances, it will resolve against <see cref="IEnumerable{T}" />
	/// </summary>
	/// <param name="type">Type of service to resolve</param>
	/// <returns>An instance of type <paramref name="type" /></returns>
	internal delegate object ServiceFactory(Type type);
}
