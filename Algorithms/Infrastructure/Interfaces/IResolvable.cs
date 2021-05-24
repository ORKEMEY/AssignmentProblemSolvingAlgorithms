using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
	public interface IResolvable
	{

		int[,] MatrixC { get; }
		int[,] MatrixT { get; }

		bool CheckConstraintsMatrixC(int[,] matrix);
		bool CheckConstraintsMatrixT(int[,] matrix);

	}
}
