namespace CompetitionSimulation.Tables
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
			if (!this.teams.Contains(match.HomeTeam) || !this.teams.Contains(match.HomeTeam))
				throw new ArgumentOutOfRangeException(nameof(match), "Team is not in Table");

			this.matches.Add(match);
			this.results[match.HomeTeam].AddMatch(match, match.HomeTeam);
			this.results[match.ForeignTeam].AddMatch(match, match.ForeignTeam);
		}

		public void AddMatches(IEnumerable<IMatch> matchesAdd)
		{
			if (matchesAdd == null)
				throw new ArgumentNullException(nameof(matchesAdd));

			this.matches.AddRange(matchesAdd);
			foreach (var match in matchesAdd)
			{
				if (!this.teams.Contains(match.HomeTeam) || !this.teams.Contains(match.HomeTeam))
					throw new ArgumentOutOfRangeException(nameof(match), "Team is not in Table");

				this.results[match.HomeTeam].AddMatch(match, match.HomeTeam);
				this.results[match.ForeignTeam].AddMatch(match, match.ForeignTeam);
			}
		}

		public IEnumerable<IMatch> GetMatches()
		{
			return this.matches;
		}

		/// <summary>
		///  Stejný počet bodů
		/// -> minitabulka vzajemnych zapasu
		/// -> vyšší počet vstřelených branek v minitabulce
		/// -> kladnější celkový rozdíl
		/// -> vyšší počet celkových vstřelených branek
		/// -> los
		/// </summary>
		public IDictionary<int, ITeam> GetTableResult()
		{
			// TODO jestli je tabulka uplna - tj jestli je to odehrano kazdy s kazdym n krat - ve chvili kdy ziskavam vysledky
			var output = new Dictionary<int, ITeam>();

			var order = 1;
			foreach (var teamList in this.GetTeamPointOrder().OrderByDescending(t => t.Key))
			{
				// existuje pouze jeden team s danymi body
				if (teamList.Value.Count == 1)
				{
					output.Add(order++, teamList.Value.Single());
				}
				else    // nastupuji dalsi kriteria
				{
					// tabulka vzajemnych zapasu
					var miniTable = new CompetitionTable(teamList.Value);
					miniTable.AddMatches(this.matches.Where(t => teamList.Value.Contains(t.HomeTeam) && teamList.Value.Contains(t.ForeignTeam)));

					foreach (var miniTeamList in miniTable.GetTeamPointOrder().OrderByDescending(t => t.Key))
					{
						if (miniTeamList.Value.Count == 1)
						{
							output.Add(order++, miniTeamList.Value.Single());
						}
						else
						{
							// vyssi pocet vstrelenych braek v minitabulce
							foreach (var scoreMiniTableTeam in miniTable.GetTeamShootGoalsOrder(miniTeamList.Value).OrderByDescending(t => t.Key))
							{
								if (scoreMiniTableTeam.Value.Count == 1)
								{
									output.Add(order++, scoreMiniTableTeam.Value.Single());
								}
								else
								{
									// kladnejsi celkovej rozdil
									foreach (
										var diffTotalScore in this.GetTeamScoreDiffOrder(scoreMiniTableTeam.Value).OrderByDescending(t => t.Key))
									{
										if (diffTotalScore.Value.Count == 1)
										{
											output.Add(order++, diffTotalScore.Value.Single());
										}
										else
										{
											foreach (var scoreTotal in this.GetTeamShootGoalsOrder(diffTotalScore.Value).OrderByDescending(t => t.Key))
											{
												if (scoreTotal.Value.Count == 1)
												{
													output.Add(order++, scoreTotal.Value.Single());
												}
												else
												{
													// TODO rnd
													foreach (var xxxTeam in scoreTotal.Value)
													{
														output.Add(order++, xxxTeam);
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}

			return output;
		}

		internal Dictionary<int, List<ITeam>> GetTeamPointOrder()
		{
			return this.results.GroupBy(t => t.Value.Points).ToDictionary(t => t.Key, t => t.Select(r => r.Key).ToList());
		}

		internal Dictionary<int, List<ITeam>> GetTeamShootGoalsOrder(
			IEnumerable<ITeam> filterTeam
		)
		{
			return this.results.Where(t => filterTeam.Contains(t.Key)).GroupBy(t => t.Value.MyGoals).ToDictionary(t => t.Key, t => t.Select(r => r.Key).ToList());
		}

		internal Dictionary<int, List<ITeam>> GetTeamScoreDiffOrder(
			IEnumerable<ITeam> filterTeam
		)
		{
			return this.results.Where(t => filterTeam.Contains(t.Key)).GroupBy(t => t.Value.DiffScore).ToDictionary(t => t.Key, t => t.Select(r => r.Key).ToList());
		}
	}
}