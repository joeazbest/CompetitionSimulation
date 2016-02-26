namespace CompetitionSimulation.CompetitionTable
{
	using System.Collections.Generic;

	public interface ICompetitionTable
	{
		void AddMatch(IMatch match);

		void AddMatches(IEnumerable<IMatch> matches);

		IEnumerable<IMatch> GetMatches();

		IDictionary<int, ITeam> GetTableResult();
	}
}