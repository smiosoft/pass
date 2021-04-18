using System;

namespace Smiosoft.PASS.Publisher
{
	public class PublisherNotRegisteredException : Exception
	{
		public PublisherNotRegisteredException(string message) : base(message)
		{ }
	}
}
