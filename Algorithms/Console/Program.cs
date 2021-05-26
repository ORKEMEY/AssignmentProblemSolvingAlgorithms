﻿using System;
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

			if (args.Length == 0)
			{
				ConsoleInterface.Alert($"\n < No argument specified. > ");
				ConsoleInterface.Info($" < Specify full path to .json file and try again! > ");
				return;
			}

			ConsoleInterface.GetSource()
							.Run(args[0]);

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