﻿using System;

namespace Infrastructure
{
	/// <summary>
	/// Class aggregator for algorithm, task & result
	/// </summary>
	 
	public class AssignmentProblemResolver<T> where T : AssignmentProblem
	{

		public readonly IAssignmentProblemSolvingAlgorithm<T> Algorythm;
		public readonly T Problem;
		public int[] Result { get; protected set; }

		public AssignmentProblemResolver(IAssignmentProblemSolvingAlgorithm<T> algorythm, T problem)
		{
			this.Algorythm = algorythm;
			this.Problem = problem;
		}

		public void Resolve()
		{
			Result = Algorythm.Resolve(Problem);
		}

		public override string ToString()
		{
			string str = String.Empty;

			foreach(var i in Result)
			{
				str += (i + 1).ToString() + " ";
			}
			return str;
		}

	}

}
