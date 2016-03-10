﻿namespace CompetitionSimulation.Algorithms
{
	using Baskets;
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using CompetitionSimulation.Teams;

	// TODO hodil by se asi mensi refactoring je to dost dlouhy a nekter veci se opakujou
	/// <summary>
	/// velmi primitivni algoritmus pocitam s poctem delitelnym 6ti
	/// </summary>
	public sealed class OrganizerPriorityAlgorithm : Algorithm
	{
		private readonly IList<ITeam> Teams;
		private readonly IDictionary<int, IDictionary<string, ITeam>> Organizer;    // kolo, nazev kosiku, team
		private readonly int BasketCount;

		public OrganizerPriorityAlgorithm(IList<ITeam> teams)
		{
			// TODO aktualen budu vyuzivat jen 6kovy kose, ale realne i 4, 5 a 7 kose
			if (teams == null)
				throw new ArgumentNullException(nameof(teams));

			// pro mene jak 18 tymu to nebudu resit
			if (teams.Count < 18)
				throw new InvalidDataException("Minimal team count is 18");

			this.Teams = teams;
			this.Organizer = new Dictionary<int, IDictionary<string, ITeam>>();
			foreach (var team in teams)
			{
				foreach (var round in team.GetOrganizer())
				{
					if (!this.Organizer.ContainsKey(round.Round))
					{
						this.Organizer.Add(round.Round, new Dictionary<string, ITeam>());
					}
					this.Organizer[round.Round].Add(round.Basket, team);
				}
			}

			// TODO kontrola konzistence poradatelstvi, jestli to vychazi, jestli existuje alespon prvni kos, pripadne jak dlouha je souvisla rada

			// TODO zatim jsou to jen skupiny po 6ti
			this.BasketCount = this.Teams.Count / 6;
		}

		public override IList<IBasket> CreateInitialBasket()
		{
			var roundOrganizer = this.Organizer[1];
			var teamOrganizer = roundOrganizer.Values.ToList();

			if (roundOrganizer.Keys.Count != this.BasketCount)
				throw new DataMisalignedException("Organizer count and basket count are different");

			var output = new List<IBasket>();
			for (var b = 1; b <= this.BasketCount; b++)
			{
				output.Add(
					new BasketSix(
						b.ToString(),
						b,
						1
					)
				);

				output[b - 1].AddTeam(6, roundOrganizer[b.ToString()]); // poradatel na posledni misto
			}

			var order = 1;
			var basket = 0;
			foreach (var team in this.Teams.Where(t => !teamOrganizer.Contains(t)))
			{
				if (order == 6)
				{
					order = 1;
					basket++;
				}
				output[basket].AddTeam(order, team);
				order++;
			}

			return output;
		}

		public override IList<IBasket> GetNextBasketComposition(
			IList<IBasket> previousBaskets
		)
		{
			var nextRound = previousBaskets.First().Round + 1;

			var roundOrganizer = this.Organizer[nextRound];
			var teamOrganizer = roundOrganizer.Values.ToList();             // ty ktery poradaji
			var teamHistoryBasketPlace = new Dictionary<ITeam, int>();      // ty ktery by se meli vratit do jineho kose
			var teamChangeBasket = new Dictionary<ITeam, int>();            // tymy ktere by meli vzhledem ke svemu poradi zmenit kose
			var upTeams = new Dictionary<int, ITeam>();                     // postupujici tymy - maximalne jeden na kosik
			var downTeams = new Dictionary<int, ITeam>();                   // sestupujici tymy - maximalne jeden na kosik

			var usedTeams = new HashSet<ITeam>();                           // vsechny pouzite driv
			teamOrganizer.ForEach(t => usedTeams.Add(t));

			foreach (var basket in previousBaskets)
			{
				FillStructures(
					basket,
					teamHistoryBasketPlace,
					usedTeams,
					teamChangeBasket,
					teamOrganizer,
					upTeams,
					downTeams
				);
			}

			var output = new List<IBasket>();
			for (var b = 1; b <= this.BasketCount; b++)
			{
				output.Add(
					new BasketSix(
						b.ToString(),
						b,
						nextRound
					)
				);

				var basketIndex = b - 1;
				var previousOrganizerBasket = teamChangeBasket[roundOrganizer[b.ToString()]];
				output[basketIndex].AddTeam(
					6,
					roundOrganizer[b.ToString()],
					previousOrganizerBasket != b ? (int?)previousOrganizerBasket : null
				); // poradatel na posledni misto

				// tihle by se tam meli vejit oboji :-) nepotrebuju navaratovou hodnotu
				if (upTeams.ContainsKey(b))
				{
					output[basketIndex].AddTeamFromBottom(upTeams[b]);
				}

				if (downTeams.ContainsKey(b))
				{
					output[basketIndex].AddTeamFromTop(downTeams[b]);
				}
			}

			// TODO pokud pokud se do kose vraci vice tymu, tak je treba jeste rozhodnout
			foreach (var team in teamHistoryBasketPlace)
			{
				if (!output[team.Value - 1].AddTeamFromTop(team.Key))
				{
					throw new NotSupportedException("tohle neresim zatim");
				}
			}

			var basketOrder = 0;
			foreach (var basket in previousBaskets.OrderBy(t => t.Order))
			{
				foreach (var team in basket.GetBasketResult().OrderBy(t => t.Key))
				{
					if (usedTeams.Contains(team.Value))
						continue;

					while (!output[basketOrder].AddTeamFromTop(team.Value))
					{
						basketOrder++;
					}
				}
			}

			return output;
		}

		/// <summary>
		/// "zalozim" nove kosiky a pak z jejich zalozeni dokazu vzit poradi
		/// </summary>
		public override IList<ITeam> GetTeamFinalOrder(
			IList<IBasket> baskets
		)
		{
			var finalRound = baskets.First().Round + 1;

			var teamOrganizer = new List<ITeam>();                          // jen pomocne pro zachovani konzistence
			var teamHistoryBasketPlace = new Dictionary<ITeam, int>();      // ty ktery by se meli vratit do jineho kose
			var teamChangeBasket = new Dictionary<ITeam, int>();            // tymy ktere by meli vzhledem ke svemu poradi zmenit kose
			var upTeams = new Dictionary<int, ITeam>();                     // postupujici tymy - maximalne jeden na kosik
			var downTeams = new Dictionary<int, ITeam>();                   // sestupujici tymy - maximalne jeden na kosik

			var usedTeams = new HashSet<ITeam>();                           // vsechny pouzite driv

			foreach (var basket in baskets)
			{
				FillStructures(
					basket,
					teamHistoryBasketPlace,
					usedTeams,
					teamChangeBasket,
					teamOrganizer,
					upTeams,
					downTeams
				);
			}

			var output = new List<IBasket>();
			for (var b = 1; b <= this.BasketCount; b++)
			{
				output.Add(
					new BasketSix(
						b.ToString(),
						b,
						finalRound
					)
				);

				var basketIndex = b - 1;

				// tihle by se tam meli vejit oboji :-) nepotrebuju navaratovou hodnotu
				if (upTeams.ContainsKey(b))
				{
					output[basketIndex].AddTeamFromBottom(upTeams[b]);
				}

				if (downTeams.ContainsKey(b))
				{
					output[basketIndex].AddTeamFromTop(downTeams[b]);
				}
			}

			// TODO pokud pokud se do kose vraci vice tymu, tak je treba jeste rozhodnout
			foreach (var team in teamHistoryBasketPlace)
			{
				if (!output[team.Value - 1].AddTeamFromTop(team.Key))
				{
					throw new NotSupportedException("tohle neresim zatim");
				}
			}

			var basketOrder = 0;
			foreach (var basket in baskets.OrderBy(t => t.Order))
			{
				foreach (var team in basket.GetBasketResult().OrderBy(t => t.Key))
				{
					if (usedTeams.Contains(team.Value))
						continue;

					while (!output[basketOrder].AddTeamFromTop(team.Value))
					{
						basketOrder++;
					}
				}
			}

			var teamOrder = new List<ITeam>();
			foreach (var basket in output.OrderBy(t => t.Order))
			{
				foreach (var team in basket.GetBasketIntitialOrder().OrderBy(t => t.Key))
				{
					teamOrder.Add(team.Value);
				}
			}
			return teamOrder;
		}

		private void FillStructures(
			IBasket basket,
			Dictionary<ITeam, int> teamHistoryBasketPlace,
			HashSet<ITeam> usedTeams,
			Dictionary<ITeam, int> teamChangeBasket,
			List<ITeam> teamOrganizer,
			Dictionary<int, ITeam> upTeams,
			Dictionary<int, ITeam> downTeams
		)
		{
			foreach (var teamBasket in basket.GetPreviousBasketTeam())
			{
				teamHistoryBasketPlace.Add(teamBasket.Key, teamBasket.Value);
				usedTeams.Add(teamBasket.Key);
			}

			foreach (var teamBasket in basket.GetBasketResult())
			{
				if (teamBasket.Key == 1 && basket.Order != 1)
				{
					teamChangeBasket.Add(teamBasket.Value, basket.Order - 1);
					usedTeams.Add(teamBasket.Value);

					if (!teamOrganizer.Contains(teamBasket.Value) && !teamHistoryBasketPlace.ContainsKey(teamBasket.Value))
						upTeams.Add(basket.Order - 1, teamBasket.Value);
				}
				else if (teamBasket.Key == basket.BasketTeamCount && basket.Order != this.BasketCount)
				{
					teamChangeBasket.Add(teamBasket.Value, basket.Order + 1);
					usedTeams.Add(teamBasket.Value);

					if (!teamOrganizer.Contains(teamBasket.Value) && !teamHistoryBasketPlace.ContainsKey(teamBasket.Value))
						downTeams.Add(basket.Order + 1, teamBasket.Value);
				}
				else
				{
					teamChangeBasket.Add(teamBasket.Value, basket.Order);
				}
			}
		}
	}
}