namespace CompetitionSimulation
{
	internal class Match : IMatch
	{
		// TODO skore mozna
		private readonly ITeam HomeTeam;

		private readonly ITeam ForeignTeam;
		private readonly MatchState State;

		internal Match(
			ITeam homeTeam,
			ITeam foreignTeam,
			MatchState state
			)
		{
			this.HomeTeam = homeTeam;
			this.ForeignTeam = foreignTeam;
			this.State = state;
		}

		public ITeam GetHomeTeam()
		{
			return this.HomeTeam;
		}

		public ITeam GetForeignTeam()
		{
			return this.ForeignTeam;
		}

		public MatchState GetMasMatchState()
		{
			return this.State;
		}
	}
}