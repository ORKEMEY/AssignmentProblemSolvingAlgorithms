using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Infrastructure;
using Infrastructure.Extensions;

namespace GeneticAlgorithm
{
	public class Population
	{
		private readonly Individual[] idividuals;

		public int NumberOfIdividuals => idividuals.Length;

		public Individual this[int ind]
		{
			get => idividuals[ind];
			set => idividuals[ind] = value;
		}

		public int this[int indIndiv, int indGene]
		{
			get => idividuals[indIndiv][indGene];
			set => idividuals[indIndiv][indGene] = value;
		}

		/// <param name="sizeOfPopulation"></param>
		/// <param name="numberOfGenesOfIndividual"></param>
		/// <exception cref="ArgumentException"></exception>>
		public Population(int sizeOfPopulation, int numberOfGenesOfIndividual)
		{
			if (sizeOfPopulation > Factorial(numberOfGenesOfIndividual))
			{
				throw new ArgumentException("Number of individuals in genetic algorithm's populaion cann't be larger than number of permutations of individual's genes");
			}

			static long Factorial(int numb)
			{
				long res = 1;
				for (int i = numb; i > 1; i--)
					res *= i;
				return res;
			}

			idividuals = new Individual[sizeOfPopulation];
			Individual curInd;

			for (int count = 0; count < NumberOfIdividuals; count++)
			{
				do
				{
					curInd = new Individual(numberOfGenesOfIndividual);
				} while (!IsIndividualUnique(curInd));

				idividuals[count] = curInd;
			}

		}

		public bool IsIndividualUnique(Individual individual)
		{

			for (int count = 0; count < NumberOfIdividuals; count++)
			{
				if (idividuals[count] != null && idividuals[count].Equals(individual))
				{
					return false;
				}
			}
			return true;
		}

		public Individual GetBestIndividual(PerfectPoint point, AssignmentProblem problem)
		{

			double bestDist = double.MaxValue, curDist;
			Individual best = null;
			
			foreach (var ind in idividuals)
			{
				curDist = ind.CalcDistanceToPerfectPoint(point, problem);

				if (curDist < bestDist)
				{
					bestDist = curDist;
					best = ind;
				}		
			}
			return best;
		}

		public Individual GetWorstIndividual(PerfectPoint point, AssignmentProblem problem)
		{

			double worstDist = double.MinValue, curDist;
			Individual worst = null;

			foreach (var ind in idividuals)
			{
				curDist = ind.CalcDistanceToPerfectPoint(point, problem);

				if (curDist > worstDist)
				{
					worstDist = curDist;
					worst = ind;
				}
			}
			return worst;
		}

		class IndividualComparer : IComparer<Individual>
		{

			PerfectPoint perPoint;
			AssignmentProblem problem;

			public IndividualComparer(PerfectPoint point, AssignmentProblem problem)
			{
				this.perPoint = point;
				this.problem = problem;
			}
				
			int IComparer<Individual>.Compare(Individual x, Individual y)
			{
				var curDistX = x.CalcDistanceToPerfectPoint(perPoint, problem);
				var curDistY = y.CalcDistanceToPerfectPoint(perPoint, problem);

				return curDistX.CompareTo(curDistY);
			}
		}

		public IEnumerable<Individual> GetHalfOfBestindividuals(PerfectPoint point, AssignmentProblem problem)
		{

			var indComp = new IndividualComparer(point, problem);
			Array.Sort(idividuals, indComp);

			for (int count = 0; count < NumberOfIdividuals/2; count ++)
			{
				yield return idividuals[count];
			}

		}


		public void Refresh(Individual[] descendants, PerfectPoint point, AssignmentProblem problem)
		{
			var indComp = new IndividualComparer(point, problem);
			
			for(int count = 0; count < descendants.Length; count++)
			{
				Array.Sort(idividuals, indComp);

				var curDistWorst = idividuals[NumberOfIdividuals - 1].CalcDistanceToPerfectPoint(point, problem);
				var curDistDescendant = descendants[count].CalcDistanceToPerfectPoint(point, problem);

				if (curDistWorst > curDistDescendant)
				{
					idividuals[NumberOfIdividuals - 1] = descendants[count];
				}

			}

		}

	}

}
