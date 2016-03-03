namespace CompetitionSimulation
{
	using System;

	public class Team : ITeam
	{
		public string Name { get; }
		private readonly Func<decimal, decimal> powerComputer;

		public Team(
			string name,
			Func<decimal, decimal> powerComputer
		)
		{
			this.Name = name;
			this.powerComputer = powerComputer;
		}

		public decimal GetCurrentPower(int round)
		{
			return this.powerComputer(round);
		}
	}
}