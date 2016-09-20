namespace CompetitionSimulation.Algorithms
{
    using Baskets;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Teams;

    public sealed class DynamicOrganizerAlgorithm : Algorithm
    {
        private readonly IList<ITeam> teams;
        private readonly IDictionary<int, IDictionary<string, ITeam>> organizer;    // kolo, nazev kosiku, team
        private readonly int basketCount;

        public DynamicOrganizerAlgorithm(IList<ITeam> teams)
        {
            // TODO aktualen budu vyuzivat jen 6kovy kose, ale realne i 4, 5 a 7 kose
            if (teams == null)
                throw new ArgumentNullException(nameof(teams));

            // pro mene jak 18 tymu to nebudu resit
            if (teams.Count < 18)
                throw new InvalidDataException("Minimal team count is 18");

            this.teams = teams;
            this.organizer = new Dictionary<int, IDictionary<string, ITeam>>();
            foreach (var team in teams)
            {
                foreach (var round in team.GetOrganizer())
                {
                    if (!this.organizer.ContainsKey(round.Round))
                    {
                        this.organizer.Add(round.Round, new Dictionary<string, ITeam>());
                    }
                    this.organizer[round.Round].Add(round.Basket, team);
                }
            }

            // TODO kontrola konzistence poradatelstvi, jestli to vychazi, jestli existuje alespon prvni kos, pripadne jak dlouha je souvisla rada

            // TODO zatim jsou to jen skupiny po 6ti
            this.basketCount = this.teams.Count / 6;
        }

        // TODO stejny v OrganizerPriorityAlgorithm
        /// <summary>
        /// tise predpokladam, ze poradatele kose nejsou mimo kos
        /// </summary>
        public override IList<IBasket> CreateInitialBasket()
        {
            var roundOrganizer = this.organizer[1];
            var teamOrganizer = roundOrganizer.Values.ToList();

            if (roundOrganizer.Keys.Count != this.basketCount)
                throw new DataMisalignedException("Organizer count and basket count are different");

            var output = new List<IBasket>();
            for (var b = 1; b <= this.basketCount; b++)
            {
                output.Add(
                    new Basket145(
                        b.ToString(),
                        b,
                        1
                    )
                );

                output[b - 1].AddTeam(6, roundOrganizer[b.ToString()]); // poradatel na posledni misto
            }

            var order = 1;
            var basket = 0;
            foreach (var team in this.teams.Where(t => !teamOrganizer.Contains(t)))
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

            var roundOrganizer = this.organizer[nextRound];
            var teamOrganizer = roundOrganizer.Values.ToList();             // ty ktery poradaji
            var teamOrganizerBasket = roundOrganizer.ToDictionary(t => t.Value, t => t.Key);    // ktery kosik podle nazvu poradaji
            var teamOrganizerOrder = new List<ITeam>();

            var upTeams = new Dictionary<int, ITeam>();                     // postupujici tymy - maximalne jeden na kosik
            var downTeams = new Dictionary<int, ITeam>();                   // sestupujici tymy - maximalne jeden na kosik

            var usedTeams = new HashSet<ITeam>();                           // vsechny pouzite driv
            teamOrganizer.ForEach(t => usedTeams.Add(t));

            foreach (var basket in previousBaskets.OrderBy(t => t.Order))
            {
                foreach (var teamBasket in basket.GetBasketResult())
                {
                    if (teamOrganizer.Contains(teamBasket.Value))
                    {
                        teamOrganizerOrder.Add(teamBasket.Value);
                    }

                    if (teamBasket.Key == 1 && basket.Order != 1)
                    {
                        if (!teamOrganizer.Contains(teamBasket.Value))
                        {
                            upTeams.Add(basket.Order - 1, teamBasket.Value);
                            usedTeams.Add(teamBasket.Value);
                        }
                    }
                    else if (teamBasket.Key == basket.BasketTeamCount && basket.Order != this.basketCount)
                    {
                        if (!teamOrganizer.Contains(teamBasket.Value))
                        {
                            downTeams.Add(basket.Order + 1, teamBasket.Value);
                            usedTeams.Add(teamBasket.Value);
                        }
                    }
                }
            }

            var output = new List<IBasket>();
            for (var b = 1; b <= this.basketCount; b++)
            {
                var basketIndex = b - 1;
                var basketOrganizer = teamOrganizerOrder[basketIndex];

                output.Add(
                    new Basket145(
                        teamOrganizerBasket[basketOrganizer],
                        b,
                        nextRound
                    )
                );

                // poradatel a postupujic a sestupujici
                output[basketIndex].AddTeam(6, basketOrganizer);
                if (upTeams.ContainsKey(b))
                {
                    output[basketIndex].AddTeamFromBottom(upTeams[b]);
                }

                if (downTeams.ContainsKey(b))
                {
                    output[basketIndex].AddTeamFromTop(downTeams[b]);
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

        public override IList<ITeam> GetTeamFinalOrder(
            IList<IBasket> baskets
        )
        {
            var output = new List<ITeam>();

            foreach (var basket in baskets.OrderBy(t => t.Order))
            {
                output.AddRange(basket.GetBasketIntitialOrder().OrderBy(t => t.Key).Select(t => t.Value));
            }

            return output;
        }
    }
}