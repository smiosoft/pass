using System;

namespace Smiosoft.PASS.ServiceBuss.Exceptions
{
	public class DeserialisationException : Exception
	{
		public DeserialisationException(string? message) : base(message)
		{ }

		public DeserialisationException(Type messageType)
			: base($"Failed to deserialise incoming message to type [{messageType.Name}]")
		{ }
	}
}
