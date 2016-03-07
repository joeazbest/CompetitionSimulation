namespace CompetitionSimulation.Algorithms
{
	using Baskets;
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;

	/// <summary>
	/// velmi primitivni algoritmus pocitam s poctem delitelnym 6ti
	/// </summary>
	public class OrganizerPriorityAlgorithm : Algorithm
	{
		public override IList<IBasket> CreateInitialBasket(
			IList<ITeam> teams
		)
		{
			// TODO aktualen budu vyuzivat jen 6kovy kose, ale realne i 4, 5 a 7 kose
			if (teams == null)
				throw new ArgumentNullException(nameof(teams));

			// pro mene jak 18 tymu to nebudu resit
			if (teams.Count < 18)
				throw new InvalidDataException("Minimal team count is 18");

			// TODO kontrola konzistence poradatelstvi, jestli to vychazi

			var zeroRound = new List<IBasket>();
			for (var i = 1; i <= teams.Count / 6; i++)
			{   // TODO tohle jde urcite lip, ale tohle je ted dostatecny
				var teamOrder = new Dictionary<int, ITeam>
				{
					{ 1, teams[(i - 1) * 6] },
					{ 2, teams[((i - 1) * 6) + 1] },
					{ 3, teams[((i - 1) * 6) + 2] },
					{ 4, teams[((i - 1) * 6) + 3] },
					{ 5, teams[((i - 1) * 6) + 4] },
					{ 6, teams[((i - 1) * 6) + 5] },
				};

				var currentBasket = new BasketSix(
					i.ToString(),
					i,
					1
				);

				currentBasket.AddTeams(teamOrder);
				zeroRound.Add(
					currentBasket
				);
			}

			return output;
		}

		public override IList<IBasket> GetNextBasketComposition(
			IList<IBasket> previousBaskets
		)
		{
			// TODO
			return null;
		}

		public override IList<ITeam> GetTeamFinalOrder(
			IList<IBasket> baskets
		)
		{
			// TODO
			return null;
		}
	}
}