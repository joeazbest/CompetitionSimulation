namespace CompetitionSimulation.Algorithm
{
	using System;
	using System.Collections.Generic;
	using System.IO;

	internal class PrimitiveAlgorithm : Algorithm
	{
		public override IList<IBasket> CreateInitialBastet(
			IList<ITeam> teams
		)
		{
			if (teams == null)
				throw new ArgumentNullException(nameof(teams));

			if (teams.Count % 6 != 0)
				throw new InvalidDataException("Only divisible by 6");

			var output = new List<IBasket>();
			for (var i = 1; i <= teams.Count / 6; i++)
			{	// TODO tohle jde urcite lip, ale tohle je ted dostatecny
				output.Add(
					new Basket(
						i.ToString(),
						i,
						new Dictionary<int, ITeam>
						{
							{ (i-1)*6+1, teams[(i-1)*6] },
							{ (i-1)*6+2, teams[(i-1)*6+1] },
							{ (i-1)*6+3, teams[(i-1)*6+2] },
							{ (i-1)*6+4, teams[(i-1)*6+3] },
							{ (i-1)*6+5, teams[(i-1)*6+4] },
							{ (i-1)*6+6, teams[(i-1)*6+5] },
						},
						ComputeMatches,
						ComputeTeamOrder
					)
				);
			}

			return output;
		}

		public override IDictionary<int, IMatch> ComputeMatches(
			IDictionary<int, ITeam> teams
		)
		{
			throw new System.NotImplementedException();
		}

		public override IDictionary<int, ITeam> ComputeTeamOrder(
			IDictionary<int, ITeam> teams,
			IDictionary<int, IMatch> matches
		)
		{
			throw new System.NotImplementedException();
		}
	}
}