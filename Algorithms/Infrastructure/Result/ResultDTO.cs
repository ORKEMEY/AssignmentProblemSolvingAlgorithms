using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Extensions;

namespace Infrastructure
{

	public struct ResultDTO
	{
		public double ObjectiveValueByT;
		public double ObjectiveValueByC;
		public int[][] Result;

		public ResultDTO(int[,] result, double objectiveValueByT, double objectiveValueByC)
		{
			Result = null;
			ObjectiveValueByT = objectiveValueByT;
			ObjectiveValueByC = objectiveValueByC;
			Result = result.ToJaggedArray();
		}

	}
}
