using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Infrastructure
{
	public struct AssignmentProblemDTO
	{
		[JsonPropertyName("MatrixC")]
		public int[][] MatrixC { get; set; }
		[JsonPropertyName("MatrixT")]
		public int[][] MatrixT { get; set; }

		public AssignmentProblemDTO(int[][] matrixC, int[][] matrixT)
		{
			MatrixC = matrixC;
			MatrixT = matrixT;

		}

		public AssignmentProblemDTO(int[,] matrixC, int[,] matrixT)
		{
			MatrixC = MatrixT = MatrixT = null;
			SetMatrixC(matrixC);
			SetMatrixT(matrixT);
		}

		public void SetMatrixC(int[,] matrix)
		{
			MatrixC = new int[matrix.GetLength(0)][];

			for (int row = 0; row < MatrixC.Length; row++)
			{
				MatrixC[row] = new int[matrix.GetLength(1)];

				for (int col = 0; col < matrix.GetLength(1); col++)
				{
					MatrixC[row][col] = matrix[row, col];
				}
			}

		}
		public void SetMatrixT(int[,] matrix)
		{
			MatrixT = new int[matrix.GetLength(0)][];

			for (int row = 0; row < MatrixT.Length; row++)
			{
				MatrixT[row] = new int[matrix.GetLength(1)];

				for (int col = 0; col < matrix.GetLength(1); col++)
				{
					MatrixT[row][col] = matrix[row, col];
				}
			}
		}

		public int[,] GetMatrixC()
		{
			int[,] matrix = new int[MatrixC.Length, MatrixC[0].Length];

			for (int row = 0; row < matrix.GetLength(0); row++)
			{
				for (int col = 0; col < matrix.GetLength(1); col++)
				{
					matrix[row, col] = MatrixC[row][col];
				}
			}

			return matrix;
		}
		public int[,] GetMatrixT()
		{
			int[,] matrix = new int[MatrixT.Length, MatrixT[0].Length];

			for (int row = 0; row < matrix.GetLength(0); row++)
			{
				for (int col = 0; col < matrix.GetLength(1); col++)
				{
					matrix[row, col] = MatrixT[row][col];
				}
			}

			return matrix;
		}

	}
}
