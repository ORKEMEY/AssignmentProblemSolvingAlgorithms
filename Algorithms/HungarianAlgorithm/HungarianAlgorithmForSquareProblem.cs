using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Infrastructure;
using Infrastructure.Extensions;
using Google.OrTools.LinearSolver;

namespace HungarianAlgorithm
{

	public sealed class HungarianAlgorithmForSquareProblem : HungarianAlgorithm<SquareAssignmentProblem>
	{

		enum OptimizationDirection{Minimization, Maximization }

		private struct ObjectiveValues
		{
			public readonly double ObjectiveByT;
			public readonly double ObjectiveByC;
			public readonly double ObjectiveByF;
			public readonly int[] Result;

			public ObjectiveValues(double objectiveByT, double objectiveByC, double objectiveByF, int[] result)
			{
				ObjectiveByT = objectiveByT;
				ObjectiveByC = objectiveByC;
				ObjectiveByF = objectiveByF;
				Result = result;
			}

			public ObjectiveValues(HungarianAlgorithmForSquareProblem alg, int[] result)
			{
				ObjectiveByT = alg.CalculateObjective(alg.matrixT, result);
				ObjectiveByC = alg.CalculateObjective(alg.matrixC, result);
				ObjectiveByF = alg.CalculateObjective(alg.matrixF, result);
				Result = result;
			}

			private static void CalcEquitableCompromiseCoefs(ObjectiveValues first, ObjectiveValues second, 
				out double relativeImprovement, out double relativeDegradation)
			{
				relativeImprovement = relativeDegradation = 0;

				if (first.ObjectiveByC > second.ObjectiveByC)
				{
					relativeDegradation += Math.Abs(first.ObjectiveByC - second.ObjectiveByC) / first.ObjectiveByC;
				}
				else
				{
					relativeImprovement += Math.Abs(first.ObjectiveByC - second.ObjectiveByC) / second.ObjectiveByC;
				}

				if (first.ObjectiveByT < second.ObjectiveByT)
				{
					relativeDegradation += Math.Abs(first.ObjectiveByC - second.ObjectiveByC) / first.ObjectiveByC;
				}
				else
				{
					relativeImprovement += Math.Abs(first.ObjectiveByC - second.ObjectiveByC) / second.ObjectiveByC;
				}
			}

			public static bool operator > (ObjectiveValues first, ObjectiveValues second)
			{

				CalcEquitableCompromiseCoefs(first, second,
				out double relativeImprovement, out double relativeDegradation);

				if(relativeDegradation > relativeImprovement) return true;
				else return false;
			}

			public static bool operator < (ObjectiveValues first, ObjectiveValues second)
			{
				CalcEquitableCompromiseCoefs(first, second,
				out double relativeImprovement, out double relativeDegradation);

				if (relativeDegradation < relativeImprovement) return true;
				else return false;
			}

		}

		private double[,] matrixC;
		private double[,] matrixT;

		private readonly List<ObjectiveValues> _objectives = new List<ObjectiveValues>();


		public override int[] Resolve(SquareAssignmentProblem problem)
		{
			FillMatrixF(problem);
			matrixC = problem.MatrixC.ToDouble();
			matrixT = problem.MatrixT.ToDouble();

			double[,] costs = problem.MatrixT.ToDouble();

			var t1 = RunOptimization(costs, out int[] result, OptimizationDirection.Minimization);
			if(t1.HasValue && result != null)
			{
				_objectives.Add(
					new ObjectiveValues(this, result.Clone() as int[])
					);
			}

			costs = problem.MatrixC.ToDouble();
			var c2 = RunOptimization(costs, out result, OptimizationDirection.Maximization);
			if (c2.HasValue && result != null)
			{
				_objectives.Add(
					new ObjectiveValues(this, result.Clone() as int[])
					);
			}

			var f3 = RunOptimization(matrixF, out result, OptimizationDirection.Maximization);		
			if (f3.HasValue && result != null)
			{
				_objectives.Add(
					new ObjectiveValues(this, result.Clone() as int[])
					);
			}

			var bestRes = EquitableCompromise();

			if (bestRes.HasValue) return bestRes.Value.Result;
			else return null;
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
					
				//Console.WriteLine($"Total cost: {solver.Objective().Value()}\n");
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

			if(direction == OptimizationDirection.Minimization) SetObjectiveToMinimization();
			else SetObjectiveToMaximization();
			var objectiveValue = Solve(result);

			return objectiveValue;
		}

		private ObjectiveValues? EquitableCompromise()
		{
			if (_objectives.Count == 0) return null;

			ObjectiveValues bestObj = _objectives[0];
			
			foreach(var obj in _objectives)
			{
				if (bestObj < obj)
						bestObj = obj;
			}
			
			return bestObj;
		}
	}
}