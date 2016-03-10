namespace CompetitionSimulation
{
	using System.Collections.Generic;
	using CompetitionSimulation.Teams;

	public interface IRound
	{
		int Order { get; }

		IDictionary<int, ITeam> GetRoundResult();
	}
}