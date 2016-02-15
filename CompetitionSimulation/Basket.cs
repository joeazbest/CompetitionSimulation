namespace CompetitionSimulation
{
	using System;
	using System.Collections.Generic;

	internal class Basket : IBasket
	{
		private readonly string Name;
		private readonly int Order;
		private readonly IDictionary<int, ITeam> BasketInnitial;

		private readonly IDictionary<int, IMatch> Matches;
		private readonly IDictionary<int, ITeam> BasketResult;

		internal Basket(
			string name,
			int order,
			IDictionary<int, ITeam> basketInnitial,
			Func<IDictionary<int, ITeam>, IDictionary<int, IMatch>> basketMatchSystem,
			Func<IDictionary<int, ITeam>, IDictionary<int, IMatch>, IDictionary<int, ITeam>>  getBasketResult
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

		public IDictionary<int, ITeam> GetBasketResult()
		{
			return this.BasketResult;
		}

		public IDictionary<int, IMatch> GetBasketeMatches()
		{
			return this.Matches;
		}

		public int GetTeamCount()
		{
			return this.BasketInnitial.Count;
		}
	}
}