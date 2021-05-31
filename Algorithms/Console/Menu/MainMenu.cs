using System;
using System.IO;
using Infrastructure;
using Tests;
using GeneticAlgorithm;
using GreedyAlgorithm;
using HungarianAlgorithm;

namespace Console.Menu
{

	public class MainMenu : Menu
	{
		private AssignmentProblem problem;

		public MainMenu(AssignmentProblem problem)
		{
			this.problem = problem;
		}

		public override void ShowMenu()
		{
			System.Console.WriteLine(" < Menu > ");
			System.Console.WriteLine(" < Enter \"o\" to output menu > ");
			System.Console.WriteLine(" < Enter \"r\" to read assignment problem from file > ");
			System.Console.WriteLine(" < Enter \"m\" to output current assignment problem > ");
			System.Console.WriteLine(" < Enter \"s\" to open result submenu > ");
			System.Console.WriteLine(" < Enter \"a\" to open assignment problem submenu > ");
			System.Console.WriteLine(" < Enter \"t\" to run test > ");
			System.Console.WriteLine(" < Enter \"h\" to solve problem with hungarian algorithm > ");
			System.Console.WriteLine(" < Enter \"g\" to solve problem with greedy algorithm > ");
			System.Console.WriteLine(" < Enter \"e\" to solve problem with genetic algorithm > ");
			System.Console.WriteLine(" < Enter \"q\" to quit > ");
			System.Console.WriteLine();
		}

		public override void RunMenu()
		{
			if (problem == null) ReadProblemFromFile();

			ShowMenu();
			char mode;
			AssignmentProblemResolver<SquareAssignmentProblem> currentResolver = null;

			do
			{
				mode = EnterMode();
				switch (mode)
				{

					case 'o':
						ShowMenu();
						break;

					case 'm':
						System.Console.WriteLine(problem.ToString());
						break;

					case 's':
						if (currentResolver == null)
						{
							Info(" < No current result. Run one of the algorithms first and try again! > ");
							break;
						}
						var subMenu = new ResultSubMenu(currentResolver);
						subMenu.RunMenu();
						ShowMenu();
						break;

					case 'h':
						RunHungarianAlgorithm(ref currentResolver);
						break;

					case 'g':
						RunGreedyAlgorithm(ref currentResolver);
						break;

					case 'e':
						RunGeneticAlgorithm(ref currentResolver);
						break;

					case 't':
						new TestSubMenu().RunMenu();
						ShowMenu();
						break;

					case 'r':
						ReadProblemFromFile();
						break;

					case 'a':
						var subMenuRP = new RandomProblemMenu(problem);
						subMenuRP.RunMenu();
						problem = subMenuRP.currentProblem;
						ShowMenu();
						break;

					case 'q':
						break;

					default:
						Alert(" < Wrong mode > ");
						break;
				}

			} while (mode != 'q');

		}

		private void RunGeneticAlgorithm(ref AssignmentProblemResolver<SquareAssignmentProblem> currentResolver)
		{
			//create algorithm obj
			var algGen = new GeneticAlgorithm.GeneticAlgorithmForSquareProblem();
			// create aggregator class
			currentResolver = new AssignmentProblemResolver<SquareAssignmentProblem>(algGen, problem as SquareAssignmentProblem);
			//start algirithm
			try
			{
				currentResolver.Resolve();
			}
			catch (ArgumentException e)
			{
				Alert($"Error: {e.Message}");
			}
			//output result
			System.Console.WriteLine(currentResolver.ToString());
		}

		private void RunGreedyAlgorithm(ref AssignmentProblemResolver<SquareAssignmentProblem> currentResolver)
		{
			//create algorithm obj
			var algG = new GreedyAlgorithm.GreedyAlgorithm();
			// create aggregator class
			currentResolver = new AssignmentProblemResolver<SquareAssignmentProblem>(algG, problem as SquareAssignmentProblem);
			//start algirithm
			currentResolver.Resolve();
			//output result
			System.Console.WriteLine(currentResolver.ToString());
		}

		private void RunHungarianAlgorithm(ref AssignmentProblemResolver<SquareAssignmentProblem> currentResolver)
		{
			//create algorithm obj
			var alg = new HungarianAlgorithm.HungarianAlgorithmForSquareProblem();
			// create aggregator class
			currentResolver = new AssignmentProblemResolver<SquareAssignmentProblem>(alg, problem as SquareAssignmentProblem);
			//start algirithm
			currentResolver.Resolve();
			//output result
			System.Console.WriteLine(currentResolver.ToString());
		}

		private static SquareAssignmentProblem CreateSquareAssignmentProblemInstance(string pathToFileArg)
		{
			//read task
			var pathToMatrix = Path.GetFullPath(pathToFileArg);
			var probBuilder = new SquareAssignmentProblemBuilder();
			SquareAssignmentProblem prob = null;
			//create task obj
			try
			{
				prob = probBuilder.CreateAsync(pathToMatrix).Result;
			}
			catch (AggregateException e)
			{
				e.Handle(ex => {
					if (ex is FileNotFoundException)
					{
						Alert($"Error: {ex.Message} : {(ex as FileNotFoundException).FileName}");
						return true;
					}

					if (ex is ArgumentException)
					{
						if ((ex as ArgumentException).ParamName != null)
							Alert($"Error: {ex.Message} : {(ex as ArgumentException).ParamName}");
						else
							Alert($"Error: {ex.Message}");
						return true;
					}

					if (ex is FormatException)
					{
						Alert($"Error: {ex.Message}");
						return true;
					}
					return false;
				});

				return null;
			}

			return prob;
		}

		private string GetPath()
		{
			System.Console.Write("Enter path to the input file\n>");
			string path = String.Empty;

			do
			{
				path = System.Console.ReadLine();
				if (String.IsNullOrEmpty(path))
				{
					Alert(" < Empty path to the input file > ");
					Info(" < Try again! > ");
				}
			} while (String.IsNullOrEmpty(path));

			return path;
		}
		private void ReadProblemFromFile()
		{
			do
			{
				problem = CreateSquareAssignmentProblemInstance(GetPath());
			}
			while (problem == null);
			Success(" < File has been successfully read > ");
		}
	}

}

