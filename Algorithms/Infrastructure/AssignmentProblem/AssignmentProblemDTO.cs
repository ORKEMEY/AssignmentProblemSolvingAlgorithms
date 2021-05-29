using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Infrastructure.Extensions;

namespace Infrastructure
{
	/// <summary>
	/// Data transfer object for reading tasks from files
	/// </summary>
	public struct AssignmentProblemDTO
	{
		[JsonPropertyName("MutationProbability")]
		public double? MutationProbability { get; set; }
		[JsonPropertyName("GeneticAlgorithmsNumberOfIterations")]
		public int? GeneticAlgorithmsNumberOfIterations { get; set; }
		[JsonPropertyName("MatrixC")]
		public int[][] MatrixC { get; set; }
		[JsonPropertyName("MatrixT")]
		public int[][] MatrixT { get; set; }	

		public AssignmentProblemDTO(int[][] matrixC, int[][] matrixT, double mutationProbability, int geneticAlgorithmsNumberOfIterations)
		{
			MatrixC = matrixC;
			MatrixT = matrixT;
			MutationProbability = mutationProbability;
			GeneticAlgorithmsNumberOfIterations = geneticAlgorithmsNumberOfIterations;
		}

		public AssignmentProblemDTO(int[,] matrixC, int[,] matrixT, double mutationProbability, int geneticAlgorithmsNumberOfIterations)
		{
			MatrixC = MatrixT = MatrixT = null;
			MatrixC = matrixC.ToJaggedArray();
			MatrixT = matrixT.ToJaggedArray();
			MutationProbability = mutationProbability;
			GeneticAlgorithmsNumberOfIterations = geneticAlgorithmsNumberOfIterations;
		}

	}
}
