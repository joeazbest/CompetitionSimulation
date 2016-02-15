namespace CompetitionSimulation
{
	using System.Collections.Generic;

	internal interface IBasket
	{
		int GetOrder();

		string GetName();

		IDictionary<int, ITeam> GetBasketResult();

		IDictionary<int, IMatch> GetBasketeMatches();

		int GetTeamCount();
	}
}