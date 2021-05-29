using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Infrastructure.Extensions;

namespace Infrastructure
{
	public static class ResultWriter
	{

		/// <exception cref="ArgumentException"/>
		public static async void WriteAsync(string Path, ResultDTO result)
		{
			FileStream fs = null;
			try
			{
				fs = new FileStream(Path, FileMode.Create);
				await JsonSerializer.SerializeAsync(fs,
					new { result.ObjectiveValueByC, result.ObjectiveValueByT, result.Result });
			}
			catch (Exception e)
			{
				throw new ArgumentException(e.Message);
			}
			finally
			{
				fs?.Close();
			}
		}
	}


}
