namespace CompetitionSimulation.Algorithm
{
	using System.Collections.Generic;

	public interface IAlgorithm
	{
		IList<IBasket> CreateInitialBasket(IList<ITeam> teams);

		IList<IBasket> GetNextBasketComposition(IList<IBasket> previousBaskets);

		IList<ITeam> GetTeamFinalOrder(IList<IBasket> baskets);
	}
}