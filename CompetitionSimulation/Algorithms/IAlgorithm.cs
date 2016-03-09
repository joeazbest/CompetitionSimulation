namespace CompetitionSimulation.Algorithms
{
	using Baskets;
	using System.Collections.Generic;

	public interface IAlgorithm
	{
		IList<IBasket> CreateInitialBasket();

		IList<IBasket> GetNextBasketComposition(IList<IBasket> previousBaskets);

		IList<ITeam> GetTeamFinalOrder(IList<IBasket> baskets);
	}
}