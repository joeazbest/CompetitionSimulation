namespace CompetitionSimulation
{
	using System;

	public class Match : IMatch
	{
		public int HomeScore { get; }

		public int ForeignScore { get; }

		public ITeam HomeTeam { get; }

		public ITeam ForeignTeam { get; }

		public Match(ITeam homeTeam, ITeam foreignTeam, int homeScore, int foreignScore)
		{
			this.HomeTeam = homeTeam;
			this.ForeignTeam = foreignTeam;
			this.HomeScore = homeScore;
			this.ForeignScore = foreignScore;
		}

		public int HomePoint
		{
			get
			{
				switch (this.GetMatchState())
				{
					case MatchState.HomeWin:
						return 3;

					case MatchState.ForeignWin:
						return 0;

					case MatchState.Split:
						return 1;

					default:
						throw new ArgumentOutOfRangeException(this.GetMatchState().ToString());
				}
			}
		}

		public int ForeignPoint
		{
			get
			{
				switch (this.GetMatchState())
				{
					case MatchState.HomeWin:
						return 0;

					case MatchState.ForeignWin:
						return 3;

					case MatchState.Split:
						return 1;

					default:
						throw new ArgumentOutOfRangeException(this.GetMatchState().ToString());
				}
			}
		}

		public MatchState GetMatchState()
		{
			if (this.HomeScore > this.ForeignScore)
				return MatchState.HomeWin;
			else if (this.HomeScore < this.ForeignScore)
				return MatchState.ForeignWin;

			return MatchState.Split;
		}
	}
}