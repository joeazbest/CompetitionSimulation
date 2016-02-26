namespace CompetitionSimulation
{
	using System.Collections.Generic;

	public interface IBasket
	{
		int GetOrder();

		string GetName();

		IDictionary<int, ITeam> GetBasketResult();

		IEnumerable<IMatch> GetBasketeMatches();

		int GetTeamCount();
	}
}