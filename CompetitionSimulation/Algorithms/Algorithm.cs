namespace CompetitionSimulation.Algorithms
{
	using Baskets;
	using System.Collections.Generic;
	using Teams;

	public abstract class Algorithm : IAlgorithm
	{
		public abstract IList<IBasket> CreateInitialBasket();

		public abstract IList<IBasket> GetNextBasketComposition(
			IList<IBasket> previousBaskets
		);

		public abstract IList<ITeam> GetTeamFinalOrder(
			IList<IBasket> baskets
		);
	}
}