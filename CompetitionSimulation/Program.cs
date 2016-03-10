namespace CompetitionSimulation
{
	using CompetitionSimulation.Algorithms;
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using CompetitionSimulation.Teams;

	public class Program
	{
		public static void Main(string[] args)
		{
			var rnd = new Random();
			var output = new StreamWriter("..\\..\\..\\TxtOutput\\output.txt");

			for (var i = 1; i <= 1000; i++)
			{
				// seznam tymu
				var teams = new Dictionary<double, ITeam>();
				for (var teamOrder = 1; teamOrder <= 18; teamOrder++)
				{
					var value = teamOrder;  // predpokladam ze dve rnd cisla nebudou v 18ti cisle stejny :-)
					teams.Add(
						rnd.NextDouble(),
						new Team($"Team {teamOrder}", x => value)
					);
				}

				// pridam poradatelstvi
				var teamInput = teams.OrderBy(t => t.Key).Select(t => t.Value).ToList();
				var order = 1;
				var basket = 1;
				foreach (var team in teamInput)
				{
					output.Write("{0} ", team.Name);

					team.AddOrganizer(new Organizer(order, basket.ToString()));
					order++;
					if (order == 7)
					{
						order = 1;
						basket++;
					}
				}
				output.WriteLine();

				// projedu jednotlivy algoritmy
				var alg1 = new PrimitiveAlgorithm(teamInput);
				var alg2 = new OrganizerPriorityAlgorithm(teamInput);

				var basket1First = alg1.CreateInitialBasket();
				var basket2First = alg2.CreateInitialBasket();

				for (var round = 1; round <= 5; round++)
				{
					var basket1Second = alg1.GetNextBasketComposition(basket1First);
					var basket2Second = alg2.GetNextBasketComposition(basket2First);

					basket1First = basket1Second;
					basket2First = basket2Second;
				}
			}

			output.Close();
		}
	}
}