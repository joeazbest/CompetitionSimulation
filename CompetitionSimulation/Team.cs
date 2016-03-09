namespace CompetitionSimulation
{
	using System;
	using System.Collections.Generic;

	public class Team : ITeam
	{
		public string Name { get; }
		private readonly Func<decimal, decimal> powerComputer;
		private readonly List<Organizer> organizer;

		public Team(
			string name,
			Func<decimal, decimal> powerComputer
		)
		{
			this.Name = name;
			this.powerComputer = powerComputer;
			this.organizer = new List<Organizer>();
		}

		public Team(
			string name,
			Func<decimal, decimal> powerComputer,
			List<Organizer> organizerRound
		)
		{
			this.Name = name;
			this.powerComputer = powerComputer;
			this.organizer = organizerRound;
		}

		public decimal GetCurrentPower(int round)
		{
			return this.powerComputer(round);
		}

		public List<Organizer> GetOrganizer()
		{
			return this.organizer;
		}
	}

	public class Organizer
	{
		public int Round { get; }
		public string Basket { get; }

		public Organizer(
			int round,
			string basket
		)
		{
			this.Round = round;
			this.Basket = basket;
		}
	}
}