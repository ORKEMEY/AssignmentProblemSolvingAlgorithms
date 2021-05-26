using System;
using Infrastructure;

namespace GreedyAlgorithm
{
	public class GreedyAlgorithm : IAssignmentProblemSolvingAlgorithm<AssignmentProblem>
	{

		private double[,] matrixF;

		public int[] Resolve(AssignmentProblem problem)
		{
			FillMatrixF(problem);

			int[] result = new int[matrixF.GetLength(0) <= matrixF.GetLength(1) ? matrixF.GetLength(0) : matrixF.GetLength(1)];

			for (int row = 0; row < result.Length; row++)
			{
				FindMax(matrixF, out int indexI, out int indexJ);
				result[indexI] = indexJ;
				CrossOutRowCol(indexI: indexI, indexJ: indexJ);

			}

			return result;
		}

		protected void CrossOutRowCol(int indexI, int indexJ)
		{

			for (int row = 0; row < matrixF.GetLength(0); row++)
			{
				matrixF[row, indexJ] = double.MinValue;
			}

			for (int col = 0; col < matrixF.GetLength(1); col++)
			{
				matrixF[indexI, col] = double.MinValue;
			}

		}

		protected void FindMax(double[,] matrix, out int indexI, out int indexJ)
		{

			double max = double.MinValue;
			indexI = indexJ = 0;

			for (int row = 0; row < matrix.GetLength(0); row++)
			{
				for (int col = 0; col < matrix.GetLength(1); col++)
				{

					if(max < matrix[row, col])
					{
						max = matrix[row, col];
						indexI = row;
						indexJ = col;
					}

				}

			}

		}

		protected bool AreMatrixCompatible(IResolvable problem)
		{

			if (problem.MatrixC.GetLength(0) != problem.MatrixT.GetLength(0))
				return false;

			if (problem.MatrixC.GetLength(1) != problem.MatrixT.GetLength(1))
				return false;
			return true;
		}

		protected void FillMatrixF(IResolvable problem)
		{

			if(!AreMatrixCompatible(problem)) return;

			matrixF = new double[
				problem.MatrixC.GetLength(0), 
				problem.MatrixC.GetLength(1)];

			for(int row  = 0; row < problem.MatrixC.GetLength(0); row++)
			{
				for (int col = 0; col < problem.MatrixC.GetLength(1); col++)
				{
					matrixF[row, col] = (double)problem.MatrixC[row, col] / problem.MatrixT[row, col];
				}
			}

		}

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
