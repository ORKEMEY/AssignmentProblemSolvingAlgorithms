using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure;

namespace Console.Menu
{
	public abstract class Menu
	{
		public abstract void ShowMenu();

		public abstract void RunMenu();

		public static void Info(string str) =>
			WriteColorLine(str, ConsoleColor.Cyan);

		public static void Alert(string str) =>
			WriteColorLine(str, ConsoleColor.Red);
		public static void WriteColorLine(string String, ConsoleColor Color)
		{
			System.Console.ForegroundColor = Color;
			System.Console.WriteLine(String);
			System.Console.ResetColor();
		}
		public static void WriteColor(string String, ConsoleColor Color)
		{
			System.Console.ForegroundColor = Color;
			System.Console.Write(String);
			System.Console.ResetColor();
		}

		public static char EnterMode()
		{
			char mode = ' ';
			try
			{
				System.Console.Write(" < Enter mode > \n>");
				mode = char.Parse(System.Console.ReadLine());
			}
			catch (FormatException)
			{
			}

			return char.ToLower(mode);
		}
	}
}
