using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;
using Infrastructure.Extensions;

namespace Infrastructure
{
	/// <summary>
	/// Class builder for SquareAssignmentProblem
	/// </summary>
	/// <remarks>Implementation of factory pattern</remarks>
	public class SquareAssignmentProblemBuilder : AssignmentProblemBuilder<SquareAssignmentProblem>
	{

		public override SquareAssignmentProblem Create(int[,] matrixC, int[,] matrixT)
		{
			return new SquareAssignmentProblem(matrixC: matrixC, matrixT: matrixT);
		}
		/// <summary>
		/// Reads task from file
		/// </summary>
		/// <param name="Path"></param>
		/// <exception cref="FileNotFoundException"></exception>
		/// <returns></returns>
		public async Task<SquareAssignmentProblem> CreateAsync(string Path)
		{
			AssignmentProblemDTO restoredProb;

			if (!File.Exists(Path)) throw new FileNotFoundException("Not existing file specified", Path);
			using (FileStream fs = new FileStream(Path, FileMode.OpenOrCreate))
			{
				restoredProb = await JsonSerializer.DeserializeAsync<AssignmentProblemDTO>(fs);
			}

			if (restoredProb.MatrixC == null || restoredProb.MatrixT == null) 
				throw new FormatException("Wrong format of file (make sure properties are named correctly)");

			int[,] matrixC, matrixT;
			try
			{
				matrixC = restoredProb.MatrixC.ToTwoDimArray();
			}
			catch (ArgumentException ex) when (ex.ParamName == null)
			{
				throw new ArgumentException(ex.Message, nameof(restoredProb.MatrixC));
			}
			try
			{
				matrixT = restoredProb.MatrixT.ToTwoDimArray();
			}
			catch (ArgumentException ex) when (ex.ParamName == null)
			{
				throw new ArgumentException(ex.Message, nameof(restoredProb.MatrixT));
			}
		
			if(!restoredProb.MutationProbability.HasValue || !restoredProb.GeneticAlgorithmsNumberOfIterations.HasValue)
			{
				return new SquareAssignmentProblem(matrixC: matrixC, matrixT: matrixT);
			}
			else
			{
				return new SquareAssignmentProblem(matrixC: matrixC, matrixT: matrixT, 
					restoredProb.MutationProbability.Value, restoredProb.GeneticAlgorithmsNumberOfIterations.Value);
			}

		}

	}
}
