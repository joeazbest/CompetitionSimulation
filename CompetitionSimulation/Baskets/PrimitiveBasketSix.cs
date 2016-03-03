namespace CompetitionSimulation.Baskets
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Tables;

	public class PrimitiveBasketSix: IBasket
	{
		public string Name { get; }
		public int Order { get; }
		public int Round { get; }
		private readonly IDictionary<int, ITeam> basketInnitial;
		private readonly List<IMatch> matches;
		private readonly IDictionary<int, ITeam> basketResult;

		private object computeLock = new object();

		public PrimitiveBasketSix(
			string name,
			int order,
			int round
		)
		{
			this.Name = name;
			this.Order = order;
			this.Round = round;
			this.basketInnitial = new Dictionary<int, ITeam>();
			this.matches = new List<IMatch>();
			this.basketResult = new Dictionary<int, ITeam>();
		}

		public void AddTeam(int order, ITeam team)
		{
			if (order < 1 || order > 6)
				throw new ArgumentOutOfRangeException("Order has to be between 1 and 6");
			if (this.basketInnitial.ContainsKey(order))
				throw new ArgumentException("This Order is already add");

			this.basketInnitial.Add(order, team);
		}

		public void AddTeams(IDictionary<int, ITeam> teams)
		{
			foreach (var team in teams)
			{
				var order = team.Key;
				if (order < 1 || order > 6)
					throw new ArgumentOutOfRangeException("Order has to be between 1 and 6");
				if (this.basketInnitial.ContainsKey(order))
					throw new ArgumentException("This Order is already add");

				this.basketInnitial.Add(order, team.Value);
			}
		}

		// bacha na zamikani
		private void CreateBasketResults()
		{
			if (this.basketInnitial.Count != 6)
				throw new ArgumentException("Team Count is 6 to need");

			// na poradi vcelku kaslu, tohle je testovaci algoritmus
			var table1 = new CompetitionTable(
				new List<ITeam>
				{
					this.basketInnitial[1],
					this.basketInnitial[3],
					this.basketInnitial[4]
				}
			);

			table1.AddMatches(
				new List<IMatch>
				{
					this.MatchCreate(this.basketInnitial[1], this.basketInnitial[3], true),
					this.MatchCreate(this.basketInnitial[3], this.basketInnitial[4], true),
					this.MatchCreate(this.basketInnitial[4], this.basketInnitial[1], true)
				}
			);

			// TODO tohle fakt neni dobry - vytvrarim neco a pak to hned vytahavam a ukladam
			this.matches.AddRange(table1.GetMatches());

			var table2 = new CompetitionTable(
				new List<ITeam>
				{
					this.basketInnitial[2],
					this.basketInnitial[5],
					this.basketInnitial[6]
				}
			);

			table2.AddMatches(
				new List<IMatch>
				{
					this.MatchCreate(this.basketInnitial[2], this.basketInnitial[6], true),
					this.MatchCreate(this.basketInnitial[6], this.basketInnitial[5], true),
					this.MatchCreate(this.basketInnitial[5], this.basketInnitial[2], true)
				}
			);

			// TODO tohle fakt neni dobry
			this.matches.AddRange(table2.GetMatches());

			// vim jaky pocty tam jsou cely to vytvarim sam
			var table1Result = table1.GetTableResult();
			var table2Result = table2.GetTableResult();

			// TODO - tohle v tomto pripade rucne je dost voser ale mozna je to dostatecny
			var finalMatch = this.MatchCreate(table1Result[1], table2Result[1], false);
			var middleMatch = this.MatchCreate(table1Result[2], table2Result[2], false);
			var lastMatch = this.MatchCreate(table1Result[3], table2Result[3], false);

			this.matches.Add(finalMatch);
			this.matches.Add(middleMatch);
			this.matches.Add(lastMatch);

			var place = 1;
			this.basketResult.Add(place++, finalMatch.GetMatchState() == MatchState.HomeWin ? finalMatch.HomeTeam : finalMatch.ForeignTeam);
			this.basketResult.Add(place++, finalMatch.GetMatchState() == MatchState.HomeWin ? finalMatch.ForeignTeam : finalMatch.HomeTeam);

			this.basketResult.Add(place++, middleMatch.GetMatchState() == MatchState.HomeWin ? middleMatch.HomeTeam : middleMatch.ForeignTeam);
			this.basketResult.Add(place++, middleMatch.GetMatchState() == MatchState.HomeWin ? middleMatch.ForeignTeam : middleMatch.HomeTeam);

			this.basketResult.Add(place++, lastMatch.GetMatchState() == MatchState.HomeWin ? lastMatch.HomeTeam : lastMatch.ForeignTeam);
			this.basketResult.Add(place++, lastMatch.GetMatchState() == MatchState.HomeWin ? lastMatch.ForeignTeam : lastMatch.HomeTeam);
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
			lock (this.computeLock)
			{
				if (!this.matches.Any())
				{
					this.CreateBasketResults();
				}
				return this.matches;
			}
		}

		public IDictionary<int, ITeam> GetBasketResult()
		{
			lock (this.computeLock)
			{
				if (!this.basketResult.Any())
				{
					this.CreateBasketResults();
				}
				return this.basketResult;
			}
		}

		public IDictionary<int, ITeam> GetBasketIntitialOrder()
		{
			return this.basketInnitial;
		}
	}
}