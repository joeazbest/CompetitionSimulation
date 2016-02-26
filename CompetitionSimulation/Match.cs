using System;

namespace CompetitionSimulation
{
	public class Match : IMatch
	{
		private readonly ITeam homeTeam;
		private readonly ITeam foreignTeam;

		private readonly int homeScore;
		private readonly int foreignScore;

		public int HomeScore
		{
			get
			{
				return this.homeScore;
			}
		}

		public int ForeignScore
		{
			get
			{
				return this.foreignScore;
			}
		}

		public ITeam HomeTeam
		{
			get
			{
				return this.homeTeam;
			}
		}

		public ITeam ForeignTeam
		{
			get
			{
				return this.foreignTeam;
			}
		}

		public int HomePoint
		{
			get
			{
				if (this.GetMatchState() == MatchState.HomeWin)
					return 3;
				else if (this.GetMatchState() == MatchState.ForeignWin)
					return 0;

				return 1;
			}
		}

		public int ForeignPoint
		{
			get
			{
				if (this.GetMatchState() == MatchState.HomeWin)
					return 0;
				else if (this.GetMatchState() == MatchState.ForeignWin)
					return 3;

				return 1;
			}
		}

		public Match(
			ITeam homeTeam,
			ITeam foreignTeam,
			int homeScore,
			int foreignScore
		)
		{
			this.homeTeam = homeTeam;
			this.foreignTeam = foreignTeam;
			this.homeScore = homeScore;
			this.foreignScore = foreignScore;
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