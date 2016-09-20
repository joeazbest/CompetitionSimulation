namespace CompetitionSimulation
{
    using Algorithms;
    using Baskets;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Teams;

    public class Program
    {
        public static void Main(string[] args)
        {
            var atemptCount = 1;
            var rnd = new Random();
            for (var teamCount = 18; teamCount <= 18; teamCount = teamCount + 6)
            {
                var output = new StreamWriter(string.Format("..\\..\\..\\TxtOutput\\output{0}.txt", teamCount));
                ComputeAndFillAlgoritmus(rnd, output, teamCount, atemptCount);
            }
        }

        // private class Algoritmus

        private static void ComputeAndFillAlgoritmus(
            Random rnd,
            StreamWriter output,
            int teamCount,
            int atemptCount
        )
        {
            var optimalVector = new int[teamCount];
            var primaryVector = new int[teamCount];
            var lastVector = new int[teamCount];

            for (var i = 1; i <= atemptCount; i++) // POCET POKUSU
            {
                // seznam tymu
                var teams = new Dictionary<double, ITeam>();
                for (var teamOrder = 1; teamOrder <= teamCount; teamOrder++) // POCET TYMU
                {
                    var value = teamOrder; // TODO predpokladam ze dve rnd cisla nebudou v 18ti cislech stejny :-)
                    teams.Add(
                        rnd.NextDouble(),
                        new Team($"{value}", value, x => value) // vsechno je stejny ale to nemusi byt pravda :-)
                        );

                    optimalVector[teamOrder - 1] = teamCount - teamOrder + 1;
                }

                // pridam poradatelstvi - kazdemu jednou
                var teamInput = teams.OrderBy(t => t.Key).Select(t => t.Value).ToList();
                var order = 1;
                var basket = 1;
                var fullOrder = 0;

                foreach (var team in teamInput)
                {
                    team.AddOrganizer(new Organizer(order, basket.ToString()));             // prvnich sest kol
                    // team.AddOrganizer(new Organizer(order + 6, basket.ToString()));         // druhych sest kol
                    // team.AddOrganizer(new Organizer(order + 12, basket.ToString()));         // druhych sest kol
                    order++;
                    if (order == 7)
                    {
                        order = 1;
                        basket++;
                    }

                    primaryVector[fullOrder] = team.OptimalOrder;
                    fullOrder++;
                }

                // projedu jednotlivy algoritmy
                try
                {
                    //var alg1 = new NonOrganizerAlgorithm(teamInput);
                    var alg2 = new OrganizerPriorityAlgorithm(teamInput);
                    //var alg3 = new DynamicOrganizerAlgorithm(teamInput);

                    //var basket1First = alg1.CreateInitialBasket();
                    var basket2First = alg2.CreateInitialBasket();
                    //var basket3First = alg3.CreateInitialBasket();

                    output.WriteLine("zacatek inputu");
                    foreach (var team in teamInput)
                    {
                        output.Write("{0} ", team.Name);
                    }
                    output.WriteLine();
                    output.WriteLine("konec inputu");

                    for (var round = 1; round <= 5; round++) // POCET KOL + 1
                    {
                        WriteBasketOutput(output, basket2First, string.Format("Pred kolem {0}", round));
                        //var basket1Second = alg1.GetNextBasketComposition(basket1First);
                        var basket2Second = alg2.GetNextBasketComposition(basket2First);
                        //var basket3Second = alg3.GetNextBasketComposition(basket3First);

                        //basket1First = basket1Second;
                        basket2First = basket2Second;
                        //basket3First = basket3Second;

                        WriteBasketOutput(output, basket2First, string.Format("Po kole {0}", round));
                    }

                    output.Write("{0}\t", VectorDiff(optimalVector, primaryVector));

                    //FillLastVector(
                    //    lastVector,
                    //    alg1.GetTeamFinalOrder(basket1First)
                    //    );
                    //output.Write("{0}\t", VectorDiff(optimalVector, lastVector));

                    //FillLastVector(
                    //    lastVector,
                    //    alg2.GetTeamFinalOrder(basket2First)
                    //    );
                    //output.Write("{0}\t", VectorDiff(optimalVector, lastVector));

                    //FillLastVector(
                    //    lastVector,
                    //    alg3.GetTeamFinalOrder(basket3First)
                    //    );
                    //output.Write("{0}\t", VectorDiff(optimalVector, lastVector));

                    //output.Write(" | ");
                    //foreach (var team in teamInput)
                    //{
                    //    output.Write("{0} ", team.Name);
                    //}

                    //output.Write(" | ");
                    //foreach (var team in alg1.GetTeamFinalOrder(basket1First))
                    //{
                    //    output.Write("{0} ", team.Name);
                    //}
                    //output.Write(" | ");
                    //foreach (var team in alg2.GetTeamFinalOrder(basket2First))
                    //{
                    //    output.Write("{0} ", team.Name);
                    //}
                    //output.Write(" | ");
                    //foreach (var team in alg3.GetTeamFinalOrder(basket3First))
                    //{
                    //    output.Write("{0} ", team.Name);
                    //}

                    output.WriteLine();
                    output.Flush();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    i--;
                }
            }

            output.Close();
        }

        private static void WriteBasketOutput(StreamWriter output, IList<IBasket> basket1First, string text)
        {
            output.WriteLine(text);
            foreach (var basket in basket1First)
            {
                foreach (var team in basket.GetBasketIntitialOrder())
                {
                    output.Write("{0} ", team.Value.Name);
                }
                output.Write("| ");
            }
            output.WriteLine();
        }

        private static void FillLastVector(
            int[] outputVector,
            IList<ITeam> teamOutput
        )
        {
            var lastOrder = 0;
            foreach (var team in teamOutput)
            {
                outputVector[lastOrder] = team.OptimalOrder;
                lastOrder++;
            }
        }

        private static double VectorDiff(
            int[] vector1,
            int[] vector2
        )
        {
            if (vector1 == null || vector2 == null)
                throw new ArgumentNullException(string.Format("{0} / {1}", nameof(vector1), nameof(vector2)));

            if (vector1.Count() != vector2.Count())
                throw new ArgumentException("Different vector count");

            var output = 0d;
            for (var i = 0; i < vector1.Count(); i++)
            {
                var diff = Math.Abs(vector1[i] - vector2[i]);
                if (diff >= 3)
                {
                    output += Math.Pow(2, diff);
                }
            }
            return Math.Sqrt(output);
        }
    }
}