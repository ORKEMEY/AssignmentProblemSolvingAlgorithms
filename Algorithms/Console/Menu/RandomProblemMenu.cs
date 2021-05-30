using System;
using System.IO;
using Infrastructure;

namespace Console.Menu
{
	public class RandomProblemMenu : Menu
	{

		public AssignmentProblem currentProblem { get; protected set; }

		public RandomProblemMenu(AssignmentProblem problem)
		{
			currentProblem = problem;
		}

		public override void ShowMenu()
		{
			System.Console.WriteLine(" < Random assignment problem submenu > ");
			System.Console.WriteLine(" < Enter \"o\" to output menu > ");
			System.Console.WriteLine(" < Enter \"g\" to generate random problem > ");
			System.Console.WriteLine(" < Enter \"m\" to output current assignment problem > ");
			System.Console.WriteLine(" < Enter \"s\" to save current assignment problem to file > ");
			System.Console.WriteLine(" < Enter \"q\" to quit random assignment problem submenu > ");
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
					Alert(" < Wrong format. Unable to convert value to an integer number > ");
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

				if (!readSuccefully)
				{
					Alert(" < Wrong format. Unable to convert value to a float number > ");
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
					HalfIntervalC = ReadInteger(" < Enter length of half-interval for matrix C values > "),
					HalfIntervalT = ReadInteger(" < Enter length of half-interval for matrix T values > "),
					MutationProbability = ReadDouble(" < Enter mutation probability for genetic algorithm (float number) > "),
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
