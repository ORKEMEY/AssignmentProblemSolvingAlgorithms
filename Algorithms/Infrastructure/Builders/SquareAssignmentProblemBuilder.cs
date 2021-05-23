using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructure
{
	public class SquareAssignmentProblemBuilder : AssignmentProblemBuilder
	{

		public override AssignmentProblem Create(int[,] matrixC, int[,] matrixT)
		{
			return new SquareAssignmentProblem(matrixC: matrixC, matrixT: matrixT);
		}

		public async override Task<AssignmentProblem> CreateAsync(string Path)
		{
			AssignmentProblemDTO restoredProb;
			using (FileStream fs = new FileStream(Path, FileMode.OpenOrCreate))
			{
				restoredProb = await JsonSerializer.DeserializeAsync<AssignmentProblemDTO>(fs);
			}

			return new SquareAssignmentProblem(matrixC: restoredProb.GetMatrixC(), matrixT: restoredProb.GetMatrixT());
		}

	}
}
