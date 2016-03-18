namespace CompetitionSimulation.Teams
{
	using System;
	using System.Collections.Generic;

	public class Team : ITeam
	{
		public string Name { get; }
		public int OptimalOrder { get; }
		private readonly Func<decimal, decimal> powerComputer;
		private readonly List<Organizer> organizer;

		public Team(
			string name,
			int optimalOrder,
			Func<decimal, decimal> powerComputer
		)
		{
			this.Name = name;
			this.OptimalOrder = optimalOrder;
			this.powerComputer = powerComputer;
			this.organizer = new List<Organizer>();
		}

		public Team(
			string name,
			Func<decimal, decimal> powerComputer
		)
		{
			this.Name = name;
			this.OptimalOrder = 0;
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

		public void AddOrganizer(Organizer organizerRound)
		{
			this.organizer.Add(organizerRound);
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
}