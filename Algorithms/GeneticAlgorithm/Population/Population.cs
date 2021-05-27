using System;
using System.Collections.Generic;
using System.Text;

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

			static int Factorial(int numb)
			{
				int res = 1;
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
				if (idividuals[count] != null & idividuals[count].Equals(individual))
				{
					return false;
				}
			}
			return true;
		}

	}

}
