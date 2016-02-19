namespace CompetitionSimulation.Algorithm
{
	using System;
	using System.Collections.Generic;
	using System.IO;

	internal class PrimitiveAlgorithm : Algorithm
	{
		public override IList<IBasket> CreateInitialBasket(
			IList<ITeam> teams
		)
		{
			if (teams == null)
				throw new ArgumentNullException(nameof(teams));

			if (teams.Count % 6 != 0)
				throw new InvalidDataException("Only divisible by 6");

			var output = new List<IBasket>();
			for (var i = 1; i <= teams.Count / 6; i++)
			{   // TODO tohle jde urcite lip, ale tohle je ted dostatecny

				var teamOrder = new Dictionary<int, ITeam>
				{
					{ (i-1)*6+1, teams[(i-1)*6] },
					{ (i-1)*6+2, teams[(i-1)*6+1] },
					{ (i-1)*6+3, teams[(i-1)*6+2] },
					{ (i-1)*6+4, teams[(i-1)*6+3] },
					{ (i-1)*6+5, teams[(i-1)*6+4] },
					{ (i-1)*6+6, teams[(i-1)*6+5] },
				};

				output.Add(
					new Basket(
						i.ToString(),
						i,
						1,
						teamOrder,
						new CompetitionSixTable(	// TODO tohle volim
							teamOrder
						)
					)
				);
			}

			return output;
		}

		//public override IDictionary<int, IMatch> ComputeBasketMatches(
		//	IDictionary<int, ITeam> oneBasket,
		//	int round
		//)
		//{
		//	if (oneBasket.Count != 6)
		//		throw new InvalidDataException("Only divisible by 6");

		//	var matches = new Dictionary<int, IMatch>();
		//	var order = 1;
		//	// zvolim skupinu 1, 4, 5 a 2, 3, 6
		//	matches.Add(order++, new Match(oneBasket[1], oneBasket[4], GetMatchState(oneBasket[1], oneBasket[4], round)));
		//	matches.Add(order++, new Match(oneBasket[1], oneBasket[5], GetMatchState(oneBasket[1], oneBasket[4], round)));
		//	matches.Add(order++, new Match(oneBasket[4], oneBasket[5], GetMatchState(oneBasket[1], oneBasket[4], round)));

		//	matches.Add(order++, new Match(oneBasket[2], oneBasket[3], GetMatchState(oneBasket[1], oneBasket[4], round)));
		//	matches.Add(order++, new Match(oneBasket[2], oneBasket[6], GetMatchState(oneBasket[1], oneBasket[4], round)));
		//	matches.Add(order, new Match(oneBasket[3], oneBasket[6], GetMatchState(oneBasket[1], oneBasket[4], round)));

		//	// urcim poradi ve skupine

		//	return null;
		//}

		//public override IDictionary<int, ITeam> ComputeBasketTeamOrder(
		//	IDictionary<int, ITeam> oneBasket,
		//	IDictionary<int, IMatch> basketMatches
		//)
		//{
		//	throw new NotImplementedException();
		//}

		//private MatchState GetMatchState(ITeam team1, ITeam team2, int round)
		//{
		//	var team1Power = team1.GetCurrentPower(round);
		//	var team2Power = team2.GetCurrentPower(round);

		//	if (team1Power < team2Power)
		//		return MatchState.HomeWin;
		//	else if (team2Power > team1Power)
		//		return MatchState.ForeignWin;
		//	else
		//		return MatchState.Split;
		//}
	}
}