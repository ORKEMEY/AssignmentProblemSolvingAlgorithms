using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Infrastructure;
using System.Linq;
using GeneticAlgorithm;

namespace Tests
{
	public class MainChartPainter : ChartPainter
	{

		public MainChartPainter(List<AssignmentProblemResolver<SquareAssignmentProblem>> resolvers, List<ProblemResolvedEventArgs> metrics, List<TesterOptions> testerOptions)
		: base(resolvers, metrics, testerOptions)
		{
		}

		public void DrawChartsAccuracyByIterations(List<GeneticAlgEventArgs> iterationMetrics, TesterOptions testerOptions)
		{
			int maxDist = (int)iterationMetrics.Max(x => x.BestRelativeDistanceToPerfectPointInPercent);
			int minDist = (int)iterationMetrics.Min(x => x.BestRelativeDistanceToPerfectPointInPercent);

			int maxIteration = (int)iterationMetrics.Max(x => x.NumberOfIteration);
			int minIteration = (int)iterationMetrics.Min(x => x.NumberOfIteration);

			var graph = DrawSkeleton(out Image image, "Comparison by relative distance to perfect point on each iteration", minIteration, maxIteration, minDist, maxDist);

			graph.DrawString($"{testerOptions}",
			new Font(new FontFamily("Arial Black"), 20, FontStyle.Regular),
			Brushes.Blue, new PointF(sizeX - 350, 2));

			graph.DrawString("%",
			new Font(new FontFamily("Arial Black"), 20, FontStyle.Regular),
			Brushes.Blue, new PointF(2, 2));

			graph.DrawString("№",
			new Font(new FontFamily("Arial Black"), 20, FontStyle.Regular),
			Brushes.Blue, new PointF(sizeX - 70, sizeY - 2 * margin - 20));

			DrawLinesForAccuracyComparison(graph, iterationMetrics);

			image.Save($"{(String.IsNullOrEmpty(TesterOptions[0].Path) ? "last" : TesterOptions[0].Path)}.{testerOptions.ToStringWithoutSlash()}.iteration.accuracy.comarison.png", System.Drawing.Imaging.ImageFormat.Png);

		}

		private void DrawLinesForAccuracyComparison(Graphics graph, List<GeneticAlgEventArgs> iterationMetrics)
		{
			Pen pen = new Pen(Brushes.Black);
			Pen penForLines = new Pen(Brushes.Red);
			penForLines.Width = pen.Width = 3;

			Point[] valPoints = new Point[iterationMetrics.Count];

			int maxDist = iterationMetrics.Max(x => x.BestRelativeDistanceToPerfectPointInPercent);
			int minDist = iterationMetrics.Min(x => x.BestRelativeDistanceToPerfectPointInPercent);

			int maxIteration = iterationMetrics.Max(x => x.NumberOfIteration);
			int minIteration = iterationMetrics.Min(x => x.NumberOfIteration);

			double scaleYStep = Math.Abs((double)(maxDist - minDist)) / 9;


			int currrentX = margin, scaleXStepPxl = (int)Math.Round((decimal)(sizeX - 2 * margin) / Math.Abs((maxIteration - minIteration)));

			for (int count = 0; count < valPoints.Length; count++)
			{
				int curHeight = (int)((((double)iterationMetrics[count].BestRelativeDistanceToPerfectPointInPercent - minDist + scaleYStep) / (maxDist - minDist + scaleYStep)) * (sizeY - 2 * margin));

				if (currrentX + (count * scaleXStepPxl) < sizeX - margin)
					valPoints[count] = new Point(currrentX + (count * scaleXStepPxl), sizeY - margin - curHeight);
				else
					valPoints[count] = new Point(sizeX - margin, sizeY - margin - curHeight);
			}

			graph.DrawLines(penForLines, valPoints);
		}

		public void DrawComparativeChartsByAccuracy(string remark = "")
		{
			int maxDist = (int)Metrics.Max(x => x.GetRelativeDistanceInPercent);
			int minDist = (int)Metrics.Min(x => x.GetRelativeDistanceInPercent);

			var graph = DrawSkeleton(out Image image, "Comparison by relative distance to perfect point", minDist, maxDist);

			string paramTitle = $"Variable parameter name: {TesterOptions[0].VaryingParameterName}";

			graph.DrawString(paramTitle,
			new Font(new FontFamily("Arial Black"), 20, FontStyle.Regular),
			Brushes.Blue, new PointF((sizeX - 200) / 2, sizeY - margin));

			graph.DrawString("N/C,c/T,t/p",
			new Font(new FontFamily("Arial Black"), 20, FontStyle.Regular),
			Brushes.Blue, new PointF(sizeX - 250, sizeY - margin));

			graph.DrawString(remark,
			new Font(new FontFamily("Arial Black"), 20, FontStyle.Regular),
			Brushes.Blue, new PointF(margin, sizeY - margin));

			graph.DrawString("%",
			new Font(new FontFamily("Arial Black"), 20, FontStyle.Regular),
			Brushes.Blue, new PointF(2, 2));

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

				graph.DrawString($"{(int)Metrics[count].GetRelativeDistanceInPercent} %, " +
					$"{this.TesterOptions[count]}",
				new Font(new FontFamily("Arial Black"), 20, FontStyle.Bold),
				Brushes.Blue, new PointF(curRectX, margin + ((sizeY - 2 * margin)) - curHeight - 40));
				curRectX += RectLengthX + margin;
			}

		}

		public void DrawComparativeChartsByTime(string remark = "")
		{
			int maxTime = (int)Metrics.Max(x => x.TimeOfWork);
			int minTime = (int)Metrics.Min(x => x.TimeOfWork);

			var graph = DrawSkeleton(out Image image, "Comparison by time", minTime, maxTime);

			string paramTitle = $"Variable parameter name: {TesterOptions[0].VaryingParameterName}";

			graph.DrawString(paramTitle,
			new Font(new FontFamily("Arial Black"), 20, FontStyle.Regular),
			Brushes.Blue, new PointF((sizeX - 200) / 2, sizeY - margin));

			graph.DrawString("N/C,c/T,t/p",
			new Font(new FontFamily("Arial Black"), 20, FontStyle.Regular),
			Brushes.Blue, new PointF(sizeX - 250, sizeY - margin));

			/*graph.DrawString(remark,
			new Font(new FontFamily("Arial Black"), 20, FontStyle.Regular),
			Brushes.Blue, new PointF(margin, sizeY - margin));*/

			graph.DrawString("ms",
			new Font(new FontFamily("Arial Black"), 20, FontStyle.Regular),
			Brushes.Blue, new PointF(2, 2));

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

				graph.DrawString($"{(int)Metrics[count].TimeOfWork} ms, " +
					$"{this.TesterOptions[count]}",
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
			Brushes.Blue, new PointF(sizeX - 2 * margin, margin), new StringFormat(StringFormatFlags.DirectionVertical));

			return graph;

		}
	}
}
