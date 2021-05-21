using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{

	public abstract class AssignmentProblem : IResolvable
	{

		protected int[,] _matrixC;
		protected int[,] _matrixT;

		public virtual int[,] MatrixC
		{
			get => _matrixC;

			protected set
			{
				if (CheckConstraintsMatrixC(value))
					_matrixC = value;
			}
		}
		public virtual int[,] MatrixT
		{
			get => _matrixT;

			protected set
			{
				if (CheckConstraintsMatrixT(value))
					_matrixT = value;
			}
		}

		public AssignmentProblem(int[,] matrixC, int[,] matrixT)
		{
			this.MatrixC = matrixC;
			this.MatrixT = matrixT;
		}

		public bool CheckConstraintsMatrixC(int[,] matrix)
		{
			foreach (var elem in matrix)
			{
				if (elem < 0) return false;
			}

			return true;
		}
		public bool CheckConstraintsMatrixT(int[,] matrix)
		{
			foreach (var elem in matrix)
			{
				if (elem <= 0) return false;
			}

			return true;
		}
	}

}
