using System;
using Infrastructure;

namespace GreedyAlgorithm
{
	/// <summary>
	/// Implementation of greedy algorithm
	/// </summary>
	public sealed class GreedyAlgorithm : CompositeMatrixFiller<AssignmentProblem>, IAssignmentProblemSolvingAlgorithm<AssignmentProblem>
	{

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
		//Deletion of rows and columns which cann't be used in search for best assignment
		private void CrossOutRowCol(int indexI, int indexJ)
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
		//Search for best assignment on current step
		private void FindMax(double[,] matrix, out int indexI, out int indexJ)
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

	}
}
