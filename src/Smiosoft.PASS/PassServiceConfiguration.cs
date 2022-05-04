using System;

namespace Smiosoft.PASS
{
	public class PassServiceConfiguration
	{
		internal Func<Type, bool> TypeEvaluator { get; private set; }

		public PassServiceConfiguration()
		{
			TypeEvaluator = (type) => true;
		}

		public PassServiceConfiguration WithEvaluator(Func<Type, bool> evaluator)
		{
			TypeEvaluator = evaluator;
			return this;
		}
	}
}
