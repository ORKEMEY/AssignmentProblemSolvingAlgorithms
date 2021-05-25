using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
	/// <inheritdoc/>
	/// <remarks>Assignment problem with additional constraints on squareness of matrix</remarks>>
	public class SquareAssignmentProblem : AssignmentProblem
	{
		public int Size => MatrixT.GetLength(0); 
		public override int[,] MatrixC
		{
			get => base.MatrixC;

			protected set
			{
				if (value.GetLength(0) != value.GetLength(1))
					throw new ArgumentException("Matrix isn't square", nameof(MatrixC));

				base.MatrixC = value;
			}
		}
		public override int[,] MatrixT
		{
			get => base.MatrixT;

			protected set
			{
				if (value.GetLength(0) != value.GetLength(1))
					throw new ArgumentException("Matrix isn't square", nameof(MatrixT));

				base.MatrixT = value;
			}
		}

		public SquareAssignmentProblem(int[,] matrixC, int[,] matrixT) : base(matrixC, matrixT)
		{
		}

	}
}
