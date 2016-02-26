namespace CompetitionSimulation
{
	using System.Collections.Generic;

	public interface IRound
	{
		IDictionary<int, ITeam> GetRoundResult();

		int GetOrder();
	}
}