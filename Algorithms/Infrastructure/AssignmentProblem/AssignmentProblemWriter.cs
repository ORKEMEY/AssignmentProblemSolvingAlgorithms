using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{

	public static class AssignmentProblemWriter
	{

		public static void Write(string Path, AssignmentProblem result, ProblemResolvedEventArgs metrics, string testOptions)
		{
			var problemDTO = new TestResultsDTO(result.MutationProbability,
				result.GeneticAlgorithmsNumberOfIterations, metrics.TimeOfWork, metrics.GetRelativeDistanceInPercent, testOptions);

			WriteAsync(Path, problemDTO);

		}

		/// <exception cref="ArgumentException"/>
		public static async void WriteAsync(string Path, TestResultsDTO result)
		{
			FileStream fs = null;
			try
			{
				fs = new FileStream(Path, FileMode.Append);
				var op = new JsonSerializerOptions()
				{
					WriteIndented = true
				};

				await JsonSerializer.SerializeAsync(fs, result, op);
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

		public static void Write(string Path, AssignmentProblem result)
		{
			var problemDTO = new AssignmentProblemDTO(result.MatrixC, result.MatrixT, result.MutationProbability, result.GeneticAlgorithmsNumberOfIterations);

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
