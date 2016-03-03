namespace CompetitionSimulation
{
	using System.Collections.Generic;

	public interface IRound
	{
		int Order { get; }

		IDictionary<int, ITeam> GetRoundResult();
	}
}