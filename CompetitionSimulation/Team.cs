﻿namespace CompetitionSimulation
{
	using System;

	public class Team : ITeam
	{
		private readonly string Name;
		private readonly Func<decimal, decimal> PowerComputer;

		public Team(string name, Func<decimal, decimal> powerComputer)
		{
			this.Name = name;
			this.PowerComputer = powerComputer;
		}

		public decimal GetCurrentPower(int round)
		{
			return PowerComputer(round);
		}

		public string GetName()
		{
			return this.Name;
		}
	}
}