using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Infrastructure.Extensions;

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
			MatrixC = matrixC.ToJaggedArray();
			MatrixT = matrixT.ToJaggedArray();
		}

	}
}
