namespace CompetitionSimulation.Baskets
{
	using System.Collections.Generic;
	using CompetitionSimulation.Teams;

	public interface IBasket
	{
		int Order { get; }
		int Round { get; }
		string Name { get; }
		int BasketTeamCount { get; }

		void AddTeam(int order, ITeam team);

		void AddTeam(int order, ITeam team, int? previousBasketOrder);

		void AddTeams(IDictionary<int, ITeam> teams);

		bool AddTeamFromBottom(ITeam team, int? previousBasketOrder = null);

		bool AddTeamFromTop(ITeam team, int? previousBasketOrder = null);

		IDictionary<int, ITeam> GetBasketIntitialOrder();

		IDictionary<int, ITeam> GetBasketResult();

		IEnumerable<IMatch> GetBasketeMatches();

		IDictionary<ITeam, int> GetPreviousBasketTeam();
	}
}