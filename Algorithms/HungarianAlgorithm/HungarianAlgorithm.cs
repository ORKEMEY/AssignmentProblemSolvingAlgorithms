using System;
using Infrastructure;

namespace HungarianAlgorithm
{
	/// <typeparam name="T">Specific type of assignment problem</typeparam>
	public abstract class HungarianAlgorithm<T> : CompositeMatrixFiller<T>, IAssignmentProblemSolvingAlgorithm<T>
		where T : AssignmentProblem
	{
		
		public abstract int[] Resolve(T problem);

	}

}
