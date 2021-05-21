using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
	public interface IAssignmentProblemSolvingAlgorithm<T> where T : IResolvable
	{
		public int[] Resolve(T problem);
	}

}
