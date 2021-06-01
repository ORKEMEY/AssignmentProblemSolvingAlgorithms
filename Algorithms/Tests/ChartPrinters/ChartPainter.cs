using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithm;
using System.Drawing;
using Infrastructure;

namespace Tests
{

	public abstract class ChartPainter
	{

		public List<AssignmentProblemResolver<SquareAssignmentProblem>> Resolvers { get; protected set; }
		public List<ProblemResolvedEventArgs> Metrics { get; protected set; }
		public List<TesterOptions> TesterOptions { get; set; }
		protected int sizeX, sizeY, margin;


		public ChartPainter(List<AssignmentProblemResolver<SquareAssignmentProblem>> resolvers, List<ProblemResolvedEventArgs> metrics, List<TesterOptions> testerOptions)
		{
			Resolvers = resolvers;
			Metrics = metrics;
			sizeX = 2000;
			sizeY = 1024;
			margin = 35;
			TesterOptions = testerOptions;
		}

		protected Graphics DrawSkeleton(out Image image, string title, int minScaleXDiv = 0, int maxScaleXDiv = 10, int minScaleYDiv = 0, int maxScaleYDiv = 10, int numberOfMarksX = 25)
		{

			image = new Bitmap(sizeX, sizeY);

			Graphics graph = Graphics.FromImage(image);

			graph.Clear(Color.Azure);

			Pen pen = new Pen(Brushes.Black);
			pen.Width = 3;

			graph.DrawLines(pen, new Point[] { new Point(margin, margin), new Point(margin, sizeY - margin) });
			graph.DrawLines(pen, new Point[] { new Point(sizeX - margin, sizeY - margin), new Point(margin, sizeY - margin) });


			int scaleXStep = (int)Math.Round(((decimal)Math.Abs(maxScaleXDiv - minScaleXDiv) / numberOfMarksX));

			int scaleXStepPxl = (sizeX - 2 * margin) / numberOfMarksX;
			double scaleXCount = minScaleXDiv + scaleXStep;
			for (int count = scaleXStepPxl; count < sizeX - margin; count += scaleXStepPxl, scaleXCount += scaleXStep)
			{
				graph.DrawLines(pen, new Point[] { new Point(margin + count, sizeY - margin - 5), new Point(margin + count, sizeY - margin + 5) });
				graph.DrawString($"{(int)scaleXCount}", new Font(new FontFamily("Centaur"), 15, FontStyle.Bold),
					Brushes.Black, new PointF(margin + count, sizeY - margin + 10));
			}

			int scaleYStepPxl = (sizeY - 2 * margin) / 10;
			double scaleYStep = Math.Abs((double)(maxScaleYDiv - minScaleYDiv)) / 9;
			double scaleYCount = minScaleYDiv;
			for (int count = scaleYStepPxl; count < sizeY - margin; count += scaleYStepPxl, scaleYCount += scaleYStep)
			{
				graph.DrawLines(pen, new Point[] { new Point(margin - 5, sizeY - margin - count), new Point(margin + 5, sizeY - margin - count) });
				graph.DrawString($"{scaleYCount:F1}", new Font(new FontFamily("Centaur"), 15, FontStyle.Bold),
					Brushes.Black, new PointF(0, sizeY - margin - count), new StringFormat(StringFormatFlags.DirectionVertical));
			}

			graph.DrawString(title,
			new Font(new FontFamily("Arial Black"), 20, FontStyle.Bold),
			Brushes.Blue, new PointF(2 * margin, 2));

			return graph;

		}

	}

}
