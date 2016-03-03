namespace CompetitionSimulation
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Tables;

	public class BasketSix : IBasket
	{
		public string Name { get; }
		public int Order { get; }
		public int Round { get; }
		private readonly IDictionary<int, ITeam> BasketInnitial;
		private readonly List<IMatch> Matches;
		private readonly IDictionary<int, ITeam> BasketResult;

		private Object computeLock = new Object();

		public BasketSix(
			string name,
			int order,
			int round
		)
		{
			this.Name = name;
			this.Order = order;
			this.Round = round;
			this.BasketInnitial = new Dictionary<int, ITeam>();
			this.Matches = new List<IMatch>();
			this.BasketResult = new Dictionary<int, ITeam>();
		}

		public void AddTeam(int order, ITeam team)
		{
			if (order < 1 || order > 6)
				throw new ArgumentOutOfRangeException("Order has to be between 1 and 6");
			if (this.BasketInnitial.ContainsKey(order))
				throw new ArgumentException("This Order is already add");

			this.BasketInnitial.Add(order, team);
		}

		public void AddTeams(IDictionary<int, ITeam> teams)
		{
			foreach (var team in teams)
			{
				var order = team.Key;
				if (order < 1 || order > 6)
					throw new ArgumentOutOfRangeException("Order has to be between 1 and 6");
				if (this.BasketInnitial.ContainsKey(order))
					throw new ArgumentException("This Order is already add");

				this.BasketInnitial.Add(order, team.Value);
			}
		}

		// bacha na zamikani
		private void CreateBasketResults()
		{
			if (this.BasketInnitial.Count != 6)
				throw new ArgumentException("Team Count is 6 to need");

			// na poradi vcelku kaslu, tohle je testovaci algoritmus
			var table1 = new CompetitionTable(
				new List<ITeam>
				{
					this.BasketInnitial[1],
					this.BasketInnitial[3],
					this.BasketInnitial[4]
				}
			);

			table1.AddMatches(
				new List<IMatch>
				{
					MatchCreate(this.BasketInnitial[1], this.BasketInnitial[3], true),
					MatchCreate(this.BasketInnitial[3], this.BasketInnitial[4], true),
					MatchCreate(this.BasketInnitial[4], this.BasketInnitial[1], true)
				}
			);

			// TODO tohle fakt neni dobry - vytvrarim neco a pak to hned vytahavam a ukladam
			this.Matches.AddRange(table1.GetMatches());

			var table2 = new CompetitionTable(
				new List<ITeam>
				{
					this.BasketInnitial[2],
					this.BasketInnitial[5],
					this.BasketInnitial[6]
				}
			);

			table2.AddMatches(
				new List<IMatch>
				{
					MatchCreate(this.BasketInnitial[2], this.BasketInnitial[6], true),
					MatchCreate(this.BasketInnitial[6], this.BasketInnitial[5], true),
					MatchCreate(this.BasketInnitial[5], this.BasketInnitial[2], true)
				}
			);

			// TODO tohle fakt neni dobry
			this.Matches.AddRange(table2.GetMatches());

			// vim jaky pocty tam jsou cely to vytvarim sam
			var table1Result = table1.GetTableResult();
			var table2Result = table2.GetTableResult();

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
			lock (computeLock)
			{
				if (!this.Matches.Any())
				{
					this.CreateBasketResults();
				}
				return this.Matches;
			}
		}

		public IDictionary<int, ITeam> GetBasketResult()
		{
			lock (computeLock)
			{
				if (!this.BasketResult.Any())
				{
					this.CreateBasketResults();
				}
				return this.BasketResult;
			}
		}

		public IDictionary<int, ITeam> GetBasketIntitialOrder()
		{
			return this.BasketInnitial;
		}
	}
}