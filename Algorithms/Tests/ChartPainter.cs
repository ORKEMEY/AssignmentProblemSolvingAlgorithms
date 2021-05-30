using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Drawing;
using Infrastructure;

namespace Tests
{
	public class ChartPainter
	{

		public List<AssignmentProblemResolver<SquareAssignmentProblem>> Resolvers { get; protected set; }
		public List<ProblemResolvedEventArgs> Metrics { get; protected set; }
		public List<TesterOptions> TesterOptions { get; set; }
		private int sizeX, sizeY, margin;

		public ChartPainter(List<AssignmentProblemResolver<SquareAssignmentProblem>> resolvers, List<ProblemResolvedEventArgs> metrics, List<TesterOptions> testerOptions)
		{
			Resolvers = resolvers;
			Metrics = metrics;
			sizeX = 2000;
			sizeY = 1024;
			margin = 35;
			TesterOptions = testerOptions;
		}

		public void DrawComparativeChartsByAccuracy()
		{
			int maxDist = (int)Metrics.Max(x => x.GetRelativeDistanceInPercent);
			int minDist = (int)Metrics.Min(x => x.GetRelativeDistanceInPercent);

			var graph = DrawSkeleton(out Image image, "Comparison by relative distance to perfect point", minDist, maxDist);

			string paramTitle = $"Variable parameter name: {TesterOptions[0].VaryingParameterName}";

			graph.DrawString(paramTitle,
			new Font(new FontFamily("Arial Black"), 20, FontStyle.Regular),
			Brushes.Blue, new PointF((sizeX - 200) / 2, sizeY - margin));

			graph.DrawString("N/C,c/T,t",
			new Font(new FontFamily("Arial Black"), 20, FontStyle.Regular),
			Brushes.Blue, new PointF(sizeX - 250, sizeY - margin));

			DrawRectsForAccuracyComparison(graph);

			image.Save($"{(String.IsNullOrEmpty(TesterOptions[0].Path) ? "last" : TesterOptions[0].Path)}.accuracy.comarison.png", System.Drawing.Imaging.ImageFormat.Png);

		}

		private void DrawRectsForAccuracyComparison(Graphics graph)
		{
			Pen pen = new Pen(Brushes.Black);
			Pen penForLines = new Pen(Brushes.Red);
			penForLines.Width = pen.Width = 3;

			Point[] valPoints = new Point[Metrics.Count];
			var rects = new Rectangle[Metrics.Count];

			int RectLengthX = ((sizeX - 4 * margin) / rects.Length) - margin;
			int curRectX = 2 * margin;

			int maxDist = (int)Metrics.Max(x => x.GetRelativeDistanceInPercent);
			int minDist = (int)Metrics.Min(x => x.GetRelativeDistanceInPercent);
			double scaleStep = Math.Abs((double)(maxDist - minDist)) / 9;

			for (int count = 0; count < rects.Length; count++)
			{
				int curHeight = (int)((((double)Metrics[count].GetRelativeDistanceInPercent - minDist + scaleStep) / (maxDist - minDist + scaleStep)) * (sizeY - 2 * margin));

				rects[count] = new Rectangle(new Point(curRectX, margin + (sizeY - 2 * margin) - curHeight),
					new Size(RectLengthX, curHeight));

				valPoints[count] = new Point(curRectX + RectLengthX / 2, margin + (sizeY - 2 * margin) - curHeight);

				graph.DrawRectangle(pen, rects[count]);
				graph.FillRectangle(Brushes.Green, rects[count]);

				curRectX += RectLengthX + margin;
			}

			graph.DrawLines(penForLines, valPoints);
			curRectX = 2 * margin;
			for (int count = 0; count < rects.Length; count++)
			{
				int curHeight = (int)((((double)Metrics[count].GetRelativeDistanceInPercent - minDist + scaleStep) / (maxDist - minDist + scaleStep)) * (sizeY - 2 * margin));

				graph.DrawString($"{(int)Metrics[count].GetRelativeDistanceInPercent} %, {this.TesterOptions[count]}",
				new Font(new FontFamily("Arial Black"), 20, FontStyle.Bold),
				Brushes.Blue, new PointF(curRectX, margin + ((sizeY - 2 * margin)) - curHeight - 40));
				curRectX += RectLengthX + margin;
			}

		}

