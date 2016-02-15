namespace CompetitionSimulation
{
	internal class Match : IMatch
	{
		// TODO skore mozna
		private readonly string HomeTeam;

		private readonly string ForeignTeam;
		private readonly MatchState State;

		internal Match(
			string homeTeam,
			string foreignTeam,
			MatchState state
			)
		{
			this.HomeTeam = homeTeam;
			this.ForeignTeam = foreignTeam;
			this.State = state;
		}

		public string GetHomeTeam()
		{
			return this.HomeTeam;
		}

		public string GetForeignTeam()
		{
			return this.ForeignTeam;
		}

		public MatchState GetMasMatchState()
		{
			return this.State;
		}
	}
}