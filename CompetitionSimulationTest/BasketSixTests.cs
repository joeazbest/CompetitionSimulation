namespace CompetitionSimulation.Tests
{
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using System.Collections.Generic;
	using System.Linq;

	[TestClass]
	public class BasketSixTests
	{
		[TestMethod]
		public void BasketSixTest()
		{
			var team1 = new Team("Team1", x => 6);
			var team2 = new Team("Team2", x => 5);
			var team3 = new Team("Team3", x => 4);
			var team4 = new Team("Team4", x => 3);
			var team5 = new Team("Team5", x => 2);
			var team6 = new Team("Team6", x => 1);

			var basket = new BasketSix(
				"Test",
				1,
				1,
				new Dictionary<int, ITeam>
				{
					{ 1, team1 },
					{ 2, team2 },
					{ 3, team3 },
					{ 4, team4 },
					{ 5, team5 },
					{ 6, team6 }
				}
			);

			Assert.AreEqual(1, basket.Order);
			Assert.AreEqual(1, basket.Round);
			Assert.AreEqual("Test", basket.Name);
			Assert.AreEqual(9, basket.GetBasketeMatches().Count());

			var basketResult = basket.GetBasketResult();

			// tohle poradi by melo byt t1, t2, t3, t5, t4, t6
			Assert.AreEqual(team1, basketResult[1]);
			Assert.AreEqual(team2, basketResult[2]);
			Assert.AreEqual(team3, basketResult[3]);
			Assert.AreEqual(team5, basketResult[4]);
			Assert.AreEqual(team4, basketResult[5]);
			Assert.AreEqual(team6, basketResult[6]);
		}
	}
}