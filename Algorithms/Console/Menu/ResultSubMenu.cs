using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Infrastructure;

namespace Console.Menu
{
	public class ResultSubMenu : Menu
	{
		private readonly AssignmentProblemResolver<SquareAssignmentProblem> currentResolver;

		public ResultSubMenu(AssignmentProblemResolver<SquareAssignmentProblem> resolver)
		{
			currentResolver = resolver;
		}

		public override void ShowMenu()
		{
			System.Console.WriteLine(" < Result submenu > ");
			System.Console.WriteLine(" < Enter \"o\" to output menu > ");
			System.Console.WriteLine(" < Enter \"m\" to output current result as matrix > ");
			System.Console.WriteLine(" < Enter \"v\" to output current result as vector > ");
			System.Console.WriteLine(" < Enter \"s\" to save current result in file > ");
			System.Console.WriteLine(" < Enter \"q\" to quit menu > ");
			System.Console.WriteLine();

		}

		public override void RunMenu()
		{
			if (currentResolver == null)
			{
				Info(" < No current result. Run one of the algorithms first and try again! > ");
				return;
			}

			ShowMenu();
			char mode;
			do
			{
				mode = EnterMode();
				switch (mode)
				{

					case 'o':
						ShowMenu();
						break;

					case 's':
						SaveResult();
						break;

					case 'm':		
						System.Console.WriteLine(currentResolver.ToStringAsMatrix());
						break;

					case 'v':
						System.Console.WriteLine(currentResolver.ToString());
						break;

					case 'q':
						break;

					default:
						WriteColorLine(" < Wrong mode > ", ConsoleColor.Red);
						break;
				}

			} while (mode != 'q');
		}

		private void SaveResult()
		{
			System.Console.Write("Enter path to the output file\n>");
			string path = System.Console.ReadLine();
			if (path == null)
			{
				Alert(" < Empty path to the output file > ");
				return;
			}
			try
			{
				currentResolver.SaveResult(path);
				Success($"Result has been successfully saved: {Path.GetFullPath(path)}");
			}
			catch (ArgumentException e)
			{
				Alert($"Error: {e.Message}");
			}
		}

	}

	public class RandomMProblemMenu : Menu
	{

		public AssignmentProblem currentProblem { get; protected set; }

		public RandomMProblemMenu(AssignmentProblem problem)
		{
			currentProblem = problem;
		}

		public override void ShowMenu()
		{
			System.Console.WriteLine(" < Matrix assignment problem submenu > ");
			System.Console.WriteLine(" < Enter \"o\" to output menu > ");
			System.Console.WriteLine(" < Enter \"g\" to generate random problem > ");
			System.Console.WriteLine(" < Enter \"m\" to output current assignment problem > ");
			System.Console.WriteLine(" < Enter \"s\" to save current assignment problem to file > ");
			System.Console.WriteLine(" < Enter \"q\" to quit menu > ");
			System.Console.WriteLine();

		}

		public override void RunMenu()
		{

			ShowMenu();
			char mode;
			do
			{
				mode = EnterMode();
				switch (mode)
				{

					case 'o':
						ShowMenu();
						break;

					case 'm':
						if (currentProblem != null)
							System.Console.WriteLine(currentProblem.ToString());
						else
							Info(" < No current problem > ");
						break;

					case 'g':
						currentProblem = GenerateSquareAssignmentProblem();
						break;

					case 's':
						SaveProblem();
						break;

					case 'q':
						break;

					default:
						WriteColorLine(" < Wrong mode > ", ConsoleColor.Red);
						break;
				}

			} while (mode != 'q');
		}


		private int ReadInteger(string output)
		{
			System.Console.Write(output + "\n>");
			string str = String.Empty;
			int number;
			bool readSuccefully = false;
			do
			{
				str = System.Console.ReadLine();
				readSuccefully = int.TryParse(str, out number);

				if (!readSuccefully)
				{
					Alert(" < Wrong format. unable to convert value to an integer number > ");
					Info(" < Try again!  > ");
					System.Console.Write(">");
				}
			}
			while (!readSuccefully);


			return number;
		}

		private double ReadDouble(string output)
		{
			System.Console.Write(output + "\n>");
			string str = String.Empty;
			double number;
			bool readSuccefully = false;
			do
			{
				str = System.Console.ReadLine();
				readSuccefully = double.TryParse(str, out number);

				if (!readSuccefully) {
					Alert(" < Wrong format. unable to convert value to a float number > ");
					Info(" < Try again!  > ");
					System.Console.Write(">");
				}
			}
			while (!readSuccefully);


			return number;
		}

		private AssignmentProblem GenerateSquareAssignmentProblem()
		{
			try
			{
				int size = ReadInteger(" < Enter size of assignment problem > ");

				var options = new RandomAssignmentProblemBuilderOptions()
				{
					NumberOfTasks = size,
					NumberOfWorkers = size,
					ExpectedValC = ReadInteger(" < Enter expected value for matrix C > "),
					ExpectedValT = ReadInteger(" < Enter expected value for matrix T > "),
					HalfIntervalC = ReadInteger(" < Enter length of half-interval half-interval for expected value of matrix C > "),
					HalfIntervalT = ReadInteger(" < Enter length of half-interval for expected value of matrix T > "),
					MutationProbability = ReadDouble(" < Enter mutation probability for genetic algorithm > "),
					GeneticAlgorithmsNumberOfIterations = ReadInteger(" < Enter number of iterations for genetic algorithm > ")

				};

			
				var builder = new RandomSquareAssignmentProblemBuilder(options);
				Success(" < Problem has been successfully generated > ");
				return builder.Create();
			}
			catch (ArgumentException e)
			{
				Alert($"Error: {e.Message}");
			}
			return null;

		}

		private void SaveProblem()
		{
			System.Console.Write("Enter path to the output file\n>");
			string path = System.Console.ReadLine();
			if (path == null)
			{
				Alert(" < Empty path to the output file > ");
				return;
			}
			try
			{
				currentProblem.SaveProblem(path);
				Success($"Result has been successfully saved: {Path.GetFullPath(path)}");
			}
			catch (ArgumentException e)
			{
				Alert($"Error: {e.Message}");
			}
		}

	}


}
