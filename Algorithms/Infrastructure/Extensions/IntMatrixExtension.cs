using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Extensions
{
	public static class IntMatrixExtension
	{
		public static double[,] ToDouble(this int[,] array)
		{
			double[,] d_array = new double[array.GetLength(0), array.GetLength(1)];
			for (int row = 0; row < array.GetLength(0); row++)
			{
				for (int col = 0; col < array.GetLength(1); col++)
				{
					d_array[row, col] = Convert.ToDouble(array[row, col]);
				}
			}
			return d_array;
		}

		public static string MatrixToString(this int[,] matrix)
		{

			string matrixStr = String.Empty;

			matrixStr += $"{" |",4}";
			for (int col = 0; col < matrix.GetLength(1); col++)
				matrixStr += $"{(col + 1),-6}|";
			matrixStr += "\n----";
			for (int col = 0; col < matrix.GetLength(1); col++)
				matrixStr += $"-------";
			matrixStr += "\n";

			for (int row = 0; row < matrix.GetLength(0); row++)
			{
				matrixStr += $"{(row + 1),-3}|";

				for (int col = 0; col < matrix.GetLength(1); col++)
				{
					matrixStr += $"{matrix[row, col],-6}|";
				}
				matrixStr += "\n";
			}
			return matrixStr;
		}


		public static int[][] ToJaggedArray(this int[,] matrix)
		{
			if (matrix == null) return null;

			int[][] Result = new int[matrix.GetLength(0)][];

			for (int row = 0; row < Result.Length; row++)
			{
				Result[row] = new int[matrix.GetLength(1)];

				for (int col = 0; col < matrix.GetLength(1); col++)
				{
					Result[row][col] = matrix[row, col];
				}
			}
			return Result;
		}

	}

}
