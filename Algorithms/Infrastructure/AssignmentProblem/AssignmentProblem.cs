using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Extensions;

namespace Infrastructure
{

	public abstract class AssignmentProblem : IResolvable
	{

		private int[,] _matrixC;
		private int[,] _matrixT;

		public virtual int[,] MatrixC
		{
			get => _matrixC;

			protected set
			{
				if (!CheckConstraintsMatrixC(value))
					throw new ArgumentException("Matrix has to consist only of positive number and null", nameof(MatrixC));

				_matrixC = value;
			}
		}
		public virtual int[,] MatrixT
		{
			get => _matrixT;

			protected set
			{
				if (!CheckConstraintsMatrixC(value))
					throw new ArgumentException("Matrix has to consist only of positive number", nameof(MatrixT));

				_matrixT = value;
			}
		}

		public double? MutationProbability { get; protected set; }
		public int? GeneticAlgorithmsNumberOfIterations{ get; protected set; }

		public AssignmentProblem(int[,] matrixC, int[,] matrixT, double mutationProbability, int geneticAlgorithmsNumberOfIterations) 
			: this(matrixC, matrixT)
		{
			MutationProbability = mutationProbability;
			GeneticAlgorithmsNumberOfIterations = geneticAlgorithmsNumberOfIterations;
		}

		public AssignmentProblem(int[,] matrixC, int[,] matrixT)
		{
			if (!AreMatrixCompatible(matrixC: matrixC, matrixT: matrixT))
				throw new ArgumentException("Parameters aren't cpmpetible", $"{nameof(matrixC)}; {nameof(matrixT)};");
			this.MatrixC = matrixC;
			this.MatrixT = matrixT;
		}


		public abstract double CalculateObjective(double[,] matrix, int[] assignment);
		private bool AreMatrixCompatible(int[,] matrixC, int[,] matrixT)
		{
			if (matrixC.GetLength(0) != matrixT.GetLength(0))
				return false;

			if (matrixC.GetLength(1) != matrixT.GetLength(1))
				return false;
			return true;
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

		public override string ToString()
		{
			string matrixStr = String.Empty;

			matrixStr += $"Matrix C: \n{MatrixC.MatrixToString()}\n";
			matrixStr += $"Matrix T: \n{MatrixT.MatrixToString()}\n";

			return matrixStr;
		}


		public void SaveProblem(string path)
		{
			if (!path.EndsWith(".json")) path += ".json";
			AssignmentProblemWriter.Write(path, this);
		}

	}

}
