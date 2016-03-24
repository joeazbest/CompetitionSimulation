namespace CompetitionSimulationWPF
{
	using Microsoft.Research.DynamicDataDisplay;
	using Microsoft.Research.DynamicDataDisplay.DataSources;
	using Microsoft.Research.DynamicDataDisplay.PointMarkers;
	using System;
	using System.Collections.Generic;
	using System.Windows;
	using System.Windows.Media;

	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			for (var teamCount = 18; teamCount <= 36; teamCount = teamCount + 6)
			{
				TeamCount.Items.Add(teamCount);
			}

			for (var round = 6; round <= 18; round = round + 6)
			{
				RoundCount.Items.Add(round);
			}

			//plotter.AddLineGraph(ds, Colors.Green, 2, "Volts"); // to use this method you need "using Microsoft.Research.DynamicDataDisplay;"
		}

		private void GenerateButtonClick(object sender, RoutedEventArgs e)
		{
			var points1 = new List<Tuple<double, double>>();
			var points2 = new List<Tuple<double, double>>();
			var points3 = new List<Tuple<double, double>>();
			for (int i = 0; i < 1000; i++)
			{
				points1.Add(new Tuple<double, double>(i * Math.PI / 50, Math.Cos(i * Math.PI / 50)));
				points2.Add(new Tuple<double, double>(i * Math.PI / 50, Math.Cos(i * Math.PI / 50 + 0.01)));
			}

			var ds1 = new EnumerableDataSource<Tuple<double, double>>(points1);
			ds1.SetXMapping(x => x.Item1);
			ds1.SetYMapping(y => y.Item2);

			var ds2 = new EnumerableDataSource<Tuple<double, double>>(points2);
			ds2.SetXMapping(x => x.Item1);
			ds2.SetYMapping(y => y.Item2);

			plotter.AddLineGraph(ds1, null, new CirclePointMarker { Size = 5, Fill = Brushes.Red }, null);
			plotter.AddLineGraph(ds2, null, new CirclePointMarker { Size = 5, Fill = Brushes.Green }, null);
		}
	}
}