namespace CompetitionSimulation
{
	using System.Collections.Generic;

	public interface IBasket
	{
		int Order { get; }
		int Round { get; }

		string Name { get; }

		IDictionary<int, ITeam> GetBasketResult();

		IEnumerable<IMatch> GetBasketeMatches();
	}
}