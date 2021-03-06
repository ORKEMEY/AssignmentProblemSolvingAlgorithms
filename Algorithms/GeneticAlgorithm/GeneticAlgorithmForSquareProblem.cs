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
		public event EventHandler<GeneticAlgEventArgs> OnIterationEnded;

		/// <exception cref="ArgumentException"/>
		public override int[] Resolve(SquareAssignmentProblem problem)
		{
			if(!problem.GeneticAlgorithmsNumberOfIterations.HasValue | !problem.MutationProbability.HasValue)
				throw new ArgumentException("Number of interations and mutation probability have to have value");

			NumberOfIterations = problem.GeneticAlgorithmsNumberOfIterations.Value;
			MutationProbability = problem.MutationProbability.Value;
			PerfectPoint = new PerfectPoint(problem);
			Population = new Population(10, problem.Size);
			Problem = problem;
			Individual bestCurrentInd;

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

				bestCurrentInd = Population.GetBestIndividual(PerfectPoint, problem);
				OnIterationEnded?.Invoke(this, new GeneticAlgEventArgs
				{
					BestRelativeDistanceToPerfectPoint = bestCurrentInd.CalcRelativeDistanceToPerfectPoint(PerfectPoint, problem),
					NumberOfIteration = count + 1

				});
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

	public class GeneticAlgEventArgs
	{
		public double BestRelativeDistanceToPerfectPoint { get; set; }
		public int NumberOfIteration { get; set; }
		public int BestRelativeDistanceToPerfectPointInPercent => (int)(BestRelativeDistanceToPerfectPoint * 100);
	}

}
