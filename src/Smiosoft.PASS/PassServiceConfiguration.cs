using System;

namespace Smiosoft.PASS
{
	public class PassServiceConfiguration
	{
		internal Func<Type, bool> TypeEvaluator { get; private set; }

		public PassServiceConfiguration()
		{
			TypeEvaluator = (type) => typeof(IDomain).IsAssignableFrom(type);
		}

		public PassServiceConfiguration WithEvaluator(Func<Type, bool> evaluator)
		{
			TypeEvaluator = evaluator;
			return this;
		}
	}
}
