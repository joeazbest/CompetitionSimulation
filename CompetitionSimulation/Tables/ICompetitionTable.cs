namespace CompetitionSimulation.Tables
{
	using System.Collections.Generic;
	using CompetitionSimulation.Baskets;
	using CompetitionSimulation.Teams;

	public interface ICompetitionTable
	{
		void AddMatch(IMatch match);

		void AddMatches(IEnumerable<IMatch> matches);

		IEnumerable<IMatch> GetMatches();

		IDictionary<int, ITeam> GetTableResult();
	}
}