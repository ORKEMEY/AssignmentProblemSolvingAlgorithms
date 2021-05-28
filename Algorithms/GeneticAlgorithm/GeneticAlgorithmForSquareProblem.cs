using System;
using System.Linq;
using System.Collections.Generic;
using Infrastructure;
using Infrastructure.Extensions;

namespace GeneticAlgorithm
{

	public class GeneticAlgorithmForSquareProblem : GeneticAlgorithm<SquareAssignmentProblem>
	{

		public Population Population { get; protected set; }
		public PerfectPoint PerfectPoint { get; protected set; }
		public SquareAssignmentProblem Problem { get; protected set; }

		public override int[] Resolve(SquareAssignmentProblem problem)
		{
			NumberOfIterations = problem.GeneticAlgorithmsNumberOfIterations.Value;
			MutationProbability = problem.MutationProbability.Value;
			PerfectPoint = new PerfectPoint(problem);
			Population = new Population(10, problem.Size);
			Problem = problem;
			

			for (int count = 0; count < problem.GeneticAlgorithmsNumberOfIterations; count++)
			{
				Individual fisrt, second;
				if (count%2 == 0)
				{
					ChooseParentsBySelection(out fisrt, out second);
				}
				else
				{
					ChooseParentsByPhenotypicOutbreeding(out fisrt, out second);
				}
			
				var descendants = CreateDescendants(fisrt, second).ToArray();

				Mutate(descendants, problem.MutationProbability.Value);

				var random = new Random(DateTime.Now.Millisecond);

				ImproveLocallyDescendant(
					descendants[random.Next(0, descendants.Length)]);

				Population.Refresh(descendants, PerfectPoint, problem);

				Population.GetBestIndividual(PerfectPoint, problem);

			}

			return Population.GetBestIndividual(PerfectPoint, problem).genes;
			
		}

		public void ChooseParentsByPhenotypicOutbreeding(out Individual best, out Individual worst)
		{
			best = worst = null;
			if (Population == null) return;
			
			best = Population.GetBestIndividual(PerfectPoint, Problem);
			worst = Population.GetWorstIndividual(PerfectPoint, Problem);
		}

		public void ChooseParentsBySelection(out Individual fisrt, out Individual second)
		{

			fisrt = second = null;
			if (Population == null) return;

			Individual[] halfOfBestInds = Population.GetHalfOfBestindividuals(PerfectPoint, Problem).ToArray();
			var random = new Random(DateTime.Now.Millisecond);

			int forstInd = random.Next(0, halfOfBestInds.Length), secondind;

			do {
				secondind = random.Next(0, halfOfBestInds.Length);
			} while (forstInd == secondind);

			fisrt = halfOfBestInds[forstInd];
			second = halfOfBestInds[secondind];

		}

		public IEnumerable<Individual> CreateDescendants(Individual fisrt, Individual second)
		{
			for(int count = 0; count < Population.NumberOfIdividuals/2; count++)
			{
				yield return new Individual(fisrt, second);
			}
		}

		public void Mutate(Individual[] individuals, double mutationProbability)
		{

			var randonm = new Random();
			int swap, randFirstSwapInd, randSecondSwapInd;

			for(int count = 0; count < individuals.Length; count++)
			{
				var randVal = randonm.NextDouble();

				if (randVal > mutationProbability) continue;

				randFirstSwapInd = randonm.Next(0, individuals[count].NumberOfGenes);
				do
				{
					randSecondSwapInd = randonm.Next(0, individuals[count].NumberOfGenes);
				} while (randSecondSwapInd == randFirstSwapInd);
				 swap = individuals[count][randFirstSwapInd];

				individuals[count][randFirstSwapInd] = individuals[count][randSecondSwapInd];
				individuals[count][randSecondSwapInd] = swap;
			}


		}

		public void ImproveLocallyDescendant(Individual individual)
		{

			double initDist = individual.CalcDistanceToPerfectPoint(PerfectPoint, Problem);
			double initDistAfterMutation;
			int count = 0;
			do
			{
				var arrInd = new Individual[] { individual };

				Mutate(arrInd, mutationProbability: 1);
			
				initDistAfterMutation = individual.CalcDistanceToPerfectPoint(PerfectPoint, Problem);
				count++;
				
			} while (initDist <= initDistAfterMutation && count < (Problem.Size * (Problem.Size - 1)) / 6); //one third of all possible permutations iterated
		}



	}

}
