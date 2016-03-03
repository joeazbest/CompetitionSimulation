namespace CompetitionSimulation.Baskets
{
	using System.Collections.Generic;

	public interface IBasket
	{
		int Order { get; }
		int Round { get; }
		string Name { get; }

		void AddTeam(int order, ITeam team);

		void AddTeams(IDictionary<int, ITeam> teams);

		IDictionary<int, ITeam> GetBasketIntitialOrder();

		IDictionary<int, ITeam> GetBasketResult();

		IEnumerable<IMatch> GetBasketeMatches();
	}
}