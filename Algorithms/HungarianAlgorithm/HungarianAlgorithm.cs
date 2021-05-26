using System;
using Infrastructure;

namespace HungarianAlgorithm
{
	/// <typeparam name="T">Specific type of assignment problem</typeparam>
	public abstract class HungarianAlgorithm<T> : CompositeMatrixFiller<T>, IAssignmentProblemSolvingAlgorithm<T>
		where T : AssignmentProblem
	{
		
		public abstract int[] Resolve(T problem);

		public double CalculateObjective(double[,] matrix, int[] assignment)
		{
			double result = 0;

			for (int count = 0; count < assignment.Length; count++)
			{
				result += matrix[count, assignment[count]];
			}

			return result;
		}
	}

}
