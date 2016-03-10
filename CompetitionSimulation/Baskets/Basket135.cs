namespace CompetitionSimulation.Baskets
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public sealed class Basket135 : Basket
	{
		private static readonly int basketTeamCount = 6;

		public Basket135(
			string name,
			int order,
			int round
		) : base(name, order, round, basketTeamCount)
		{ }

		protected override void CreateBasketResults()
		{
			// overeni zakladnich predpokladu, pro jistotu
			if (this.BasketInnitial.Count != basketTeamCount)
				throw new ArgumentException("Team Count is nessecery 6");
			if ((this.BasketInnitial.Keys.Min() != 1) || (this.BasketInnitial.Keys.Max() != basketTeamCount))
				throw new IndexOutOfRangeException("Order of teams are strange");

			// TODO - tvorba tabulek by mohl byt vstup
			// na poradi (1, 3, 4) vcelku kaslu, tohle je testovaci algoritmus - ale uzivam to v testu
			var tableA = CreateTable(new List<int> { 1, 3, 5 });
			var matchesA = CreateTableMatches(new List<int> { 1, 3, 5 });
			tableA.AddMatches(matchesA);
			this.Matches.AddRange(matchesA);

			var tableB = CreateTable(new List<int> { 2, 4, 6 });
			var matchesB = CreateTableMatches(new List<int> { 2, 4, 6 });
			tableB.AddMatches(matchesB);
			this.Matches.AddRange(matchesB);

			// vim jaky pocty tam jsou cely to vytvarim sam
			var tableAResult = tableA.GetTableResult();
			var tableBResult = tableB.GetTableResult();

			// nastavba
			var finalMatch = this.MatchCreate(tableAResult[1], tableBResult[1], false);
			var middleMatch = this.MatchCreate(tableAResult[2], tableBResult[2], false);
			var lastMatch = this.MatchCreate(tableAResult[3], tableBResult[3], false);

			this.Matches.Add(finalMatch);
			this.Matches.Add(middleMatch);
			this.Matches.Add(lastMatch);

			// trochu rucne
			var place = 1;
			this.BasketResult.Add(place++, finalMatch.GetMatchState() == MatchState.HomeWin ? finalMatch.HomeTeam : finalMatch.ForeignTeam);
			this.BasketResult.Add(place++, finalMatch.GetMatchState() == MatchState.HomeWin ? finalMatch.ForeignTeam : finalMatch.HomeTeam);

			this.BasketResult.Add(place++, middleMatch.GetMatchState() == MatchState.HomeWin ? middleMatch.HomeTeam : middleMatch.ForeignTeam);
			this.BasketResult.Add(place++, middleMatch.GetMatchState() == MatchState.HomeWin ? middleMatch.ForeignTeam : middleMatch.HomeTeam);

			this.BasketResult.Add(place++, lastMatch.GetMatchState() == MatchState.HomeWin ? lastMatch.HomeTeam : lastMatch.ForeignTeam);
			this.BasketResult.Add(place, lastMatch.GetMatchState() == MatchState.HomeWin ? lastMatch.ForeignTeam : lastMatch.HomeTeam);
		}
	}
}