using System.Text;
using Newtonsoft.Json;
using Smiosoft.PASS.Exceptions;

namespace Smiosoft.PASS.Extensions
{
	public static class MessageExtensions
	{
		public static TMessage Deserialise<TMessage>(this byte[] source)
			where TMessage : class
		{
			return JsonConvert.DeserializeObject<TMessage>(Encoding.UTF8.GetString(source))
						?? throw new DeserialisationException(typeof(TMessage));
		}

		public static byte[] Serialise<TMessage>(this TMessage value)
			where TMessage : class
		{
			return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value));
		}
	}
}
