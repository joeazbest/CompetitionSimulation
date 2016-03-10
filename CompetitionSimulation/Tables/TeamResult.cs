namespace CompetitionSimulation.Tables
{
	using System.Collections.Generic;
	using Baskets;
	using Teams;

	internal class TeamResult
	{
		internal int Points { get; private set; }

		internal int MyGoals { get; private set; }
		internal int OtherGoals { get; private set; }

		internal int DiffScore
		{
			get
			{
				return this.MyGoals - this.OtherGoals;
			}
		}

		// melo by jit jen o zapasy ktere se na vysledku podilely
		internal List<IMatch> Matches { get; private set; }

		internal TeamResult()
		{
			this.Points = 0;
			this.MyGoals = 0;
			this.OtherGoals = 0;
			this.Matches = new List<IMatch>();
		}

		internal void AddMatch(IMatch match, ITeam team)
		{
			if (match.HomeTeam == team)        // TODO ???? jestli staci
			{
				this.MyGoals += match.HomeScore;
				this.OtherGoals += match.ForeignScore;
				this.Points += match.HomePoint;
			}
			else
			{
				this.MyGoals += match.ForeignScore;
				this.OtherGoals += match.HomeScore;
				this.Points += match.ForeignPoint;
			}
		}
	}
}