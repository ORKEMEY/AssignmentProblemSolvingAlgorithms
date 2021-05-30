using System;
using System.IO;
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
			System.Console.WriteLine(" < Enter \"q\" to quit result submenu > ");
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
}
