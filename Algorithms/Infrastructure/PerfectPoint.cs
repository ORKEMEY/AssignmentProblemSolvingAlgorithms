using System;
using System.Collections.Generic;
using System.Text;
using Google.OrTools.LinearSolver;
using Infrastructure;
using Infrastructure.Extensions;

namespace Infrastructure
{
	public class PerfectPoint
	{
		enum OptimizationDirection { Minimization, Maximization }

		public readonly double CoordinateByC;
		public readonly double CoordinateByT;

		public PerfectPoint(AssignmentProblem problem)
		{

			var objValueByC = RunOptimization(problem.MatrixC.ToDouble(), out int[] result, OptimizationDirection.Maximization);
			if (objValueByC.HasValue && result != null)
			{
				CoordinateByC = objValueByC.Value;
			}
			else CoordinateByC = double.MaxValue;

			var objValueByT = RunOptimization(problem.MatrixT.ToDouble(), out result, OptimizationDirection.Minimization);
			if (objValueByT.HasValue && result != null)
			{
				CoordinateByT = objValueByT.Value;
			}
			else CoordinateByT = 0;

		}

		private double? RunOptimization(double[,] costs, out int[] result, OptimizationDirection direction)
		{
			result = new int[costs.GetLength(0)];

			int numWorkers, numTasks;
			numWorkers = numTasks = costs.GetLength(0);


			Solver solver = Solver.CreateSolver("SCIP");

			// Variables.
			// x[i, j] is an array of 0-1 variables, which will be 1
			// if worker i is assigned to task j.
			Variable[,] x = new Variable[numWorkers, numTasks];

			void SetVariables()
			{
				for (int i = 0; i < numTasks; ++i)
				{
					for (int j = 0; j < numWorkers; ++j)
					{
						x[i, j] = solver.MakeIntVar(0, 1, $"worker_{j}_task_{i}");
					}
				}
			}
			void SetConstraints()
			{
				// Constraints
				// Each worker is assigned to at most one task.
				for (int j = 0; j < numWorkers; ++j)
				{
					Constraint constraint = solver.MakeConstraint(1, 1, "");
					for (int i = 0; i < numTasks; ++i)
					{
						constraint.SetCoefficient(x[i, j], 1);
					}
				}
				// Each task is assigned to exactly one worker.
				for (int i = 0; i < numTasks; ++i)
				{
					Constraint constraint = solver.MakeConstraint(1, 1, "");
					for (int j = 0; j < numWorkers; ++j)
					{
						constraint.SetCoefficient(x[i, j], 1);
					}
				}
			}
			void SetObjectiveToMinimization()
			{
				// Objective
				Objective objective = solver.Objective();
				for (int i = 0; i < numTasks; ++i)
				{
					for (int j = 0; j < numWorkers; ++j)
					{
						objective.SetCoefficient(x[i, j], costs[i, j]);
					}
				}
				objective.SetMinimization();
			}
			void SetObjectiveToMaximization()
			{
				// Objective
				Objective objective = solver.Objective();
				for (int i = 0; i < numTasks; ++i)
				{
					for (int j = 0; j < numWorkers; ++j)
					{
						objective.SetCoefficient(x[i, j], costs[i, j]);
					}
				}
				objective.SetMaximization();
			}

			double? Solve(int[] result)
			{
				Solver.ResultStatus resultStatus = solver.Solve();
				// Print solution.
				// Check that the problem has a feasible solution.
				if (resultStatus != Solver.ResultStatus.FEASIBLE & resultStatus != Solver.ResultStatus.OPTIMAL)
				{
					Console.WriteLine("No solution found.");
					result = null;
					return null;
				}


				for (int i = 0; i < numTasks; ++i)
				{
					for (int j = 0; j < numWorkers; ++j)
					{
						// Test if x[i, j] is 0 or 1 (with tolerance for floating point
						// arithmetic).
						if (x[i, j].SolutionValue() > 0.5)
						{
							result[i] = j;
							//Console.WriteLine($"Worker {j} assigned to task {i}. Cost: {costs[i, j]}");
						}
					}
				}

				return solver.Objective().Value();
			}

			SetVariables();
			SetConstraints();

			if (direction == OptimizationDirection.Minimization) SetObjectiveToMinimization();
			else SetObjectiveToMaximization();
			var objectiveValue = Solve(result);

			return objectiveValue;
		}

		public double CalcDistanceToPerfectPoint(double coordinateC, double coordinateT)
		{
			return Math.Sqrt(
				Math.Pow(Math.Abs(CoordinateByC - coordinateC), 2) +
				Math.Pow(Math.Abs(CoordinateByT - coordinateT), 2));
		}

		public double CalcRelativeDistanceToPerfectPoint(double coordinateC, double coordinateT)
		{
			return Math.Sqrt(
				Math.Pow(Math.Abs(CoordinateByC - coordinateC)/ CoordinateByC, 2) +
				Math.Pow(Math.Abs(CoordinateByT - coordinateT)/ CoordinateByT, 2));
		}

	}
}
