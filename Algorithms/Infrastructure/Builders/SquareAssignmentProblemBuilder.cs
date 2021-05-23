using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructure
{
	/// <summary>
	/// Class builder for SquareAssignmentProblem
	/// </summary>
	/// <remarks>Implementation of fabrik pattern</remarks>
	public class SquareAssignmentProblemBuilder : AssignmentProblemBuilder
	{

		public override AssignmentProblem Create(int[,] matrixC, int[,] matrixT)
		{
			return new SquareAssignmentProblem(matrixC: matrixC, matrixT: matrixT);
		}

		/// <summary>
		/// Reads task from file
		/// </summary>
		/// <param name="Path"></param>
		/// <exception cref="FileNotFoundException"></exception>
		/// <returns></returns>
		public async override Task<AssignmentProblem> CreateAsync(string Path)
		{
			AssignmentProblemDTO restoredProb;

			if (File.Exists(Path)) throw new FileNotFoundException();
			using (FileStream fs = new FileStream(Path, FileMode.OpenOrCreate))
			{
				restoredProb = await JsonSerializer.DeserializeAsync<AssignmentProblemDTO>(fs);
			}

			return new SquareAssignmentProblem(matrixC: restoredProb.GetMatrixC(), matrixT: restoredProb.GetMatrixT());
		}

	}
}
