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
		private AssignmentProblem problem;

		public MainMenu(AssignmentProblem problem)
		{
			this.problem = problem;
		}

		public override void ShowMenu()
		{
			System.Console.WriteLine(" < Menu > ");
			System.Console.WriteLine(" < Enter \"o\" to output menu > ");
			System.Console.WriteLine(" < Enter \"m\" to output current assignment problem > ");
			System.Console.WriteLine(" < Enter \"s\" to open result submenu> ");
			System.Console.WriteLine(" < Enter \"h\" to solve problem with hungarian algorithm > ");
			System.Console.WriteLine(" < Enter \"g\" to solve problem with greedy algorithm > ");
			System.Console.WriteLine(" < Enter \"t\" to solve problem with genetic algorithm > ");
			System.Console.WriteLine(" < Enter \"r\" to open assignment problem submenu > ");
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
						var subMenu = new ResultSubMenu(currentResolver);
						subMenu.RunMenu();
						break;

					case 'h':
						RunHungarianAlgorithm(ref currentResolver);
						break;

					case 'g':
						RunGreedyAlgorithm(ref currentResolver);
						break;

					case 't':
						RunGeneticAlgorithm(ref currentResolver);
						break;

					case 'r':
						var subMenuRP = new RandomMProblemMenu(problem);
						subMenuRP.RunMenu();
						problem = subMenuRP.currentProblem;
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
	}
}
