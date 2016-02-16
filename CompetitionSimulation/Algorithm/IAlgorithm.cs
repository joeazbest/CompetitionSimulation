namespace CompetitionSimulation.Algorithm
{
	using System.Collections.Generic;

	internal interface IAlgorithm
	{
		IList<IBasket> CreateInitialBastet(IList<ITeam> teams);

		IDictionary<int, IMatch> ComputeMatches(IDictionary<int, ITeam> teams);

		IDictionary<int, ITeam> ComputeTeamOrder(IDictionary<int, ITeam> teams, IDictionary<int, IMatch> matches);
	}
}