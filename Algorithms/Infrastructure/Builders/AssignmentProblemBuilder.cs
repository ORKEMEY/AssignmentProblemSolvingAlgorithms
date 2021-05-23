using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
	public abstract class AssignmentProblemBuilder
	{

		public abstract AssignmentProblem Create(int[,] matrixC, int[,] matrixT);
		public abstract Task<AssignmentProblem> CreateAsync(string Path);

	}
}
