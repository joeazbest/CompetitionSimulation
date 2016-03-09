namespace CompetitionSimulation
{
	public class Organizer
	{
		public int Round { get; }
		public string Basket { get; }

		public Organizer(
			int round,
			string basket
			)
		{
			this.Round = round;
			this.Basket = basket;
		}
	}
}