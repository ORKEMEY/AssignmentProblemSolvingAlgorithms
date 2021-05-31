using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure;
using Tests;

namespace Console.Menu
{
	public class TestSubMenu : Menu
	{


		public override void ShowMenu()
		{
			System.Console.WriteLine(" < Test submenu > ");
			System.Console.WriteLine(" < Enter \"o\" to output menu > ");
			System.Console.WriteLine(" < Enter \"h\" to run hungarian algorithm test > ");
			System.Console.WriteLine(" < Enter \"g\" to run genetic algorithm test > ");
			System.Console.WriteLine(" < Enter \"r\" to run greedy algorithm test > ");
			System.Console.WriteLine(" < Enter \"t\" to run big dimensional test > ");
			System.Console.WriteLine(" < Enter \"q\" to quit test submenu > ");
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

					case 'h':
						RunHungarianAlgoritmTest();
						break;

					case 'g':
						RunGeneticAlgoritmTest();
						break;

					case 'r':
						RunGreedyAlgoritmTest();
						break;

					case 't':
						RunDimensionalHungarianAlgoritmTest();
						break;

					case 'q':
						break;

					default:
						WriteColorLine(" < Wrong mode > ", ConsoleColor.Red);
						break;
				}

			} while (mode != 'q');

		}

		private int? ReadInteger(string output, bool isVarying, string clue = "")
		{
			System.Console.WriteLine(output);
			if (isVarying) System.Console.WriteLine(clue);
			System.Console.Write("\n>");

			string str = String.Empty;
			int number;
			bool readSuccefully = false;
			do
			{
				str = System.Console.ReadLine();
				readSuccefully = int.TryParse(str, out number);

				if (str.Equals("*"))
				{
					if (isVarying) return null;
					else readSuccefully = false;
				}

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

		private Parameter? ReadParameter(string output, bool isVarying, string clue = "")
		{
			System.Console.WriteLine(output);
			if (isVarying) System.Console.WriteLine(clue);
			System.Console.Write("\n>");


			char key;
			Parameter? res = null;
			bool readSuccefully = false, classifiedSuccessfully = true;
			do
			{
				key = ' ';
				classifiedSuccessfully = true;
				readSuccefully = char.TryParse(System.Console.ReadLine(), out key);
				key = char.ToLower(key);

				if (!readSuccefully)
				{
					Alert(" < Wrong format. Unable to convert value to a symbol > ");
					Info(" < Try again!  > ");
					System.Console.Write(">");
					continue;
				}

				switch (key)
				{

					case 's':
						res = Parameter.Small;
						break;

					case 'm':
						res = Parameter.Middle;
						break;

					case 'l':
						res = Parameter.Large;
						break;

					case '*':
						if (isVarying) res = null;
						else classifiedSuccessfully = false;
						break;

					default:
						classifiedSuccessfully = false;
						break;
				}

				if (!classifiedSuccessfully)
				{
					Alert(" < Wrong format (acceptable values: s, m, l) > ");
					Info(" < Try again!  > ");
					System.Console.Write(">");
				}

			}
			while (!readSuccefully || !classifiedSuccessfully);

			return res;
		}

		private double? ReadDouble(string output, bool isVarying, string clue = "")
		{
			System.Console.WriteLine(output);
			if (isVarying) System.Console.WriteLine(clue);
			System.Console.Write("\n>");

			string str = String.Empty;
			double number;
			bool readSuccefully = false;
			do
			{
				str = System.Console.ReadLine();
				readSuccefully = double.TryParse(str, out number);

				if (str.Equals("*"))
				{
					if (isVarying) return null;
					else readSuccefully = false;
				}

				if (!readSuccefully)
				{
					Alert(" < Wrong format. Unable to convert value to an float number > ");
					Info(" < Try again!  > ");
					System.Console.Write(">");
				}
			}
			while (!readSuccefully);

			return number;
		}

		private void RunGeneticAlgoritmTest()
		{

			string path = GetPath();
			string varyingParameterName = String.Empty;
			string clueStr = " < Enter \"*\" if you want this parameter to be varied > ";

			int? size = ReadInteger(" < Enter size of assignment problem > ", String.IsNullOrEmpty(varyingParameterName), clueStr);
			if (!size.HasValue)
			{
				varyingParameterName = nameof(TesterOptions.NumberOfTasks);
				Info(" < Size of assignment problem will be varied > ");
				clueStr = String.Empty;
			}

			Parameter? expectedValC = ReadParameter($" < Enter expected value for matrix C (S - small, M - middle, L - Large) > ", String.IsNullOrEmpty(varyingParameterName), clueStr);
			if (String.IsNullOrEmpty(varyingParameterName) && !expectedValC.HasValue)
			{
				varyingParameterName = nameof(TesterOptions.ExpectedValC);
				Info(" < Expected value for matrix C will be varied > ");
				clueStr = String.Empty;
			}

			Parameter? expectedValT = ReadParameter($" < Enter expected value for matrix T (S - small, M - middle, L - Large) > ", String.IsNullOrEmpty(varyingParameterName), clueStr);
			if (String.IsNullOrEmpty(varyingParameterName) && !expectedValT.HasValue)
			{
				varyingParameterName = nameof(TesterOptions.ExpectedValT);
				Info(" < Expected value for matrix T will be varied > ");
				clueStr = String.Empty;
			}

			Parameter? halfIntervalC = ReadParameter($" < Enter length of half-interval for matrix C values (S - small, M - middle, L - Large) > ", String.IsNullOrEmpty(varyingParameterName), clueStr);
			if (String.IsNullOrEmpty(varyingParameterName) && !halfIntervalC.HasValue)
			{
				varyingParameterName = nameof(TesterOptions.HalfIntervalC);
				Info(" < Length of half-interval for matrix C values will be varied > ");
				clueStr = String.Empty;
			}

			Parameter? halfIntervalT = ReadParameter($" < Enter length of half-interval for matrix T values (S - small, M - middle, L - Large) > ", String.IsNullOrEmpty(varyingParameterName), clueStr);
			if (String.IsNullOrEmpty(varyingParameterName) && !halfIntervalT.HasValue)
			{
				varyingParameterName = nameof(TesterOptions.HalfIntervalT);
				Info(" < Length of half-interval for matrix T values will be varied > ");
				clueStr = String.Empty;
			}

			double? mutationProbability = ReadDouble($" < Enter mutation probability (float number [0, 1]) > ", String.IsNullOrEmpty(varyingParameterName), clueStr);
			if (String.IsNullOrEmpty(varyingParameterName))
			{
				if (halfIntervalT.HasValue)
				{
					Info(" < Mutation probability will be varied, because no parameter was specified as varying  > ");
				}
				else
				{
					Info(" < Mutation probability will be varied > ");
				}

				varyingParameterName = nameof(TesterOptions.MutationProbability);
			}


			var op = new TesterOptions()
			{
				Path = path,
				NumberOfTasks = size.HasValue ? size.Value : default,
				NumberOfWorkers = size.HasValue ? size.Value : default,
				ExpectedValC = expectedValC.HasValue ? expectedValC.Value : default,
				ExpectedValT = expectedValT.HasValue ? expectedValT.Value : default,
				HalfIntervalC = halfIntervalC.HasValue ? halfIntervalC.Value : default,
				HalfIntervalT = halfIntervalT.HasValue ? halfIntervalT.Value : default,
				MutationProbability = mutationProbability,
				VaryingParameterName = varyingParameterName,
			};

			var t = new GeneticAlgorithmBySquareAssignmentProblemTester(op);
			try
			{
				t.Test();
			}
			catch (ArgumentException e)
			{
				Alert($"Error: {e.Message}");
			}
		}

		private void RunDimensionalHungarianAlgoritmTest()
		{

			string path = GetPath();
			string varyingParameterName = String.Empty;
			string clueStr = " < Enter \"*\" if you want this parameter to be varied > ";
			varyingParameterName = nameof(TesterOptions.NumberOfTasks);

			Parameter? expectedValC = ReadParameter($" < Enter expected value for matrix C (S - small, M - middle, L - Large) > ", String.IsNullOrEmpty(varyingParameterName), clueStr);
			if (String.IsNullOrEmpty(varyingParameterName) && !expectedValC.HasValue)
			{
				varyingParameterName = nameof(TesterOptions.ExpectedValC);
				Info(" < Expected value for matrix C will be varied > ");
				clueStr = String.Empty;
			}

			Parameter? expectedValT = ReadParameter($" < Enter expected value for matrix T (S - small, M - middle, L - Large) > ", String.IsNullOrEmpty(varyingParameterName), clueStr);
			if (String.IsNullOrEmpty(varyingParameterName) && !expectedValT.HasValue)
			{
				varyingParameterName = nameof(TesterOptions.ExpectedValT);
				Info(" < Expected value for matrix T will be varied > ");
				clueStr = String.Empty;
			}

			Parameter? halfIntervalC = ReadParameter($" < Enter length of half-interval for matrix C values (S - small, M - middle, L - Large) > ", String.IsNullOrEmpty(varyingParameterName), clueStr);
			if (String.IsNullOrEmpty(varyingParameterName) && !halfIntervalC.HasValue)
			{
				varyingParameterName = nameof(TesterOptions.HalfIntervalC);
				Info(" < Length of half-interval for matrix C values will be varied > ");
				clueStr = String.Empty;
			}

			Parameter? halfIntervalT = ReadParameter($" < Enter length of half-interval for matrix T values (S - small, M - middle, L - Large) > ", String.IsNullOrEmpty(varyingParameterName), clueStr);
			if (String.IsNullOrEmpty(varyingParameterName))
			{
				if (halfIntervalT.HasValue)
				{
					Info(" < Length of half-interval for matrix T values will be varied, because no parameter was specified as varying  > ");
				}
				else
				{
					Info(" < Length of half-interval for matrix T values will be varied > ");
				}

				varyingParameterName = nameof(TesterOptions.HalfIntervalT);
			}

			var op = new TesterOptions()
			{
				Path = path,
				NumberOfTasks =  default,
				NumberOfWorkers =  default,
				ExpectedValC = expectedValC.HasValue ? expectedValC.Value : default,
				ExpectedValT = expectedValT.HasValue ? expectedValT.Value : default,
				HalfIntervalC = halfIntervalC.HasValue ? halfIntervalC.Value : default,
				HalfIntervalT = halfIntervalT.HasValue ? halfIntervalT.Value : default,
				VaryingParameterName = varyingParameterName,
			};

			var t = new HungarianAlgorithmBySquareAssignmentProblemTester(op);
			try
			{
				t.BigDimensionalTest();
			}
			catch (ArgumentException e)
			{
				Alert($"Error: {e.Message}");
			}
		}

		private void RunHungarianAlgoritmTest()
		{

			string path = GetPath();
			string varyingParameterName = String.Empty;
			string clueStr = " < Enter \"*\" if you want this parameter to be varied > ";

			int? size = ReadInteger(" < Enter size of assignment problem > ", String.IsNullOrEmpty(varyingParameterName), clueStr);
			if (!size.HasValue)
			{
				varyingParameterName = nameof(TesterOptions.NumberOfTasks);
				Info(" < Size of assignment problem will be varied > ");
				clueStr = String.Empty;
			}

			Parameter? expectedValC = ReadParameter($" < Enter expected value for matrix C (S - small, M - middle, L - Large) > ", String.IsNullOrEmpty(varyingParameterName), clueStr);
			if (String.IsNullOrEmpty(varyingParameterName) && !expectedValC.HasValue)
			{
				varyingParameterName = nameof(TesterOptions.ExpectedValC);
				Info(" < Expected value for matrix C will be varied > ");
				clueStr = String.Empty;
			}

			Parameter? expectedValT = ReadParameter($" < Enter expected value for matrix T (S - small, M - middle, L - Large) > ", String.IsNullOrEmpty(varyingParameterName), clueStr);
			if (String.IsNullOrEmpty(varyingParameterName) && !expectedValT.HasValue)
			{
				varyingParameterName = nameof(TesterOptions.ExpectedValT);
				Info(" < Expected value for matrix T will be varied > ");
				clueStr = String.Empty;
			}

			Parameter? halfIntervalC = ReadParameter($" < Enter length of half-interval for matrix C values (S - small, M - middle, L - Large) > ", String.IsNullOrEmpty(varyingParameterName), clueStr);
			if (String.IsNullOrEmpty(varyingParameterName) && !halfIntervalC.HasValue)
			{
				varyingParameterName = nameof(TesterOptions.HalfIntervalC);
				Info(" < Length of half-interval for matrix C values will be varied > ");
				clueStr = String.Empty;
			}

			Parameter? halfIntervalT = ReadParameter($" < Enter length of half-interval for matrix T values (S - small, M - middle, L - Large) > ", String.IsNullOrEmpty(varyingParameterName), clueStr);
			if (String.IsNullOrEmpty(varyingParameterName))
			{
				if (halfIntervalT.HasValue)
				{
					Info(" < Length of half-interval for matrix T values will be varied, because no parameter was specified as varying  > ");
				}
				else
				{
					Info(" < Length of half-interval for matrix T values will be varied > ");
				}

				varyingParameterName = nameof(TesterOptions.HalfIntervalT);
			}

			var op = new TesterOptions()
			{
				Path = path,
				NumberOfTasks = size.HasValue ? size.Value : default,
				NumberOfWorkers = size.HasValue ? size.Value : default,
				ExpectedValC = expectedValC.HasValue ? expectedValC.Value : default,
				ExpectedValT = expectedValT.HasValue ? expectedValT.Value : default,
				HalfIntervalC = halfIntervalC.HasValue ? halfIntervalC.Value : default,
				HalfIntervalT = halfIntervalT.HasValue ? halfIntervalT.Value : default,
				VaryingParameterName = varyingParameterName,	
			};

			var t = new HungarianAlgorithmBySquareAssignmentProblemTester(op);
			try
			{
				t.Test();
			}
			catch (ArgumentException e)
			{
				Alert($"Error: {e.Message}");
			}
		}

		private void RunGreedyAlgoritmTest()
		{

			string path = GetPath();
			string varyingParameterName = String.Empty;
			string clueStr = " < Enter \"*\" if you want this parameter to be varied > ";

			int? size = ReadInteger(" < Enter size of assignment problem > ", String.IsNullOrEmpty(varyingParameterName), clueStr);
			if (!size.HasValue)
			{
				varyingParameterName = nameof(TesterOptions.NumberOfTasks);
				Info(" < Size of assignment problem will be varied > ");
				clueStr = String.Empty;
			}

			Parameter? expectedValC = ReadParameter($" < Enter expected value for matrix C (S - small, M - middle, L - Large) > ", String.IsNullOrEmpty(varyingParameterName), clueStr);
			if (String.IsNullOrEmpty(varyingParameterName) && !expectedValC.HasValue)
			{
				varyingParameterName = nameof(TesterOptions.ExpectedValC);
				Info(" < Expected value for matrix C will be varied > ");
				clueStr = String.Empty;
			}

			Parameter? expectedValT = ReadParameter($" < Enter expected value for matrix T (S - small, M - middle, L - Large) > ", String.IsNullOrEmpty(varyingParameterName), clueStr);
			if (String.IsNullOrEmpty(varyingParameterName) && !expectedValT.HasValue)
			{
				varyingParameterName = nameof(TesterOptions.ExpectedValT);
				Info(" < Expected value for matrix T will be varied > ");
				clueStr = String.Empty;
			}

			Parameter? halfIntervalC = ReadParameter($" < Enter length of half-interval for matrix C values (S - small, M - middle, L - Large) > ", String.IsNullOrEmpty(varyingParameterName), clueStr);
			if (String.IsNullOrEmpty(varyingParameterName) && !halfIntervalC.HasValue)
			{
				varyingParameterName = nameof(TesterOptions.HalfIntervalC);
				Info(" < Length of half-interval for matrix C values will be varied > ");
				clueStr = String.Empty;
			}

			Parameter? halfIntervalT = ReadParameter($" < Enter length of half-interval for matrix T values (S - small, M - middle, L - Large) > ", String.IsNullOrEmpty(varyingParameterName), clueStr);
			if (String.IsNullOrEmpty(varyingParameterName))
			{
				if (halfIntervalT.HasValue)
				{
					Info(" < Length of half-interval for matrix T values will be varied, because no parameter was specified as varying  > ");
				}
				else
				{
					Info(" < Length of half-interval for matrix T values will be varied > ");
				}

				varyingParameterName = nameof(TesterOptions.HalfIntervalT);
			}

			var op = new TesterOptions()
			{
				Path = path,
				NumberOfTasks = size.HasValue ? size.Value : default,
				NumberOfWorkers = size.HasValue ? size.Value : default,
				ExpectedValC = expectedValC.HasValue ? expectedValC.Value : default,
				ExpectedValT = expectedValT.HasValue ? expectedValT.Value : default,
				HalfIntervalC = halfIntervalC.HasValue ? halfIntervalC.Value : default,
				HalfIntervalT = halfIntervalT.HasValue ? halfIntervalT.Value : default,
				VaryingParameterName = varyingParameterName,
			};

			var t = new GreedyAlgorithmBySquareAssignmentProblemTester(op);
			try
			{
				t.Test();
			}
			catch (ArgumentException e)
			{
				Alert($"Error: {e.Message}");
			}
		}

		private string GetPath()
		{
			System.Console.Write("Enter path to the output file\n>");
			string path = String.Empty;

			do
			{
				path = System.Console.ReadLine();
				if (String.IsNullOrEmpty(path))
				{
					Alert(" < Empty path to the output file > ");
					Info(" < Try again! > ");
				}
			} while (String.IsNullOrEmpty(path));

			return path;
		}
	}
}
