namespace CompetitionSimulation
{
	using Baskets;
	using System.Collections.Generic;
	using System.Linq;
	using Teams;

	public class Round : IRound
	{
		public int Order { get; }
		private readonly IList<IBasket> baskets;

		internal Round(
			int order,
			IList<IBasket> baskets
		)
		{
			this.Order = order;
			this.baskets = baskets;
		}

		public IDictionary<int, ITeam> GetRoundResult()
		{
			var output = new Dictionary<int, ITeam>();
			var order = 1;
			foreach (var basket in this.baskets.OrderBy(t => t.Order))
			{
				foreach (var team in basket.GetBasketResult())
				{
					output.Add(order, team.Value);
					order++;
				}
			}

			return output;
		}
	}
}