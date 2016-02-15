namespace CompetitionSimulation
{
	using System.Collections.Generic;
	using System.Linq;

	internal class Program
	{
		private static void Main(string[] args)
		{
			// nacteni seznamu tymu - vim seznam typu a vim jejich funkci, kterea urcuje jejich silu v prubehu casovych useku (kola)
			var teams = new Dictionary<int, Team>();
			for (var i = 1; i <= 36; i++)
			{
				teams.Add(i, new Team(i.ToString(), arg => i)); // zatim jen blbustka, tak funkce musi byt nacitane z nejakych konfiguraku
			}

			// prvotni rozzareni do skupin - prozatim udelane jen jednoduse druhotne pak na to bude muset byt opet fce

			// jejich rozdeleni do skupin

			// provadeni jednotlivych kol
		}
	}

	internal class Round : IRound
	{
		private readonly int Order;
		private readonly IList<IBasket> Baskets;

		internal Round(
			int order,
			IList<IBasket> baskets
		)
		{
			this.Order = order;
			this.Baskets = baskets;
		}

		public IDictionary<int, ITeam> GetRoundResult()
		{
			var output = new Dictionary<int, ITeam>();
			var order = 1;
			foreach (var basket in this.Baskets.OrderBy(t => t.GetOrder()))
			{
				foreach (var team in basket.GetBasketResult())
				{
					output.Add(order, team.Value);
					order++;
				}
			}

			return output;
		}

		public int GetOrder()
		{
			return this.Order;
		}
	}

	internal interface IRound
	{
		IDictionary<int, ITeam> GetRoundResult();

		int GetOrder();
	}
}