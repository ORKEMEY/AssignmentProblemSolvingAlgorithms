using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Infrastructure;
using System.Reflection;
using System.Drawing;
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
				$"{HalfIntervalC.ToString()[0]}/{ExpectedValT.ToString()[0]},{HalfIntervalT.ToString()[0]}";
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

		protected Tester(TesterOptions options)
		{
			this.options = options;
		}

		public abstract void Test();

	}

}
