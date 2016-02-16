namespace CompetitionSimulation
{
	using System.Collections.Generic;

	internal interface IRound
	{
		IDictionary<int, ITeam> GetRoundResult();

		int GetOrder();
	}
}