namespace CompetitionSimulation.Algorithm
{
	using System.Collections.Generic;

	internal abstract class Algorithm : IAlgorithm
	{
		public abstract IList<IBasket> CreateInitialBasket(
			IList<ITeam> teams
		);

		//public abstract IDictionary<int, IMatch> ComputeBasketMatches(
		//	IDictionary<int, ITeam> oneBasket,
		//	int round
		//);

		//public abstract IDictionary<int, ITeam> ComputeBasketTeamOrder(
		//	IDictionary<int, ITeam> oneBasket,
		//	IDictionary<int, IMatch> basketMatches
		//);
	}
}