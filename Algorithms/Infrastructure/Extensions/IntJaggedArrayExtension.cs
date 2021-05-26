using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Extensions
{
	public static class IntJaggedArrayExtension
	{
		public static int[,] ToTwoDimArray(this int[][] jaggedMatrix)
		{
			if (jaggedMatrix == null) return null;

			int[,] matrix = new int[jaggedMatrix.Length, jaggedMatrix[0].Length];

			for (int row = 0; row < matrix.GetLength(0); row++)
			{
				if (matrix.GetLength(1) != jaggedMatrix[row].Length)
					throw new ArgumentException("Matrix doesn't have the same numer of elements in each row and column");

				for (int col = 0; col < matrix.GetLength(1); col++)
				{

					matrix[row, col] = jaggedMatrix[row][col];
				}
			}

			return matrix;
		}

	}
}
