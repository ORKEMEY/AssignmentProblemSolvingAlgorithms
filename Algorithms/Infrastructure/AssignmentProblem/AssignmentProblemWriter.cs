using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{

	public static class AssignmentProblemWriter
	{

		public static void Write(string Path, AssignmentProblem result)
		{
			var problemDTO = new AssignmentProblemDTO(result.MatrixC, result.MatrixT, result.MutationProbability.Value, result.GeneticAlgorithmsNumberOfIterations.Value);

			WriteAsync(Path, problemDTO);
		}



		/// <exception cref="ArgumentException"/>
		public static async void WriteAsync(string Path, AssignmentProblemDTO result)
		{
			FileStream fs = null;
			try
			{
				fs = new FileStream(Path, FileMode.Create);

				await JsonSerializer.SerializeAsync(fs, result);
			}
			catch (Exception e)
			{
				throw new ArgumentException(e.Message);
			}
			finally
			{
				fs?.Close();
			}
		}

	}


}
