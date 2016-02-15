namespace CompetitionSimulation
{
	using System;
	using System.Collections.Generic;

	internal class Basket : IBasket
	{
		private readonly string Name;
		private readonly int Order;
		private readonly Dictionary<int, Team> BasketInnitial;

		private readonly SortedList<int, Match> Matches;
		private readonly Dictionary<int, Team> BasketResult;

		internal Basket(
			string name,
			int order,
			Dictionary<int, Team> basketInnitial,
			Func<Dictionary<int, Team>, SortedList<int, Match>> basketMatchSystem,
			Func<Dictionary<int, Team>, SortedList<int, Match>, Dictionary<int, Team>>  getBasketResult
			)
		{
			this.Name = name;
			this.Order = order;
			this.BasketInnitial = basketInnitial;

			this.Matches = basketMatchSystem(basketInnitial);
			this.BasketResult = getBasketResult(this.BasketInnitial, this.Matches);
		}

		public int GetOrder()
		{
			return this.Order;
		}

		public string GetName()
		{
			return this.Name;
		}

		public Dictionary<int, Team> GetBasketResult()
		{
			return this.BasketResult;
		}

		public SortedList<int, Match> GetBasketeMatches()
		{
			return this.Matches;
		}

		public int GetTeamCount()
		{
			return this.BasketInnitial.Count;
		}
	}
}