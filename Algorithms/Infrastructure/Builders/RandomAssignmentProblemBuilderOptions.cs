using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
	public class RandomAssignmentProblemBuilderOptions
	{

		protected int _numberOfWorkers;
		protected int _numberOfTasks;
		protected int _expectedValC;
		protected int _expectedValT;
		protected int _halfIntervalC;
		protected int _halfIntervalT;

		public virtual int NumberOfWorkers
		{
			get => _numberOfWorkers;
			set
			{
				if (value <= 0)
				{
					throw new ArgumentException("Number of workers cann't be less than null");
				}
				_numberOfWorkers = value;
			}

		}
		public virtual int NumberOfTasks
		{
			get => _numberOfTasks;
			set
			{
				if (value <= 0)
				{
					throw new ArgumentException("Number of tasks cann't be less than null");
				}
				_numberOfTasks = value;
			}

		}
		public virtual int ExpectedValC
		{
			get => _expectedValC;
			set
			{
				if (value <= 0)
				{
					throw new ArgumentException("Expected value in matrix C cann't be less than null");
				}
				_expectedValC = value;
			}

		}
		public virtual int ExpectedValT
		{
			get => _expectedValT;
			set
			{
				if (value <= 0)
				{
					throw new ArgumentException("Expected value in matrix T cann't be less than null");
				}
				_expectedValT = value;
			}

		}
		public virtual int HalfIntervalC
		{
			get => _halfIntervalC;
			set
			{
				if (value < 0 || ExpectedValC - value < 0)
				{
					throw new ArgumentException("Interval of possible elements of matrix C cann't include negative numbers. Half-interval cann't be less than null");
				}
				_halfIntervalC = value;
			}

		}
		public virtual int HalfIntervalT
		{
			get => _halfIntervalT;
			set
			{
				if (value < 0 || ExpectedValT - value <= 0)
				{
					throw new ArgumentException("Interval of possible elements of matrix T cann't include negative numbers and null. Half-interval cann't be less than null");
				}
				_halfIntervalT = value;
			}

		}
		public virtual double? MutationProbability { get; set; }
		public virtual int? GeneticAlgorithmsNumberOfIterations { get; set; }


		public RandomAssignmentProblemBuilderOptions(int numberOfWorkers, int numberOfTasks, int expectedValC, int expectedValT)
		{
			this.NumberOfWorkers = numberOfWorkers;
			this.NumberOfTasks = numberOfTasks;
			this.ExpectedValC = expectedValC;
			this.ExpectedValT = expectedValT;
		}

		public RandomAssignmentProblemBuilderOptions()
		{

		}
	}
}
