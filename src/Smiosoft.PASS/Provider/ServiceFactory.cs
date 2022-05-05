using System;

namespace Smiosoft.PASS.Provider
{
	/// <summary>
	/// Factory method used to resolve all services />
	/// </summary>
	/// <param name="type">Type of service to resolve</param>
	/// <returns>An instance of type <paramref name="type" /></returns>
	internal delegate object ServiceFactory(Type type);
}
