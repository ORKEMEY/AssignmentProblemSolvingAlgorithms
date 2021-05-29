using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
	public class RandomSquareAssignmentProblemBuilder : RandomAssignmentProblemBuilder<SquareAssignmentProblem>
	{

		public RandomSquareAssignmentProblemBuilder(RandomAssignmentProblemBuilderOptions options) : base(options)
		{
			if(options.NumberOfWorkers != options.NumberOfTasks)
			{
				throw new ArgumentException("Number of workers and number of tasks must be equal");
			}
		}

		public override SquareAssignmentProblem Create()
		{
			var matrixC = GenerateMatrixC();
			var matrixT = GenerateMatrixT();

			if (!Options.MutationProbability.HasValue || !Options.GeneticAlgorithmsNumberOfIterations.HasValue)
			{
				return new SquareAssignmentProblem(matrixC: matrixC, matrixT: matrixT);
			}
			else
			{
				return new SquareAssignmentProblem(matrixC: matrixC, matrixT: matrixT,
					Options.MutationProbability.Value, Options.GeneticAlgorithmsNumberOfIterations.Value);
			}
		}

		private int[,] GenerateMatrixC()
		{
			int[,] matrixC = new int[Options.NumberOfTasks, Options.NumberOfWorkers];

			var random = new Random();
			int lowBound = Options.ExpectedValC - Options.HalfIntervalC;
			int highBound = Options.ExpectedValC + Options.HalfIntervalC;

			for (int row =0; row < matrixC.GetLength(0); row++)
			{
				for (int col = 0; col < matrixC.GetLength(1); col++)
				{
					matrixC[row, col] = random.Next(lowBound, highBound);
				}

			}
			return matrixC;
		}

		private int[,] GenerateMatrixT()
		{
			int[,] matrixT = new int[Options.NumberOfTasks, Options.NumberOfWorkers];

			var random = new Random();
			int lowBound = Options.ExpectedValT - Options.HalfIntervalT;
			int highBound = Options.ExpectedValT + Options.HalfIntervalT;

			for (int row = 0; row < matrixT.GetLength(0); row++)
			{
				for (int col = 0; col < matrixT.GetLength(1); col++)
				{
					matrixT[row, col] = random.Next(lowBound, highBound);
				}

			}
			return matrixT;
		}


	}
}
