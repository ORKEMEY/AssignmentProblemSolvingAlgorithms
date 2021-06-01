using System;
using System.Collections.Generic;
using System.IO;
using Infrastructure;
using System.Drawing.Text;

namespace Tests
{

	public enum Parameter { Small, Middle, Large };

	public class TesterOptions : ICloneable
	{

		public int NumberOfWorkers { get; set; }
		public int NumberOfTasks { get; set; }
		public Parameter ExpectedValC { get; set; }
		public Parameter ExpectedValT { get; set; }
		public Parameter HalfIntervalC { get; set; }
		public Parameter HalfIntervalT { get; set; }
		public virtual double? MutationProbability { get; set; }
		public virtual int? GeneticAlgorithmsNumberOfIterations { get; set; }
		public string Path { get; set; }

		public string VaryingParameterName { get; set; }

		public override string ToString()
		{
			return $"{NumberOfWorkers}/{ExpectedValC.ToString()[0]}," +
				$"{HalfIntervalC.ToString()[0]}/{ExpectedValT.ToString()[0]},{HalfIntervalT.ToString()[0]}" +
				$"/{(MutationProbability.HasValue ? MutationProbability : 0):f1}";
		}

		public string ToStringWithoutSize()
		{
			return $"/{ExpectedValC.ToString()[0]}," +
				$"{HalfIntervalC.ToString()[0]}/{ExpectedValT.ToString()[0]},{HalfIntervalT.ToString()[0]}" +
				$"/{(MutationProbability.HasValue ? MutationProbability : 0):f1}";
		}

		public string ToStringWithoutSlash()
		{
			return $"{NumberOfWorkers}-{ExpectedValC.ToString()[0]}," +
				$"{HalfIntervalC.ToString()[0]}-{ExpectedValT.ToString()[0]},{HalfIntervalT.ToString()[0]}" +
				$"-{(MutationProbability.HasValue ? MutationProbability : 0):f1}";
		}

		public object Clone()
		{
			return new TesterOptions()
			{
				NumberOfWorkers = this.NumberOfWorkers,
				NumberOfTasks = this.NumberOfWorkers,
				ExpectedValC = this.ExpectedValC,
				ExpectedValT = this.ExpectedValT,
				HalfIntervalC = this.HalfIntervalC,
				HalfIntervalT = this.HalfIntervalT,
				MutationProbability = this.MutationProbability,
				GeneticAlgorithmsNumberOfIterations = this.GeneticAlgorithmsNumberOfIterations,
				VaryingParameterName = this.VaryingParameterName,
				Path = this.Path
			};
		}

	}

	public abstract class Tester<T> where T : AssignmentProblem
	{
		protected readonly TesterOptions options;
		public List<AssignmentProblemResolver<T>> Resolvers { get; protected set; }
		public List<ProblemResolvedEventArgs> Metrics { get; protected set; }
		public List<TesterOptions> TesterOptions { get; protected set; }

		protected Tester(TesterOptions options)
		{
			this.options = options;
			Resolvers = new List<AssignmentProblemResolver<T>>();
			Metrics = new List<ProblemResolvedEventArgs>();
			TesterOptions = new List<TesterOptions>();
		}

		public abstract void Test();

		protected int GetExpectedValueByParameter(Parameter param)
		{
			int value = 0;

			switch (param)
			{
				case Parameter.Large:
					value = 100;
					break;

				case Parameter.Middle:
					value = 50;
					break;

				case Parameter.Small:
					value = 10;
					break;

			}
			return value;
		}

		protected float GetHalfIntervalByParameter(Parameter param)
		{
			float value = 0;

			switch (param)
			{
				case Parameter.Large:
					value = 0.5f;
					break;

				case Parameter.Middle:
					value = 0.25f;
					break;

				case Parameter.Small:
					value = 0.1f;
					break;

			}
			return value;
		}
	}

}
