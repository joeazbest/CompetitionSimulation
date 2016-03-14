namespace CompetitionSimulation.Baskets
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Tables;
	using Teams;

	public abstract class Basket : IBasket
	{
		public int Order { get; }
		public int Round { get; }
		public string Name { get; }
		public int BasketTeamCount { get; }

		protected readonly IDictionary<int, ITeam> BasketInnitial;
		protected readonly List<IMatch> Matches;
		protected readonly IDictionary<int, ITeam> BasketResult;
		protected readonly IDictionary<ITeam, int> PreviousBasketPlace;

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

			this.BasketInnitial = new Dictionary<int, ITeam>();
			this.Matches = new List<IMatch>();
			this.BasketResult = new Dictionary<int, ITeam>();
			this.PreviousBasketPlace = new Dictionary<ITeam, int>();
		}

		public void AddTeam(
			int order,
			ITeam team
		)
		{
			if (order < 1 || order > this.BasketTeamCount)
				throw new ArgumentOutOfRangeException(nameof(order), "order is out of limit for basket");
			if (this.BasketInnitial.ContainsKey(order))
				throw new ArgumentException("This Order is already add");

			this.BasketInnitial.Add(order, team);
		}

		public void AddTeam(
			int order,
			ITeam team,
			int? previousBasketOrder
		)
		{
			AddTeam(order, team);
			if (previousBasketOrder.HasValue)
			{
				this.PreviousBasketPlace.Add(team, previousBasketOrder.Value);
			}
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
				if (this.BasketInnitial.ContainsKey(order))
					throw new ArgumentException("This Order is already add");

				this.BasketInnitial.Add(order, team.Value);
			}
		}

		public bool AddTeamFromBottom(
			ITeam team,
			int? previousBasketOrder = null
		)
		{
			for (var order = this.BasketTeamCount; order >= 1; order--)
			{
				if (!this.BasketInnitial.ContainsKey(order))
				{
					this.BasketInnitial.Add(order, team);
					return true;
				}
			}
			return false;
		}

		public bool AddTeamFromTop(
			ITeam team,
			int? previousBasketOrder = null
		)
		{
			for (var order = 1; order <= this.BasketTeamCount; order++)
			{
				if (!this.BasketInnitial.ContainsKey(order))
				{
					this.BasketInnitial.Add(order, team);
					return true;
				}
			}
			return false;
		}

		public IDictionary<int, ITeam> GetBasketIntitialOrder()
		{
			return this.BasketInnitial;
		}

		public IDictionary<int, ITeam> GetBasketResult()
		{
			lock (this.computeLock)
			{
				if (!this.BasketResult.Any())
				{
					this.CreateBasketResults();
				}
				return this.BasketResult;
			}
		}

		public IEnumerable<IMatch> GetBasketeMatches()
		{
			lock (this.computeLock)
			{
				if (!this.Matches.Any())
				{
					this.CreateBasketResults();
				}
				return this.Matches;
			}
		}

		public IDictionary<ITeam, int> GetPreviousBasketTeam()
		{
			return this.PreviousBasketPlace;
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
				teams.Add(this.BasketInnitial[teamOrder]);
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
					outputMatches.Add(MatchCreate(this.BasketInnitial[teamOrder], this.BasketInnitial[teamOrder2], true));
				}
			}

			return outputMatches;
		}

		protected abstract void CreateBasketResults();
	}
}