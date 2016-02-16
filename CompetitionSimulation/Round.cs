namespace CompetitionSimulation
{
	using System.Collections.Generic;
	using System.Linq;

	internal class Round : IRound
	{
		private readonly int Order;
		private readonly IList<IBasket> Baskets;

		internal Round(
			int order,
			IList<IBasket> baskets
			)
		{
			this.Order = order;
			this.Baskets = baskets;
		}

		public IDictionary<int, ITeam> GetRoundResult()
		{
			var output = new Dictionary<int, ITeam>();
			var order = 1;
			foreach (var basket in this.Baskets.OrderBy(t => t.GetOrder()))
			{
				foreach (var team in basket.GetBasketResult())
				{
					output.Add(order, team.Value);
					order++;
				}
			}

			return output;
		}

		public int GetOrder()
		{
			return this.Order;
		}
	}
}