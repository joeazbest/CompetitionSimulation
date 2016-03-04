﻿namespace CompetitionSimulation.Algorithms
{
	using Baskets;
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;

	/// <summary>
	/// velmi primitivni algoritmus pocitam s poctem delitelnym 6ti
	/// </summary>
	public sealed class PrimitiveAlgorithm : Algorithm
	{
		public override IList<IBasket> CreateInitialBasket(
			IList<ITeam> teams
		)
		{
			if (teams == null)
				throw new ArgumentNullException(nameof(teams));

			if (teams.Count % 6 != 0)
				throw new InvalidDataException("Only divisible by 6");

			if (teams.Count < 12)
				throw new InvalidDataException("Minimal team count is 12");

			var output = new List<IBasket>();
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

				var currentBasket = new PrimitiveBasketSix(
					i.ToString(),
					i,
					1
				);
				currentBasket.AddTeams(teamOrder);

				output.Add(
					currentBasket
				);
			}

			return output;
		}

		public override IList<IBasket> GetNextBasketComposition(
			IList<IBasket> previousBaskets
		)
		{
			// v tomto algoritmu neresim uzivatele a mam skupiny prave po 6ti a ani se me nijak nemeni poradi
			var currentRound = previousBaskets.First().Round + 1;

			var outputBasket = new List<IBasket>();

			// vlozim prazdny kosiky
			for (var i = 1; i <= previousBaskets.Count; i++)
			{
				outputBasket.Add(new PrimitiveBasketSix(
					i.ToString(),
					i,
					currentRound
					)
				);
			}

			foreach (var basket in previousBaskets)
			{
				var result = basket.GetBasketResult();

				if (basket.Order == 1)  // prvni kos
				{
					outputBasket[0].AddTeams(
						result.Where(t => t.Key < 6).ToDictionary(t => t.Key, t => t.Value)
					);

					outputBasket[1].AddTeam(
						1, result[6]
					);
				}
				else if (basket.Order == previousBaskets.Count)     // posledni kos
				{
					outputBasket[previousBaskets.Count - 1].AddTeams(
						result.Where(t => t.Key > 1).ToDictionary(t => t.Key, t => t.Value)
					);

					outputBasket[previousBaskets.Count - 2].AddTeam(
						6, result[1]
					);
				}
				else
				{
					// bacha na pocitani - pole od nuly poradi od jednicky
					outputBasket[basket.Order - 1].AddTeams(
						result.Where(t => t.Key > 1 && t.Key < 6).ToDictionary(t => t.Key, t => t.Value)
					);

					outputBasket[basket.Order - 2].AddTeam(
						6, result[1]
					);

					outputBasket[basket.Order].AddTeam(
						1, result[6]
					);
				}
			}

			return outputBasket;
		}

		// v tomto pripade podobny GetNextBasketComposition tak to zneuziju :-)
		public override IList<ITeam> GetTeamFinalOrder(
			IList<IBasket> baskets
		)
		{
			var output = new List<ITeam>();

			var newOrder = this.GetNextBasketComposition(baskets);
			foreach (var basket in newOrder.OrderBy(t => t.Order))
			{
				output.AddRange(basket.GetBasketIntitialOrder().OrderBy(t => t.Key).Select(t => t.Value));
			}

			return output;
		}
	}
}