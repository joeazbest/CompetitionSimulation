namespace CompetitionSimulation
{
	using System;
	using System.Collections.Generic;
	using Tables;

	public class BasketSix : IBasket
	{
		public string Name { get; }
		public int Order { get; }
		public int Round { get; }
		private readonly IDictionary<int, ITeam> BasketInnitial;
		private readonly List<IMatch> Matches;
		private readonly IDictionary<int, ITeam> BasketResult;

		public BasketSix(
			string name,
			int order,
			int round,
			IDictionary<int, ITeam> basketInnitial
		)
		{
			if (basketInnitial == null || basketInnitial.Count != 6)
				throw new ArgumentException("Team Count is 6 to need");

			this.Name = name;
			this.Order = order;
			this.Round = round;
			this.BasketInnitial = basketInnitial;   //	TODO mozna se me to bude hodit
			this.Matches = new List<IMatch>();

			// TODO poradi je treba si nejak vymyslet
			var table1 = new CompetitionTable(
				new List<ITeam>
				{
					basketInnitial[1],
					basketInnitial[3],
					basketInnitial[4]
				}
			);

			table1.AddMatches(
				new List<IMatch>
				{
					MatchCreate(basketInnitial[1], basketInnitial[3], true),
					MatchCreate(basketInnitial[3], basketInnitial[4], true),
					MatchCreate(basketInnitial[4], basketInnitial[1], true)
				}
			);

			// TODO tohle fakt neni dobry
			this.Matches.AddRange(table1.GetMatches());

			var table2 = new CompetitionTable(
				new List<ITeam>
				{
					basketInnitial[2],
					basketInnitial[5],
					basketInnitial[6]
				}
			);

			table2.AddMatches(
				new List<IMatch>
				{
					MatchCreate(basketInnitial[2], basketInnitial[6], true),
					MatchCreate(basketInnitial[6], basketInnitial[5], true),
					MatchCreate(basketInnitial[5], basketInnitial[2], true)
				}
			);

			// TODO tohle fakt neni dobry
			this.Matches.AddRange(table2.GetMatches());

			// vim jaky pocty tam jsou cely to vytvarim sam
			var table1Result = table1.GetTableResult();
			var table2Result = table2.GetTableResult();

			this.BasketResult = new Dictionary<int, ITeam>();

			// TODO - tohle v tomto pripade rucne je dost voser ale mozna je to dostatecny
			var finalMatch = MatchCreate(table1Result[1], table2Result[1], false);
			var middleMatch = MatchCreate(table1Result[2], table2Result[2], false);
			var lastMatch = MatchCreate(table1Result[3], table2Result[3], false);

			this.Matches.Add(finalMatch);
			this.Matches.Add(middleMatch);
			this.Matches.Add(lastMatch);

			var place = 1;
			this.BasketResult.Add(place++, finalMatch.GetMatchState() == MatchState.HomeWin ? finalMatch.HomeTeam : finalMatch.ForeignTeam);
			this.BasketResult.Add(place++, finalMatch.GetMatchState() == MatchState.HomeWin ? finalMatch.ForeignTeam : finalMatch.HomeTeam);

			this.BasketResult.Add(place++, middleMatch.GetMatchState() == MatchState.HomeWin ? middleMatch.HomeTeam : middleMatch.ForeignTeam);
			this.BasketResult.Add(place++, middleMatch.GetMatchState() == MatchState.HomeWin ? middleMatch.ForeignTeam : middleMatch.HomeTeam);

			this.BasketResult.Add(place++, lastMatch.GetMatchState() == MatchState.HomeWin ? lastMatch.HomeTeam : lastMatch.ForeignTeam);
			this.BasketResult.Add(place++, lastMatch.GetMatchState() == MatchState.HomeWin ? lastMatch.ForeignTeam : lastMatch.HomeTeam);
		}

		private IMatch MatchCreate(
			ITeam homeTeam,
			ITeam foreignTeam,
			bool isSplitPossible    // TODO nijak jsem to zatim nevyuzil, mozna by stalo za to i to prejmenovat
		)
		{
			var homePower = homeTeam.GetCurrentPower(this.Round);
			var foreignPower = foreignTeam.GetCurrentPower(this.Round);

			return new Match(homeTeam, foreignTeam, (int)homePower, (int)foreignPower); //	TODO vim ze jsem si udelal zakladni omezeni, ale obecne chce tohle lip
		}

		public IEnumerable<IMatch> GetBasketeMatches()
		{
			return this.Matches;
		}

		public IDictionary<int, ITeam> GetBasketResult()
		{
			return this.BasketResult;
		}
	}
}