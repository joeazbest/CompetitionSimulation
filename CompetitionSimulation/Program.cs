namespace CompetitionSimulation
{
	using System.Collections.Generic;

	internal class Program
	{
		// TODO - poradatelstvi
		private static void Main(string[] args)
		{
			// nacteni seznamu tymu - vim seznam typu a vim jejich funkci, kterea urcuje jejich silu v prubehu casovych useku (kola)
			var teams = new List<ITeam>();
			for (var i = 1; i <= 36; i++)
			{
				teams.Add(new Team(i.ToString(), arg => i)); // zatim jen blbustka, tak funkce musi byt nacitane z nejakych konfiguraku
			}

			// prvotni rozzareni do skupin - prozatim udelane jen jednoduse druhotne pak na to bude muset byt opet fce
			var InnitialBasket = CreateInnitionalBasket(teams);

			// jejich rozdeleni do skupin

			// provadeni jednotlivych kol
		}

		private static IList<IBasket> CreateInnitionalBasket(
			IList<ITeam> teams
		)
		{
			var output = new List<IBasket>();

			return output;
		}
	}
}