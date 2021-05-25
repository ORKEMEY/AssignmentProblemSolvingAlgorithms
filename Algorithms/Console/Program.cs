using System;
using System.IO;
using System.Text.Json;
using Infrastructure;
using GreedyAlgorithm;

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
			var alg = new GreedyAlgorithm.GreedyAlgorithm();
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



//Задача про таски 
/*
матрица С
{ {5, 1,   8,   8,   0,   2,   5,   3,   1,   2},
{ 8, 4,   4,   1,   5,   5,   9,   4,   4,   3},
{ 3, 4,   5,  5,   6,   8,   0,   8,   3,   5},
{ 6, 1,   7,  3,   5,   1,   5,   3,   1,   9},
{ 2, 8,   9,  0,   4,   5,   6,   3,   8,   4},
{ 4, 3,   6,  1,   7,   3,   5,   5,   3,   4},
{ 3, 2,   2,  8,   9,   0,   4,   3,   6,   1},
{ 2, 7,   1,  9,   5,   2,   5,   3,   8,   4},
{ 1, 0 ,  4,  3,   7,   5,   9,   4,   4,   7},
{ 0, 9,   5,  1,   8,   8,   0,   8,   3,   3}}

матрица Т
{{ 6,   9,   3,   1,   7,   8,   3,   2,   3,   1 },
{ 6,   5,   1,   8,   8,   1,   7,   5,   2,   3 },
{ 5,   8,   4,   4,   1,   5,   6,   9,   5,   6},
{ 8,   3,   4,   5,   5,   6,   4,   5,   2,   7},
{ 3,   6,   1,   7,   3,   5,   9,   2,   2,   5},
{ 8,   1,   7,   5,   2,   4,   2,   9,   4,   1},
{ 1,   5,   6,   9,   5,   1,   5,   2,   8,   6},
{ 5,   6,   4,   5,   2,   5,   6,   2,   9,   3},
{ 3,   5,   9,   2,   2,   1,   6,   5,   7,   5},
{ 1,   4,  2,   9,   4,   2,   5,   6,   4,  5,}}

*/