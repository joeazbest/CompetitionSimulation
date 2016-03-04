namespace CompetitionSimulation.Baskets
{
	using CompetitionSimulation.Tables;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public abstract class Basket : IBasket
	{
		public int Order { get; }
		public int Round { get; }
		public string Name { get; }

		protected readonly IDictionary<int, ITeam> basketInnitial;
		protected readonly List<IMatch> matches;
		protected readonly IDictionary<int, ITeam> basketResult;

		protected readonly int BasketTeamCount;

		private readonly object computeLock = new object();

		protected Basket(
			string name,
			int order,
			int round,
			int basketTeamCount
		)
		{
			this.Name = name;
			this.Order = order;
			this.Round = round;
			this.BasketTeamCount = basketTeamCount;

			this.basketInnitial = new Dictionary<int, ITeam>();
			this.matches = new List<IMatch>();
			this.basketResult = new Dictionary<int, ITeam>();
		}

		public virtual void AddTeam(
			int order,
			ITeam team
		)
		{
			if (order < 1 || order > this.BasketTeamCount)
				throw new ArgumentOutOfRangeException(nameof(order), "order is out of limit for basket");
			if (this.basketInnitial.ContainsKey(order))
				throw new ArgumentException("This Order is already add");

			this.basketInnitial.Add(order, team);
		}

		public void AddTeams(
			IDictionary<int, ITeam> teams
		)
		{
			foreach (var team in teams)
			{
				var order = team.Key;
				if (order < 1 || order > this.BasketTeamCount)
					throw new ArgumentOutOfRangeException(nameof(order), "order is out of limit for basket");
				if (this.basketInnitial.ContainsKey(order))
					throw new ArgumentException("This Order is already add");

				this.basketInnitial.Add(order, team.Value);
			}
		}

		public virtual IDictionary<int, ITeam> GetBasketIntitialOrder()
		{
			return this.basketInnitial;
		}

		public virtual IDictionary<int, ITeam> GetBasketResult()
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

		public virtual IEnumerable<IMatch> GetBasketeMatches()
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

		protected IMatch MatchCreate(
			ITeam homeTeam,
			ITeam foreignTeam,
			bool isSplitPossible    // TODO nijak jsem to zatim nevyuzil, mozna by stalo za to i to prejmenovat
		)
		{
			var homePower = homeTeam.GetCurrentPower(this.Round);
			var foreignPower = foreignTeam.GetCurrentPower(this.Round);

			return new Match(homeTeam, foreignTeam, (int)homePower, (int)foreignPower); //	TODO vim ze jsem si udelal zakladni omezeni, ale obecne chce tohle lip
		}

		protected CompetitionTable CreateTable(
			List<int> teamOrders
		)
		{
			var teams = new List<ITeam>();
			foreach (var teamOrder in teamOrders)
			{
				teams.Add(this.basketInnitial[teamOrder]);
			}
			return new CompetitionTable(teams);
		}

		// TODO tady kaslu na varietu domaci vs hoste
		protected IEnumerable<IMatch> CreateTableMatches(
			List<int> teamOrders
		)
		{
			var outputMatches = new List<IMatch>();
			foreach (var teamOrder in teamOrders)
			{
				foreach (var teamOrder2 in teamOrders.Where(t => t > teamOrder))
				{
					outputMatches.Add(MatchCreate(this.basketInnitial[teamOrder], this.basketInnitial[teamOrder2], true));
				}
			}

			return outputMatches;
		}

		protected abstract void CreateBasketResults();
	}
}