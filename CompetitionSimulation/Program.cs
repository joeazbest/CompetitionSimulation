namespace CompetitionSimulation
{
	using System;
	using System.Collections.Generic;

	internal class Program
	{
		private static void Main(string[] args)
		{
			// nacteni seznamu tymu
			var teams = new Dictionary<int, Team>();

			// jejich rozdeleni do skupin

			// provadeni jednotlivych kol
		}
	}

	internal class Team : ITeam
	{
		public decimal GetCurrentPower(int round)
		{
			throw new NotImplementedException();
		}

		public string GetName()
		{
			throw new NotImplementedException();
		}
	}

}