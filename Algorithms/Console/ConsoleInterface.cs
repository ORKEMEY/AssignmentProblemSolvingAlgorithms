using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Infrastructure;
using Console.Menu;

namespace Console
{

	class ConsoleInterface
	{

		private static ConsoleInterface Source;
		public static ConsoleInterface GetSource()
		{
			if (Source == null)
				Source = new ConsoleInterface();
			return Source;
		}

		private static MainMenu mainMenu;

		private ConsoleInterface()
		{
			mainMenu = null;
		}

		public void Run(string pathToFileArg)
		{
			var problem = CreateSquareAssignmentProblemInstance(pathToFileArg);

			if (problem == null) return;

			mainMenu = new MainMenu(problem);
			mainMenu.RunMenu();

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

		public static void Info(string str) =>
			MainMenu.Info(str);

		public static void Alert(string str) =>
			MainMenu.Alert(str);
	}


	

}
