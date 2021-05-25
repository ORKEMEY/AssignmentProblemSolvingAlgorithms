using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Extensions
{
	public static class IntMAtrixExtension
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
	}
}
