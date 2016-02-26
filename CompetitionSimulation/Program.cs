namespace CompetitionSimulation
{
	using CompetitionSimulation.Algorithm;
	using System.Collections.Generic;

	public class Program
	{
		// TODO - poradatelstvi
		public static void Main(string[] args)
		{
			var roundCount = 6;
			// nacteni seznamu tymu - vim seznam typu a vim jejich funkci, kterea urcuje jejich silu v prubehu casovych useku (kola)
			var teams = new List<ITeam>();
			for (var i = 1; i <= 36; i++)
			{
				teams.Add(new Team(i.ToString(), arg => i)); // zatim jen blbustka, tak funkce musi byt nacitane z nejakych konfiguraku
			}

			var alg = new PrimitiveAlgorithm();

			// prvotni rozzareni do skupin
			var innitialBasket = alg.CreateInitialBasket(teams);

			var rounds = new Dictionary<int, Round>();
			rounds.Add(1, new Round(1, innitialBasket));

			// provadeni jednotlivych kol
			for (var roundOrder = 2; roundOrder <= roundCount; roundOrder++)
			{
				var previousRoundResult = rounds[roundOrder - 1].GetRoundResult();

				rounds.Add(roundOrder, new Round(roundOrder, alg.CreateRoundBasket(previousRoundResult)));
			}
		}

		private static IList<IBasket> CreateInnitionalBasket(
			IList<ITeam> teams
		)
		{
			var output = new List<IBasket>();

			return output;
		}
	}
}