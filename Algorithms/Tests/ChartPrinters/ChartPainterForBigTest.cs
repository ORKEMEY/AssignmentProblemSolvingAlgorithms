using System;
using System.Collections.Generic;
using System.Drawing;
using Infrastructure;
using System.Linq;

namespace Tests
{
	public class ChartPainterForBigTest : ChartPainter
	{

		public List<ProblemResolvedEventArgs> MetricsFromGeneticAlg { get; protected set; }

		public ChartPainterForBigTest(List<AssignmentProblemResolver<SquareAssignmentProblem>> resolvers, List<ProblemResolvedEventArgs> metricsFromHunAlg, List<ProblemResolvedEventArgs> metricsFromGenAlg, List<TesterOptions> testerOptions)
		: base(resolvers, metricsFromHunAlg, testerOptions)
		{		
			MetricsFromGeneticAlg = metricsFromGenAlg;
		}

		public void DrawComparativeLineChartsByAccuracy(string remark = "")
		{
			int maxTimeHun = (int)Metrics.Max(x => x.GetRelativeDistanceInPercent), maxTimeGen = (int)MetricsFromGeneticAlg.Max(x => x.GetRelativeDistanceInPercent);
			int minTimeHun = (int)Metrics.Min(x => x.GetRelativeDistanceInPercent), minTimeGen = (int)MetricsFromGeneticAlg.Min(x => x.GetRelativeDistanceInPercent);

			int maxTime = maxTimeHun > maxTimeGen ? maxTimeHun : maxTimeGen;
			int minTime = minTimeHun < minTimeGen ? minTimeHun : minTimeGen;

			int maxSize = Resolvers.Max(x => x.Problem.Size);
			int minSize = Resolvers.Min(x => x.Problem.Size);

			var graph = DrawSkeleton(out Image image, "Comparison of resolving problems by accuracy", minSize, maxSize, minTime, maxTime, (Metrics.Count - 1));

			graph.DrawString("%",
			new Font(new FontFamily("Arial Black"), 20, FontStyle.Regular),
			Brushes.Blue, new PointF(2, 2));

			graph.DrawString("size",
			new Font(new FontFamily("Arial Black"), 20, FontStyle.Regular),
			Brushes.Blue, new PointF(sizeX - 100, sizeY - 2 * margin - 20));

			graph.DrawString("Hungarian",
			new Font(new FontFamily("Arial Black"), 20, FontStyle.Regular),
			Brushes.Red, new PointF(1000, 2));

			graph.DrawString("Genetic",
			new Font(new FontFamily("Arial Black"), 20, FontStyle.Regular),
			Brushes.Blue, new PointF(1200, 2));

			graph.DrawString($"{TesterOptions[0].ToStringWithoutSize()}",
			new Font(new FontFamily("Arial Black"), 20, FontStyle.Regular),
			Brushes.Blue, new PointF(1400, 2));

			DrawLinesForAccuracyComparison(graph);

			image.Save($"{(String.IsNullOrEmpty(TesterOptions[0].Path) ? "last" : TesterOptions[0].Path)}.big.dimensional.accuracy.comarison.png", System.Drawing.Imaging.ImageFormat.Png);

		}

		private void DrawLinesForAccuracyComparison(Graphics graph)
		{
			Pen pen = new Pen(Brushes.Black);
			Pen penForLines = new Pen(Brushes.Red);
			penForLines.Width = pen.Width = 3;

			Point[] valPointsHun = new Point[Metrics.Count];
			Point[] valPointsGen = new Point[MetricsFromGeneticAlg.Count];

			int maxDistHun = (int)Metrics.Max(x => x.GetRelativeDistanceInPercent), maxDistGen = (int)MetricsFromGeneticAlg.Max(x => x.GetRelativeDistanceInPercent);
			int minDistHun = (int)Metrics.Min(x => x.GetRelativeDistanceInPercent), minDistGen = (int)MetricsFromGeneticAlg.Min(x => x.GetRelativeDistanceInPercent);

			int maxDist = maxDistHun > maxDistGen ? maxDistHun : maxDistGen;
			int minDist = minDistHun < minDistGen ? minDistHun : minDistGen;

			int maxSize = Resolvers.Max(x => x.Problem.Size);
			int minSize = Resolvers.Min(x => x.Problem.Size);

			double scaleYStep = Math.Abs((double)(maxDist - minDist)) / 9;

			int scaleXStepPxl = (int)(sizeX - 2 * margin) / (Metrics.Count - 1);

			for (int count = 0; count < Metrics.Count; count++)
			{
				int curHeight = (int)((((double)Metrics[count].GetRelativeDistanceInPercent - minDist + scaleYStep) / (maxDist - minDist + scaleYStep)) * (sizeY - 2 * margin));

				if (margin + (count * scaleXStepPxl) < sizeX - margin)
					valPointsHun[count] = new Point(margin + (count * scaleXStepPxl), sizeY - margin - curHeight);
				else
					valPointsHun[count] = new Point(sizeX - margin, sizeY - margin - curHeight);
			}

			graph.DrawLines(penForLines, valPointsHun);

			penForLines.Brush = Brushes.Blue;
			scaleXStepPxl = (int)(sizeX - 2 * margin) / (MetricsFromGeneticAlg.Count - 1);
			for (int count = 0; count < MetricsFromGeneticAlg.Count; count++)
			{
				int curHeight = (int)((((double)MetricsFromGeneticAlg[count].GetRelativeDistanceInPercent - minDist + scaleYStep) / (maxDist - minDist + scaleYStep)) * (sizeY - 2 * margin));

				if (margin + (count * scaleXStepPxl) < sizeX - margin)
					valPointsGen[count] = new Point(margin + (count * scaleXStepPxl), sizeY - margin - curHeight);
				else
					valPointsGen[count] = new Point(sizeX - margin, sizeY - margin - curHeight);
			}

			graph.DrawLines(penForLines, valPointsGen);


		}

