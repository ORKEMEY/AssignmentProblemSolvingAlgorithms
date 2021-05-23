using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
	/// <summary>
	/// Class builder for AssignmentProblem
	/// </summary>
	public abstract class AssignmentProblemBuilder
	{
		//fabric methods
		public abstract AssignmentProblem Create(int[,] matrixC, int[,] matrixT);
		public abstract Task<AssignmentProblem> CreateAsync(string Path);

	}
}
