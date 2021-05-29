using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{

	public abstract class RandomAssignmentProblemBuilder<T> where T : AssignmentProblem
	{
		public readonly RandomAssignmentProblemBuilderOptions Options;

		protected RandomAssignmentProblemBuilder(RandomAssignmentProblemBuilderOptions options)
		{
			this.Options = options;
		}

		public abstract T Create();
	}
}
