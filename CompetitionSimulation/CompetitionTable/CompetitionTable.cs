﻿namespace CompetitionSimulation.CompetitionTable
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class CompetitionTable : ICompetitionTable
	{
		private readonly IEnumerable<ITeam> teams;
		private readonly List<IMatch> matches;
		private readonly Dictionary<ITeam, TeamResult> results;

		public CompetitionTable(
			IEnumerable<ITeam> inputTeam
		)
		{
			if (!inputTeam.Any())
				throw new NotSupportedException("Undifined team Count");

			this.matches = new List<IMatch>();

			this.teams = inputTeam;
			this.results = new Dictionary<ITeam, TeamResult>();
			foreach (var team in this.teams)
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
			foreach (var match in matchesAdd)
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
			var pointOrder = this.results.GroupBy(t => t.Value.Points).ToDictionary(t => t.Key, t => t.Select(r => r.Key).ToList());

			var output = new Dictionary<int, ITeam>();

			var order = 1;
			foreach (var teamList in pointOrder.OrderByDescending(t => t.Key))
			{
				// existuje pouze jeden team s danymi body
				if (teamList.Value.Count() == 1)
				{
					output.Add(order++, teamList.Value.Single());
				}
				else	// nastupuji dalsi kriteria
				{
					// tabulka vzajemnych zapasu
					var miniTable = new CompetitionTable(teamList.Value);
					miniTable.AddMatches(this.matches.Where(t => teamList.Value.Contains(t.HomeTeam) && teamList.Value.Contains(t.ForeignTeam)));

					var miniTableOrder = miniTable.results.GroupBy(t => t.Value.Points).ToDictionary(t => t.Key, t => t.Select(r => r.Key).ToList());

					foreach(var miniTeamList in miniTableOrder.OrderByDescending(t => t.Key))
					{
						if(miniTeamList.Value.Count() == 1)
						{
							output.Add(order++, miniTeamList.Value.Single());
						}
						else
						{
							// vyssi pocet vstrelenych branek
							
							// TODO tohle me ted nebavi a chce to vymyslit hezky, takle to bude strasne ifu
						}
					}

				}
			}

			return output;
		}
	}
}