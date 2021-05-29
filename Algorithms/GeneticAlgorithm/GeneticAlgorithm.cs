using System;
using Infrastructure;

namespace GeneticAlgorithm
{
	public abstract class GeneticAlgorithm<T> : IAssignmentProblemSolvingAlgorithm<T>
		where T : AssignmentProblem
	{

		private double _mutationProbability;
		private int _numberOfIterations;

		public double MutationProbability 
		{
			get => _mutationProbability;
			protected set
			{
				if(value < 0 || value > 1)
				{
					throw new ArgumentException("Probability of mutation in genetic algorithm must be within the [0, 1] interval");
				}
				_mutationProbability = value;
			}
		}
		public int NumberOfIterations
		{
			get => _numberOfIterations;
			protected set
			{
				if (value < 0)
				{
					throw new ArgumentException("Number of iterations in genetic algorithm cann't be less than null");
				}
				_numberOfIterations = value;
			}
		}

		public abstract int[] Resolve(T problem);

	}


}
