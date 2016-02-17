namespace CompetitionSimulation.Algorithm
{
	using System.Collections.Generic;

	internal interface IAlgorithm
	{
		IList<IBasket> CreateInitialBasket(IList<ITeam> teams);

		IDictionary<int, IMatch> ComputeBasketMatches(
			IDictionary<int, ITeam> oneBasket,
			int round
		);

		IDictionary<int, ITeam> ComputeBasketTeamOrder(
			IDictionary<int, ITeam> oneBasket,
			IDictionary<int, IMatch> basketMatches
		);
	}
}