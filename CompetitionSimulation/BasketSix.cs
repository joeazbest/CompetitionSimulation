namespace CompetitionSimulation
{
	using System;
	using System.Collections.Generic;
	using Tables;

	public class BasketSix : IBasket
	{
		private readonly string Name;
		private readonly int Order;
		private readonly IDictionary<int, ITeam> BasketInnitial;

		private readonly IEnumerable<IMatch> Matches;
		private readonly IDictionary<int, ITeam> BasketResult;

		public BasketSix(
			string name,
			int order,
			int round,
			IDictionary<int, ITeam> basketInnitial
		)
		{
			if (basketInnitial == null || basketInnitial.Count != 6)
				throw new ArgumentException("Team Count is 6 to need");

			this.Name = name;
			this.Order = order;
			this.BasketInnitial = basketInnitial;

			// TODO poradi je treba si nejak vymyslet
			var table1 = new CompetitionTable(
				new List<ITeam>
				{
					basketInnitial[0],
					basketInnitial[1],
					basketInnitial[4]
				}
			);

			var table2 = new CompetitionTable(
				new List<ITeam>
				{
					basketInnitial[3],
					basketInnitial[3],
					basketInnitial[5]
				}
			);
		}

		public int GetOrder()
		{
			return this.Order;
		}

		public string GetName()
		{
			return this.Name;
		}

		public IEnumerable<IMatch> GetBasketeMatches()
		{
			return this.Matches;
		}

		public IDictionary<int, ITeam> GetBasketResult()
		{
			return this.BasketResult;
		}

		public int GetTeamCount()
		{
			return this.BasketInnitial.Count;
		}
	}
}