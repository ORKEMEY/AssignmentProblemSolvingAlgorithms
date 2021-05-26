using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
	public interface IAssignmentProblemSolvingAlgorithm<in T> where T : IResolvable
	{
		int[] Resolve(T problem);
		double CalculateObjective(double[,] matrix, int[] assignment);
	}

}
