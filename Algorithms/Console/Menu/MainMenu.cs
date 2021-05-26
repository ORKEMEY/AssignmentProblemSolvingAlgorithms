using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure;
using GeneticAlgorithm;
using GreedyAlgorithm;
using HungarianAlgorithm;

namespace Console.Menu
{

	public class MainMenu : Menu
	{
		private readonly AssignmentProblem problem;

		public MainMenu(AssignmentProblem problem)
		{
			this.problem = problem;
		}

		public override void ShowMenu()
		{
			System.Console.WriteLine(" < Menu > ");
			System.Console.WriteLine(" < Enter \"o\" to output menu > ");
			System.Console.WriteLine(" < Enter \"m\" to output matrices > ");
			System.Console.WriteLine(" < Enter \"s\" to open result submenu> ");
			System.Console.WriteLine(" < Enter \"h\" to solve problem with hungarian algorithm > ");
			System.Console.WriteLine(" < Enter \"g\" to solve problem with greedy algorithm > ");
			System.Console.WriteLine(" < Enter \"t\" to solve problem with genetic algorithm > ");
			System.Console.WriteLine(" < Enter \"q\" to quit > ");
			System.Console.WriteLine();
		}

		public override void RunMenu()
		{
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
						var sunbMenu = new ResultSubMenu(currentResolver);
						sunbMenu.RunMenu();
						break;

					case 'h':
						//create algorithm obj
						var alg = new HungarianAlgorithm.HungarianAlgorithmForSquareProblem();
						// create aggregator class
						currentResolver = new AssignmentProblemResolver<SquareAssignmentProblem>(alg, problem as SquareAssignmentProblem);
						//start algirithm
						currentResolver.Resolve();
						//output result
						System.Console.WriteLine(currentResolver.ToString());
						break;

					case 'g':
						//create algorithm obj
						var algG = new GreedyAlgorithm.GreedyAlgorithm();
						// create aggregator class
						currentResolver = new AssignmentProblemResolver<SquareAssignmentProblem>(algG, problem as SquareAssignmentProblem);
						//start algirithm
						currentResolver.Resolve();
						//output result
						System.Console.WriteLine(currentResolver.ToString());
						break;

					case 't':
						Info(" < Not implemented > ");
						break;

					case 'q':
						break;

					default:
						WriteColorLine(" < Wrong mode > ", ConsoleColor.Red);
						break;
				}

			} while (mode != 'q');

		}

	}
}
