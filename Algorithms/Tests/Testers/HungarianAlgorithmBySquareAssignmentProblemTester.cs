using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure;
using System.Reflection;

namespace Tests
{
	public class HungarianAlgorithmBySquareAssignmentProblemTester : Tester<SquareAssignmentProblem>
	{

		public HungarianAlgorithmBySquareAssignmentProblemTester(TesterOptions options) : base(options)
		{
			Resolvers = new List<AssignmentProblemResolver<SquareAssignmentProblem>>();
			Metrics = new List<ProblemResolvedEventArgs>();
			TesterOptions = new List<TesterOptions>();
		}

		/// <exception cref="ArgumentException"/>
		/// <exception cref="ArgumentNullException"/>
		public override void Test()
		{
			var arrEnum = Enum.GetValues(typeof(Parameter));

			Type optionsType = options.GetType();
			PropertyInfo varyingProp;
			try
			{
				varyingProp = optionsType.GetProperty(options.VaryingParameterName);
			}
			catch (ArgumentNullException)
			{
				throw new ArgumentNullException("Verying parametr name is invalid");
			}

			if (varyingProp.PropertyType == typeof(int) &&
				(varyingProp.Name == nameof(options.NumberOfWorkers) || varyingProp.Name == nameof(options.NumberOfTasks)))
			{

				for (int count = 10; count <= 50; count += 10)
				{
					options.NumberOfTasks = options.NumberOfWorkers = count;

					Run(options);
				}

			}
			else if (varyingProp.PropertyType == typeof(Parameter))
			{
				foreach (var param in arrEnum)
				{
					varyingProp.SetValue(options, (Parameter)param);
					Run(options);
				}
			}

			var paintor = new MainChartPainter(Resolvers, Metrics, TesterOptions);
			paintor.DrawComparativeChartsByTime();
			paintor.DrawComparativeChartsByAccuracy();
			Resolvers.Clear();
			Metrics.Clear();
			TesterOptions.Clear();
		}


		/// <exception cref="ArgumentException"/>
		public void Run(TesterOptions testerOptions)
		{

			var builderOptions = FillBuilderOptions(testerOptions);

			var builder = new RandomSquareAssignmentProblemBuilder(builderOptions);
			var problem = builder.Create();

			//create algorithm obj
			var alg = new HungarianAlgorithm.HungarianAlgorithmForSquareProblem();
			// create aggregator class
			var currentResolver = new AssignmentProblemResolver<SquareAssignmentProblem>(alg, problem);

			currentResolver.OnProblemResolved += (obj, args) =>
			{
				Resolvers.Add(currentResolver);
				Metrics.Add(args);
				TesterOptions.Add(testerOptions.Clone() as TesterOptions);
				AssignmentProblemWriter.Write($"{(String.IsNullOrEmpty(testerOptions.Path) ? "last" : testerOptions.Path)}.hungarian.test.result.json", problem, args, testerOptions.ToString());
			};

			//start algirithm
			currentResolver.Resolve();
			//output result

			System.Console.WriteLine(problem.ToString());
			System.Console.WriteLine(currentResolver.ToString());
		}

		/// <exception cref="ArgumentException"/>
		private RandomAssignmentProblemBuilderOptions FillBuilderOptions(TesterOptions testerOptions)
		{

			var builderOptions = new RandomAssignmentProblemBuilderOptions()
			{
				NumberOfTasks = testerOptions.NumberOfTasks,
				NumberOfWorkers = testerOptions.NumberOfWorkers,
				ExpectedValC = GetExpectedValueByParameter(testerOptions.ExpectedValC),
				ExpectedValT = GetExpectedValueByParameter(testerOptions.ExpectedValT),
				HalfIntervalC = (int)(GetExpectedValueByParameter(testerOptions.ExpectedValC)
				* GetHalfIntervalByParameter(testerOptions.HalfIntervalC)),
				HalfIntervalT = (int)(GetExpectedValueByParameter(testerOptions.ExpectedValT)
				* GetHalfIntervalByParameter(testerOptions.HalfIntervalT)),
				/*MutationProbability,
				GeneticAlgorithmsNumberOfIterations*/

			};

			return builderOptions;
		}

	}


}
