using System;
using System.Collections.Generic;
using Infrastructure;

namespace Tests
{
	public class BigComparisonSquareAssignmentProblemTester : Tester<SquareAssignmentProblem>
	{

		public List<ProblemResolvedEventArgs> MetricsFromGeneticAlg { get; protected set; }
		private int NumberOfProblems {get; set;}

		public BigComparisonSquareAssignmentProblemTester(TesterOptions options, int numberOfProblems) : base(options)
		{
			MetricsFromGeneticAlg = new List<ProblemResolvedEventArgs>();
			NumberOfProblems = numberOfProblems;
		}

		/// <exception cref="ArgumentException"/>
		public override void Test()
		{
			options.GeneticAlgorithmsNumberOfIterations = 500;

			if (NumberOfProblems <= 1) throw new ArgumentException(" Number of generated problems cann't be less than 2 ");

			for (int count = 10; count <= 10 + (NumberOfProblems - 1) * 5; count += 5)
			{
				options.NumberOfTasks = options.NumberOfWorkers = count;

				Run(options);
			}

			var paintor = new ChartPainterForBigTest(Resolvers, Metrics, MetricsFromGeneticAlg, TesterOptions);
			paintor.DrawComparativeLineChartsByTime();
			paintor.DrawComparativeLineChartsByAccuracy();
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

			RunBigTestOfHungarianAlgorithm(problem, testerOptions);
			RunBigTestOfGeneticAlgorithm(problem, testerOptions);

		}

		private void RunBigTestOfHungarianAlgorithm(SquareAssignmentProblem problem, TesterOptions testerOptions)
		{

			//create algorithm obj
			var alg = new HungarianAlgorithm.HungarianAlgorithmForSquareProblem();
			// create aggregator class
			var currentResolver = new AssignmentProblemResolver<SquareAssignmentProblem>(alg, problem);

			currentResolver.OnProblemResolved += (obj, args) =>
			{
				Resolvers.Add(currentResolver);
				Metrics.Add(args);
				TesterOptions.Add(testerOptions.Clone() as TesterOptions);
				AssignmentProblemWriter.Write($"{(String.IsNullOrEmpty(testerOptions.Path) ? "last" : testerOptions.Path)}.hungarian.big.test.result.json", problem, args, testerOptions.ToString());
			};

			//start algirithm
			currentResolver.Resolve();
		}

		private void RunBigTestOfGeneticAlgorithm(SquareAssignmentProblem problem, TesterOptions testerOptions)
		{

			//create algorithm obj
			var alg = new GeneticAlgorithm.GeneticAlgorithmForSquareProblem();
			// create aggregator class
			var currentResolver = new AssignmentProblemResolver<SquareAssignmentProblem>(alg, problem);

			currentResolver.OnProblemResolved += (obj, args) =>
			{
				Resolvers.Add(currentResolver);
				MetricsFromGeneticAlg.Add(args);
				TesterOptions.Add(testerOptions.Clone() as TesterOptions);
				AssignmentProblemWriter.Write($"{(String.IsNullOrEmpty(testerOptions.Path) ? "last" : testerOptions.Path)}.genetic.big.test.result.json", problem, args, testerOptions.ToString());
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
				MutationProbability = testerOptions.MutationProbability,
				GeneticAlgorithmsNumberOfIterations = testerOptions.GeneticAlgorithmsNumberOfIterations

			};
			return builderOptions;
		}


	}
}
