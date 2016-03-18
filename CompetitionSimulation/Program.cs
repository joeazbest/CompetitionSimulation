namespace CompetitionSimulation
{
	using Algorithms;
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using Teams;

	public class Program
	{
		private static readonly int atemptCount = 10000;
		private static readonly int teamCount = 18;

		public static void Main(string[] args)
		{
			var rnd = new Random();
			var output = new StreamWriter("..\\..\\..\\TxtOutput\\output.txt");

			var optimalVector = new int[teamCount];
			var primaryVector = new int[teamCount];
			var lastVector = new int[teamCount];

			for (var i = 1; i <= atemptCount; i++)      // POCET POKUSU
			{
				// seznam tymu
				var teams = new Dictionary<double, ITeam>();
				for (var teamOrder = 1; teamOrder <= teamCount; teamOrder++)    // POCET TYMU
				{
					var value = teamOrder;  // TODO predpokladam ze dve rnd cisla nebudou v 18ti cislech stejny :-)
					teams.Add(
						rnd.NextDouble(),
						new Team($"{value}", value, x => value) // vsechno je stejny ale to nemusi byt pravda :-)
					);

					optimalVector[teamOrder - 1] = teamCount - teamOrder + 1;
				}

				// pridam poradatelstvi - kazdemu jednou
				var teamInput = teams.OrderBy(t => t.Key).Select(t => t.Value).ToList();
				var order = 1;
				var basket = 1;
				var fullOrder = 0;

				foreach (var team in teamInput)
				{
					// output.Write("{0} ", team.Name);

					team.AddOrganizer(new Organizer(order, basket.ToString()));
					team.AddOrganizer(new Organizer(order + 6, basket.ToString()));
					order++;
					if (order == 7)
					{
						order = 1;
						basket++;
					}

					primaryVector[fullOrder] = team.OptimalOrder;
					fullOrder++;
				}

				// projedu jednotlivy algoritmy
				var alg1 = new NonOrganizerAlgorithm(teamInput);
				var alg2 = new OrganizerPriorityAlgorithm(teamInput);
				var alg3 = new DynamicOrganizerAlgorithm(teamInput);

				var basket1First = alg1.CreateInitialBasket();
				var basket2First = alg2.CreateInitialBasket();
				var basket3First = alg3.CreateInitialBasket();

				for (var round = 1; round <= 7; round++)       // POCET KOL + 1
				{
					var basket1Second = alg1.GetNextBasketComposition(basket1First);
					var basket2Second = alg2.GetNextBasketComposition(basket2First);
					var basket3Second = alg3.GetNextBasketComposition(basket3First);

					basket1First = basket1Second;
					basket2First = basket2Second;
					basket3First = basket3Second;
				}

				output.Write("{0}\t", VectorDiff(optimalVector, primaryVector));

				FillLastVector(
					lastVector,
					alg1.GetTeamFinalOrder(basket1First)
				);
				output.Write("{0}\t", VectorDiff(optimalVector, lastVector));

				FillLastVector(
					lastVector,
					alg2.GetTeamFinalOrder(basket2First)
				);
				output.Write("{0}\t", VectorDiff(optimalVector, lastVector));

				FillLastVector(
					lastVector,
					alg3.GetTeamFinalOrder(basket3First)
				);
				output.Write("{0}\t", VectorDiff(optimalVector, lastVector));

				//output.Write(" | ");
				//foreach (var team in alg1.GetTeamFinalOrder(basket1First))
				//{
				//	output.Write("{0} ", team.Name);
				//}
				//output.Write(" | ");
				//foreach (var team in alg2.GetTeamFinalOrder(basket2First))
				//{
				//	output.Write("{0} ", team.Name);
				//}
				//output.Write(" | ");
				//foreach (var team in alg3.GetTeamFinalOrder(basket3First))
				//{
				//	output.Write("{0} ", team.Name);
				//}

				output.WriteLine();
				output.Flush();
			}

			output.Close();
		}

		// TODO rename
		private static void FillLastVector(
			int[] outputVector,
			IList<ITeam> teamOutput
		)
		{
			var lastOrder = 0;
			foreach (var team in teamOutput)
			{
				outputVector[lastOrder] = team.OptimalOrder;
				lastOrder++;
			}
		}

		// TODO rename
		private static double VectorDiff(
			int[] vector1,
			int[] vector2
		)
		{
			if (vector1 == null || vector2 == null)
				throw new ArgumentNullException("Vector");

			if (vector1.Count() != vector2.Count())
				throw new ArgumentException("Different vector count");

			var output = 0d;
			for (var i = 0; i < vector1.Count(); i++)
			{
				var diff = vector1[i] - vector2[i];
				output += Math.Pow(2, diff);
			}
			return Math.Sqrt(output);
		}
	}
}