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
