using System;
using System.Collections.Generic;
using System.Text;

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
	}
}
