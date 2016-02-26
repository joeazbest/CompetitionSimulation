namespace CompetitionSimulation.CompetitionTable
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class CompetitionTable : ICompetitionTable
	{
		private IEnumerable<ITeam> teams;
		private List<IMatch> matches;
		private Dictionary<ITeam, TeamResult> results;

		public CompetitionTable(
			IEnumerable<ITeam> inputTeam
		)
		{
			if (!inputTeam.Any())
				throw new NotSupportedException("Undifined team Count");

			this.matches = new List<IMatch>();

			this.teams = inputTeam;
			this.results = new Dictionary<ITeam, TeamResult>();
			foreach(var team in this.teams)
			{
				this.results.Add(
					team,
					new TeamResult()
				);
			}
		}

		public void AddMatch(IMatch match)
		{
			this.matches.Add(match);
			this.results[match.HomeTeam].AddMatch(match, match.HomeTeam);
			this.results[match.ForeignTeam].AddMatch(match, match.ForeignTeam);
		}

		public void AddMatches(IEnumerable<IMatch> matchesAdd)
		{
			this.matches.AddRange(matchesAdd);
			foreach(var match in matchesAdd)
			{
				this.results[match.HomeTeam].AddMatch(match, match.HomeTeam);
				this.results[match.ForeignTeam].AddMatch(match, match.ForeignTeam);
			}
		}

		public IEnumerable<IMatch> GetMatches()
		{
			return this.matches;
		}

		/*
		* Stejný počet bodů
		* -> minitabulka vzajemnych zapasu
		* -> vyšší počet vstřelených branek v minitabulce
		* -> kladnější celkový rozdíl
		* -> vyšší počet celkových vstřelených branek
		* -> los
		*/
		public IDictionary<int, ITeam> GetTableResult()
		{
			var output = new Dictionary<int, ITeam>();

			var order = 1;
			foreach(var team in this.results.OrderByDescending(t => t.Value.Points))
			{
				output.Add(order++, team.Key);
			}

			return output;
		}
	}
}