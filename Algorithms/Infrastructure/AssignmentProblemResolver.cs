using System;
using Infrastructure.Extensions;

namespace Infrastructure
{
	/// <summary>
	/// Class aggregator for algorithm, task & result
	/// </summary>
	 
	public class AssignmentProblemResolver<T> where T : AssignmentProblem
	{

		public readonly IAssignmentProblemSolvingAlgorithm<T> Algorythm;
		public readonly T Problem;
		public int[] Result { get; protected set; }
		public double? ObjectiveValueByC { get; protected set; }
		public double? ObjectiveValueByT { get; protected set; }


		public AssignmentProblemResolver(IAssignmentProblemSolvingAlgorithm<T> algorythm, T problem)
		{
			this.Algorythm = algorythm;
			this.Problem = problem;
			ObjectiveValueByC = null;
			ObjectiveValueByT = null;
		}

		public void Resolve()
		{
			Result = Algorythm.Resolve(Problem);

			if (Result == null) return;

			ObjectiveValueByC = Problem.CalculateObjective(Problem.MatrixC.ToDouble(), Result);
			ObjectiveValueByT = Problem.CalculateObjective(Problem.MatrixT.ToDouble(), Result);

		}

		public override string ToString()
		{
			if (Result == null) return "Result isn't feasible";
	
			string str = String.Empty;
			foreach(var i in Result)
			{
				str += (i + 1).ToString() + " ";
			}
			str += $"\nObjective value calculated by matrix C: {ObjectiveValueByC:F3}";
			str += $"\nObjective value calculated by matrix T: {ObjectiveValueByT:F3}";
			return str;
		}

		public string ToStringAsMatrix()
		{
			if (Result == null) return "Result isn't feasible";

			string matrixStr = String.Empty;

			matrixStr += $"{" |",4}";
			for (int col = 0; col < Problem.MatrixC.GetLength(1); col++)
					matrixStr += $"{(col + 1),-6}|";
			matrixStr += "\n----";
			for (int col = 0; col < Problem.MatrixC.GetLength(1); col++)
				matrixStr += $"-------";
			matrixStr += "\n";

			for (int row = 0; row < Problem.MatrixC.GetLength(0); row++)
			{
				matrixStr += $"{(row + 1),-3}|";

				for (int col = 0; col < Problem.MatrixC.GetLength(1) ; col++)
				{
					matrixStr += $"{(Result[row] == col ? 1 : 0),-6}|";
				}
				matrixStr += "\n";
			}
			
			matrixStr += $"\nObjective value calculated by matrix C: {ObjectiveValueByC:F3}";
			matrixStr += $"\nObjective value calculated by matrix T: {ObjectiveValueByT:F3}";
			return matrixStr;
		}

		private ResultDTO GetResult()
		{

			int[,] res = new int[Problem.MatrixC.GetLength(0), Problem.MatrixC.GetLength(1)];

			for (int row = 0; row < Problem.MatrixC.GetLength(0); row++)
			{
				for (int col = 0; col < Problem.MatrixC.GetLength(1); col++)
				{
					res[row, col] = (Result[row] == col ? 1 : 0);
				}
				
			}

			return new ResultDTO(res, objectiveValueByT: ObjectiveValueByT.Value, objectiveValueByC : ObjectiveValueByC.Value);
		}

		public void SaveResult(string path)
		{
			if (Result == null) return;

			if (!path.EndsWith(".json")) path += ".json";

			ResultWriter.WriteAsync(path, GetResult());
		}

	}

}
