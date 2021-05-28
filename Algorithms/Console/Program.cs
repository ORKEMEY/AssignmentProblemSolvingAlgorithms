using System;
using Infrastructure;
using GeneticAlgorithm;
using System.IO;

namespace Console
{
	class Program
	{
		static void Main(string[] args)
		{
			//read task
			var pathToMatrix = Path.GetFullPath(@"../../../Examples/matrix.json");
			var probBuilder = new SquareAssignmentProblemBuilder();
			//create task obj
			var prob = probBuilder.CreateAsync(pathToMatrix).Result;
			//create algorithm obj
			var alg = new GeneticAlgorithm.GeneticAlgorithmForSquareProblem();
			// create aggregator class
			var res = new AssignmentProblemResolver<SquareAssignmentProblem>(alg, prob);
			//start algirithm
			res.Resolve();
			//output result
			System.Console.WriteLine(res.ToString());

			System.Console.ReadKey();
		}
	}
}
