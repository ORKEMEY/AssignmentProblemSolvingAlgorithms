using System;

namespace Infrastructure
{

	public class AssignmentProblemResolver
	{

		public readonly IAssignmentProblemSolvingAlgorithm<AssignmentProblem> Algorythm;
		public readonly AssignmentProblem Problem;

		public AssignmentProblemResolver(IAssignmentProblemSolvingAlgorithm<AssignmentProblem> algorythm, AssignmentProblem problem)
		{
			this.Algorythm = algorythm;
			this.Problem = problem;
		}

		public void Resolve()
		{
			Algorythm.Resolve(Problem);
		}

	}

}
