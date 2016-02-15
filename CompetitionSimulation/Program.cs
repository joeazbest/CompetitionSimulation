namespace CompetitionSimulation
{
	using System.Collections.Generic;

	internal class Program
	{
		private static void Main(string[] args)
		{
			// nacteni seznamu tymu - vim seznam typu a vim jejich funkci, kterea urcuje jejich silu v prubehu casovych useku (kola)
			var teams = new Dictionary<int, Team>();
			for (var i = 1; i <= 36; i++)
			{
				teams.Add(i, new Team(i.ToString(), arg => i)); // zatim jen blbustka, tak funkce musi byt nacitane z nejakych konfiguraku
			}

			// prvotni rozzareni do skupin - prozatim udelane jen jednoduse druhotne pak na to bude muset byt opet fce

			// jejich rozdeleni do skupin

			// provadeni jednotlivych kol
		}
	}
}