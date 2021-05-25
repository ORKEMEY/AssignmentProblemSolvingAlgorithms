using System;
using System.IO;
using GeneticAlgorithm;
using GreedyAlgorithm;
using Infrastructure;
using HungarianAlgorithm;

namespace Console
{
	class Program
	{
		static void Main(string[] args)
		{
			//read task
			var pathToMatrix = Path.GetFullPath(@"../../../Examples/matrix2.json");
			var probBuilder = new SquareAssignmentProblemBuilder();
			//create task obj
			var prob = probBuilder.CreateAsync(pathToMatrix).Result;
			//create algorithm obj
			var alg = new HungarianAlgorithm.HungarianAlgorithmForSquareProblem();
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
