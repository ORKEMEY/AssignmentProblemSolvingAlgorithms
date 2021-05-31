using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure;
using Infrastructure.Extensions;

namespace GeneticAlgorithm
{
	public class Individual
	{
		public readonly int[] genes;

		public int NumberOfGenes => genes.Length;

		public int this[int ind]
		{
			get => genes[ind];
			set => genes[ind] = value;
		}

		public Individual(int size)
		{
			genes = new int[size];
			var random = new Random();

			for (int col = 0; col < size; col++)
			{
				this[col] = random.Next(0, size);
			}
			this.Reanimate();
		}

		public Individual(Individual firstParent, Individual secondParent)
		{
			genes = new int[firstParent.NumberOfGenes < secondParent.NumberOfGenes 
				? firstParent.NumberOfGenes : secondParent.NumberOfGenes];

			var random = new Random(DateTime.Now.Millisecond);

			for (int col = 0; col < this.NumberOfGenes; col++)
			{
				int min = firstParent[col] < secondParent[col] ? firstParent[col] : secondParent[col];
				int max = firstParent[col] > secondParent[col] ? firstParent[col] : secondParent[col];

				this[col] = random.Next(min == 0 ? min : min - 1, max == NumberOfGenes -1 ? max : max + 1);
			}
			this.Reanimate();
		}

		public void Reanimate()
		{
			var notUsedValues = new Stack<int>();
			bool contains;
			for (int ind = 0; ind < genes.Length; ind++)
			{
				contains = false;
				for (int count = 0; count < genes.Length; count++)
				{
					if (genes[count] == ind)
					{
						contains = true;
						break;
					}
				}
				if (!contains) notUsedValues.Push(ind);
			}

			if (notUsedValues.Count == 0) return;

			for (int ind = 0; ind < genes.Length; ind++)
			{
				for (int count = 0; count < ind; count++)
				{
					if (genes[count] == genes[ind])
					{
						genes[ind] = notUsedValues.Pop();
						break;
					}
				}

			}
		}

		public override bool Equals(object obj)
		{

			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			if (obj as Individual == null) return false;

			if (NumberOfGenes != (obj as Individual).NumberOfGenes) return false;

			for (int count = 0; count < NumberOfGenes; count++)
			{
				if (this[count] != (obj as Individual)[count]) return false;
			}

			return true;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{

			string str = String.Empty;
			foreach (var i in genes)
			{
				str += (i + 1).ToString() + " ";
			}
			return str;
		}

		public double CalcDistanceToPerfectPoint(PerfectPoint point, AssignmentProblem problem)
		{
			var distByC = problem.CalculateObjective(problem.MatrixC.ToDouble(), genes);
			var distByT = problem.CalculateObjective(problem.MatrixT.ToDouble(), genes);
			var curDist = point.CalcDistanceToPerfectPoint(coordinateC: distByC, coordinateT: distByT);

			return curDist;
		}

		public double CalcRelativeDistanceToPerfectPoint(PerfectPoint point, AssignmentProblem problem)
		{
			var distByC = problem.CalculateObjective(problem.MatrixC.ToDouble(), genes);
			var distByT = problem.CalculateObjective(problem.MatrixT.ToDouble(), genes);
			var curDist = point.CalcRelativeDistanceToPerfectPoint(coordinateC: distByC, coordinateT: distByT);

			return curDist;
		}

	}
}
