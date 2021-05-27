using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Infrastructure;

namespace GeneticAlgorithm
{

	public class GeneticAlgorithmForSquareProblem : GeneticAlgorithm<SquareAssignmentProblem>
	{

		public Population Population { get; protected set; }

		public override int[] Resolve(SquareAssignmentProblem problem)
		{
			int[] result = new int[problem.Size];
			Population = new Population(10, problem.Size);

			throw new NotImplementedException();

			return result;
		}

	}

}
