namespace CompetitionSimulation.Baskets
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public sealed class Basket145 : Basket
	{
		private static readonly int basketTeamCount = 6;

		public Basket145(
			string name,
			int order,
			int round
		) : base(name, order, round, basketTeamCount)
		{ }

		/// <summary>
		/// do jedne tabulky jde 1, 3, 4 a do druhe 2, 5, 6 - je to jen pro otestovani
		/// </summary>
		protected override void CreateBasketResults()
		{
			// overeni zakladnich predpokladu, pro jistotu
			if (this.basketInnitial.Count != basketTeamCount)
				throw new ArgumentException("Team Count is nessecery 6");
			if ((this.basketInnitial.Keys.Min() != 1) || (this.basketInnitial.Keys.Max() != basketTeamCount))
				throw new IndexOutOfRangeException("Order of teams are strange");

			// na poradi (1, 3, 4) vcelku kaslu, tohle je testovaci algoritmus - ale uzivam to v testu
			var tableA = CreateTable(new List<int> { 1, 4, 5 });
			var matchesA = CreateTableMatches(new List<int> { 1, 4, 5 });
			tableA.AddMatches(matchesA);
			this.matches.AddRange(matchesA);

			var tableB = CreateTable(new List<int> { 2, 3, 6 });
			var matchesB = CreateTableMatches(new List<int> { 2, 3, 6 });
			tableB.AddMatches(matchesB);
			this.matches.AddRange(matchesB);

			// vim jaky pocty tam jsou cely to vytvarim sam
			var tableAResult = tableA.GetTableResult();
			var tableBResult = tableB.GetTableResult();

			// nastavba
			var finalMatch = this.MatchCreate(tableAResult[1], tableBResult[1], false);
			var middleMatch = this.MatchCreate(tableAResult[2], tableBResult[2], false);
			var lastMatch = this.MatchCreate(tableAResult[3], tableBResult[3], false);

			this.matches.Add(finalMatch);
			this.matches.Add(middleMatch);
			this.matches.Add(lastMatch);

			// trochu rucne
			var place = 1;
			this.basketResult.Add(place++, finalMatch.GetMatchState() == MatchState.HomeWin ? finalMatch.HomeTeam : finalMatch.ForeignTeam);
			this.basketResult.Add(place++, finalMatch.GetMatchState() == MatchState.HomeWin ? finalMatch.ForeignTeam : finalMatch.HomeTeam);

			this.basketResult.Add(place++, middleMatch.GetMatchState() == MatchState.HomeWin ? middleMatch.HomeTeam : middleMatch.ForeignTeam);
			this.basketResult.Add(place++, middleMatch.GetMatchState() == MatchState.HomeWin ? middleMatch.ForeignTeam : middleMatch.HomeTeam);

			this.basketResult.Add(place++, lastMatch.GetMatchState() == MatchState.HomeWin ? lastMatch.HomeTeam : lastMatch.ForeignTeam);
			this.basketResult.Add(place, lastMatch.GetMatchState() == MatchState.HomeWin ? lastMatch.ForeignTeam : lastMatch.HomeTeam);
		}
	}
}