namespace CompetitionSimulation.Algorithm
{
	using System.Collections.Generic;

	internal abstract class Algorithm : IAlgorithm
	{
		public abstract IList<IBasket> CreateInitialBastet(
			IList<ITeam> teams
		);

		public abstract IDictionary<int, IMatch> ComputeMatches(
			IDictionary<int, ITeam> teams
		);

		public abstract IDictionary<int, ITeam> ComputeTeamOrder(
			IDictionary<int, ITeam> teams,
			IDictionary<int, IMatch> matches
		);
	}
}