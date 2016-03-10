namespace CompetitionSimulation.Tables
{
	using System.Collections.Generic;
	using Baskets;
	using Teams;

	public interface ICompetitionTable
	{
		void AddMatch(IMatch match);

		void AddMatches(IEnumerable<IMatch> matches);

		IEnumerable<IMatch> GetMatches();

		IDictionary<int, ITeam> GetTableResult();
	}
}