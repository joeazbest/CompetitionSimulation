namespace CompetitionSimulation.Algorithms
{
	using Baskets;
	using System.Collections.Generic;

	public abstract class Algorithm : IAlgorithm
	{
		public abstract IList<IBasket> CreateInitialBasket(
			IList<ITeam> teams
		);

		public abstract IList<IBasket> GetNextBasketComposition(
			IList<IBasket> previousBaskets
		);

		public abstract IList<ITeam> GetTeamFinalOrder(
			IList<IBasket> baskets
		);
	}
}