using System;

namespace Smiosoft.PASS.ServiceBuss.Topic
{
	public class DeserialisationException : Exception
	{
		public DeserialisationException(string? message) : base(message)
		{ }
	}
}
