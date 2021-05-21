using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
	public class SquareAssignmentProblem : AssignmentProblem
	{

		public override int[,] MatrixC
		{
			get => _matrixC;

			protected set
			{
				if (value.GetLength(0) == value.GetLength(1)
					& CheckConstraintsMatrixC(value))
					_matrixC = value;
			}
		}
		public override int[,] MatrixT
		{
			get => _matrixT;

			protected set
			{
				if (value.GetLength(0) == value.GetLength(1)
					& CheckConstraintsMatrixT(value))
					_matrixT = value;
			}
		}

		public SquareAssignmentProblem(int[,] matrixC, int[,] matrixT) : base(matrixC, matrixT)
		{
		}

	}
}
