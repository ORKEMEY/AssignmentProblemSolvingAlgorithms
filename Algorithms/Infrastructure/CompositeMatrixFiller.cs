using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
	public abstract class CompositeMatrixFiller<T> where T : AssignmentProblem
	{
		protected double[,] matrixF;

		protected void FillMatrixF(T problem)
		{

			matrixF = new double[
				problem.MatrixC.GetLength(0),
				problem.MatrixC.GetLength(1)];

			for (int row = 0; row < problem.MatrixC.GetLength(0); row++)
			{
				for (int col = 0; col < problem.MatrixC.GetLength(1); col++)
				{
					matrixF[row, col] = (double)problem.MatrixC[row, col] / problem.MatrixT[row, col];
				}
			}

		}
	}
}
