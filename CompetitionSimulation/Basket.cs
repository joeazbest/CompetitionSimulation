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
			int round,
			IDictionary<int, ITeam> basketInnitial,
			ICompetitionTable basketResults
			//Func<IDictionary<int, ITeam>, int, IDictionary<int, IMatch>> basketMatchSystem,
			//Func<IDictionary<int, ITeam>, IDictionary<int, IMatch>, IDictionary<int, ITeam>>  getBasketResult
			)
		{
			this.Name = name;
			this.Order = order;
			this.BasketInnitial = basketInnitial;

			this.Matches = basketResults.GetMatches();
			this.BasketResult = basketResults.GetTableResult();
		}

		public int GetOrder()
		{
			return this.Order;
		}

		public string GetName()
		{
			return this.Name;
		}

		public IDictionary<int, IMatch> GetBasketeMatches()
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

	internal interface ICompetitionTable
	{
		IDictionary<int, IMatch> GetMatches();

		IDictionary<int, ITeam> GetTableResult();
	}

	internal class CompetitionSixTable : ICompetitionTable
	{
		internal CompetitionSixTable(
			IDictionary<int, ITeam> inputTeamOrder
		)
		{
		}

		public IDictionary<int, IMatch> GetMatches()
		{
			throw new NotImplementedException();
		}

		public IDictionary<int, ITeam> GetTableResult()
		{
			throw new NotImplementedException();
		}
	}
}