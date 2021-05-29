using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
	public abstract class AssignmentProblemBuilder<T> where T : AssignmentProblem
	{

		public abstract T Create(int[,] matrixC, int[,] matrixT);
		//public abstract Task<T> CreateAsync(string Path);

	}
}
