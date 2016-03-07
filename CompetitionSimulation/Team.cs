namespace CompetitionSimulation
{
	using System;
	using System.Collections.Generic;

	public class Team : ITeam
	{
		public string Name { get; }
		private readonly Func<decimal, decimal> powerComputer;
		private readonly List<int> organizerRound;

		public Team(
			string name,
			Func<decimal, decimal> powerComputer
		)
		{
			this.Name = name;
			this.powerComputer = powerComputer;
			this.organizerRound = new List<int>();
		}

		public Team(
			string name,
			Func<decimal, decimal> powerComputer,
			List<int> organizerRound
		)
		{
			this.Name = name;
			this.powerComputer = powerComputer;
			this.organizerRound = organizerRound;
		}

		public decimal GetCurrentPower(int round)
		{
			return this.powerComputer(round);
		}

		public List<int> OrganizerRound()
		{
			return this.organizerRound;
		}
	}
}