namespace CompetitionSimulation
{
	using System.Collections.Generic;

	internal interface IBasket
	{
		int GetOrder();

		string GetName();

		Dictionary<int, Team> GetBasketResult();

		SortedList<int, Match> GetBasketeMatches();

		int GetTeamCount();
	}
}