		public void DrawComparativeChartsByTime()
		{
			int maxTime = (int)Metrics.Max(x => x.TimeOfWork);
			int minTime = (int)Metrics.Min(x => x.TimeOfWork);

			var graph = DrawSkeleton(out Image image, "Comparison by time", minTime, maxTime);

			string paramTitle = $"Variable parameter name: {TesterOptions[0].VaryingParameterName}";

			graph.DrawString(paramTitle,
			new Font(new FontFamily("Arial Black"), 20, FontStyle.Regular),
			Brushes.Blue, new PointF((sizeX - 200) / 2, sizeY - margin));

			graph.DrawString("N/C,c/T,t",
			new Font(new FontFamily("Arial Black"), 20, FontStyle.Regular),
			Brushes.Blue, new PointF(sizeX - 250, sizeY - margin));

			DrawRectsForTimeComparison(graph);

			image.Save($"{(String.IsNullOrEmpty(TesterOptions[0].Path) ? "last" : TesterOptions[0].Path)}.time.comarison.png", System.Drawing.Imaging.ImageFormat.Png);

		}

		private void DrawRectsForTimeComparison(Graphics graph)
		{
			Pen pen = new Pen(Brushes.Black);
			Pen penForLines = new Pen(Brushes.Red);
			penForLines.Width = pen.Width = 3;

			Point[] valPoints = new Point[Metrics.Count];
			var rects = new Rectangle[Metrics.Count];

			int RectLengthX = ((sizeX - 4 * margin) / rects.Length) - margin;
			int curRectX = 2 * margin;

			int maxTime = (int)Metrics.Max(x => x.TimeOfWork);
			int minTime = (int)Metrics.Min(x => x.TimeOfWork);
			double scaleStep = Math.Abs((double)(maxTime - minTime)) / 9;

			for (int count = 0; count < rects.Length; count++)
			{
				int curHeight = (int)((((double)Metrics[count].TimeOfWork - minTime + scaleStep) / (maxTime - minTime + scaleStep)) * (sizeY - 2 * margin));

				rects[count] = new Rectangle(new Point(curRectX, margin + (sizeY - 2 * margin) - curHeight),
					new Size(RectLengthX, curHeight));

				valPoints[count] = new Point(curRectX + RectLengthX / 2, margin + (sizeY - 2 * margin) - curHeight);

				graph.DrawRectangle(pen, rects[count]);
				graph.FillRectangle(Brushes.Green, rects[count]);

				curRectX += RectLengthX + margin;
			}

			graph.DrawLines(penForLines, valPoints);
			curRectX = 2 * margin;
			for (int count = 0; count < rects.Length; count++)
			{
				int curHeight = (int)((((double)Metrics[count].TimeOfWork - minTime + scaleStep) / (maxTime - minTime + scaleStep)) * (sizeY - 2 * margin));

				graph.DrawString($"{(int)Metrics[count].TimeOfWork} ms, {this.TesterOptions[count]}",
				new Font(new FontFamily("Arial Black"), 20, FontStyle.Bold),
				Brushes.Blue, new PointF(curRectX, margin + ((sizeY - 2 * margin)) - curHeight - 40));
				curRectX += RectLengthX + margin;
			}

		}


		private Graphics DrawSkeleton(out Image image, string title, int minScaleDiv = 0, int maxScaleDiv = 10)
		{

			image = new Bitmap(sizeX, sizeY);

			Graphics graph = Graphics.FromImage(image);

			graph.Clear(Color.Azure);

			Pen pen = new Pen(Brushes.Black);
			pen.Width = 3;

			graph.DrawLines(pen, new Point[] { new Point(margin, margin), new Point(margin, sizeY - margin) });
			graph.DrawLines(pen, new Point[] { new Point(sizeX - margin, sizeY - margin), new Point(margin, sizeY - margin) });

			/*for (int count = 100; count < sizeX - margin; count += 100)
			{

				graph.DrawLines(pen, new Point[] { new Point(margin + count, sizeY - margin - 5), new Point(margin + count, sizeY - margin + 5) });
				graph.DrawString($"{count / 100 }", new Font(new FontFamily("Centaur"), 15, FontStyle.Bold),
					Brushes.Black, new PointF(margin + count, sizeY - margin + 10));
			}*/

			int scaleStepPxl = (sizeY - 2 * margin) / 10;
			double scaleStep = Math.Abs((double)(maxScaleDiv - minScaleDiv)) / 9;
			double scaleCount = minScaleDiv;
			for (int count = scaleStepPxl; count < sizeY - margin; count += scaleStepPxl, scaleCount += scaleStep)
			{
				graph.DrawLines(pen, new Point[] { new Point(margin - 5, sizeY - margin - count), new Point(margin + 5, sizeY - margin - count) });
				graph.DrawString($"{scaleCount:F1}", new Font(new FontFamily("Centaur"), 15, FontStyle.Bold),
					Brushes.Black, new PointF(0, sizeY - margin - count), new StringFormat(StringFormatFlags.DirectionVertical));
			}


			graph.DrawString(title,
			new Font(new FontFamily("Arial Black"), 20, FontStyle.Bold),
			Brushes.Blue, new PointF(margin, sizeY - margin));

			return graph;

		}


	}
}
