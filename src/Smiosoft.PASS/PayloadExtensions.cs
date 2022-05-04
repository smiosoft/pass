using System;
using System.Text;
using Newtonsoft.Json;

namespace Smiosoft.PASS
{
	public static class PayloadExtensions
	{
		public static TPayload Deserialise<TPayload>(this byte[] source)
			where TPayload : IPayload
		{
			try
			{
				return JsonConvert.DeserializeObject<TPayload>(Encoding.UTF8.GetString(source))
					?? throw new InvalidOperationException($"Failed to deserialise payload of type {typeof(TPayload)}.");
			}
			catch (Exception exception)
			{
				throw new InvalidOperationException($"Error deserialising payload of type {typeof(TPayload)}.", exception);
			}
		}

		public static byte[] Serialise<TPayload>(this TPayload source)
			where TPayload : IPayload
		{
			try
			{
				return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(source));
			}
			catch (Exception exception)
			{
				throw new InvalidOperationException($"Error serialising payload of type {typeof(TPayload)}.", exception);
			}
		}
	}
}
