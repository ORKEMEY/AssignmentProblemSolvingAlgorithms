using System;
using Infrastructure;

namespace GeneticAlgorithm
{
	public abstract class GeneticAlgorithm<T> : IAssignmentProblemSolvingAlgorithm<T>
		where T : AssignmentProblem
	{

		public abstract int[] Resolve(T problem);

	}


}