		public void DrawComparativeLineChartsByTime(string remark = "")
		{
			int maxTimeHun = (int)Metrics.Max(x => x.TimeOfWork), maxTimeGen = (int)MetricsFromGeneticAlg.Max(x => x.TimeOfWork);
			int minTimeHun = (int)Metrics.Min(x => x.TimeOfWork), minTimeGen = (int)MetricsFromGeneticAlg.Min(x => x.TimeOfWork);

			int maxTime = maxTimeHun > maxTimeGen ? maxTimeHun : maxTimeGen;
			int minTime = minTimeHun < minTimeGen ? minTimeHun : minTimeGen;

			int maxSize = Resolvers.Max(x => x.Problem.Size);
			int minSize = Resolvers.Min(x => x.Problem.Size);

			var graph = DrawSkeleton(out Image image, "Comparison of resolving problems by time", minSize, maxSize, minTime, maxTime, (Metrics.Count - 1));

			graph.DrawString("ms",
			new Font(new FontFamily("Arial Black"), 20, FontStyle.Regular),
			Brushes.Blue, new PointF(2, 2));

			graph.DrawString("size",
			new Font(new FontFamily("Arial Black"), 20, FontStyle.Regular),
			Brushes.Blue, new PointF(sizeX - 100, sizeY - 2 * margin - 20));

			graph.DrawString("Hungarian",
			new Font(new FontFamily("Arial Black"), 20, FontStyle.Regular),
			Brushes.Red, new PointF(1000, 2));

			graph.DrawString("Genetic",
			new Font(new FontFamily("Arial Black"), 20, FontStyle.Regular),
			Brushes.Blue, new PointF(1200, 2));

			graph.DrawString($"{TesterOptions[0].ToStringWithoutSize()}",
			new Font(new FontFamily("Arial Black"), 20, FontStyle.Regular),
			Brushes.Blue, new PointF(1400, 2));

			DrawLinesForTimeComparison(graph);

			image.Save($"{(String.IsNullOrEmpty(TesterOptions[0].Path) ? "last" : TesterOptions[0].Path)}.big.dimensional.time.comarison.png", System.Drawing.Imaging.ImageFormat.Png);

		}

		private void DrawLinesForTimeComparison(Graphics graph)
		{
			Pen pen = new Pen(Brushes.Black);
			Pen penForLines = new Pen(Brushes.Red);
			penForLines.Width = pen.Width = 3;

			Point[] valPointsHun = new Point[Metrics.Count];
			Point[] valPointsGen = new Point[MetricsFromGeneticAlg.Count];

			int maxDistHun = (int)Metrics.Max(x => x.TimeOfWork), maxDistGen = (int)MetricsFromGeneticAlg.Max(x => x.TimeOfWork);
			int minDistHun = (int)Metrics.Min(x => x.TimeOfWork), minDistGen = (int)MetricsFromGeneticAlg.Min(x => x.TimeOfWork);

			int maxDist = maxDistHun > maxDistGen ? maxDistHun : maxDistGen;
			int minDist = minDistHun < minDistGen ? minDistHun : minDistGen;

			int maxSize = Resolvers.Max(x => x.Problem.Size);
			int minSize = Resolvers.Min(x => x.Problem.Size);

			double scaleYStep = Math.Abs((double)(maxDist - minDist)) / 9;

			int scaleXStepPxl = (int)(sizeX - 2 * margin) / (Metrics.Count - 1);

			for (int count = 0; count < Metrics.Count; count++)
			{
				int curHeight = (int)((((double)Metrics[count].TimeOfWork - minDist + scaleYStep) / (maxDist - minDist + scaleYStep)) * (sizeY - 2 * margin));

				if (margin + (count * scaleXStepPxl) < sizeX - margin)
					valPointsHun[count] = new Point(margin + (count * scaleXStepPxl), sizeY - margin - curHeight);
				else
					valPointsHun[count] = new Point(sizeX - margin, sizeY - margin - curHeight);
			}

			graph.DrawLines(penForLines, valPointsHun);

			penForLines.Brush = Brushes.Blue;
			scaleXStepPxl = (int)(sizeX - 2 * margin) / (MetricsFromGeneticAlg.Count - 1);
			for (int count = 0; count < MetricsFromGeneticAlg.Count; count++)
			{
				int curHeight = (int)((((double)MetricsFromGeneticAlg[count].TimeOfWork - minDist + scaleYStep) / (maxDist - minDist + scaleYStep)) * (sizeY - 2 * margin));

				if (margin + (count * scaleXStepPxl) < sizeX - margin)
					valPointsGen[count] = new Point(margin + (count * scaleXStepPxl), sizeY - margin - curHeight);
				else
					valPointsGen[count] = new Point(sizeX - margin, sizeY - margin - curHeight);
			}

			graph.DrawLines(penForLines, valPointsGen);


		}

	}
}
