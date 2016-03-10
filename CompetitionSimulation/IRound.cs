namespace CompetitionSimulation
{
	using System.Collections.Generic;
	using Teams;

	public interface IRound
	{
		int Order { get; }

		IDictionary<int, ITeam> GetRoundResult();
	}
}