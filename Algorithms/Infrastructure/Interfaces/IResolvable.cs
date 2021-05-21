using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
	public interface IResolvable
	{

		public int[,] MatrixC { get; }
		public int[,] MatrixT { get; }

		public bool CheckConstraintsMatrixC(int[,] matrix);
		public bool CheckConstraintsMatrixT(int[,] matrix);

	}
}
