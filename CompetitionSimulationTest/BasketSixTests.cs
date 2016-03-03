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
			var team1 = new Team("Team1", x => 1);
			var team2 = new Team("Team2", x => 2);
			var team3 = new Team("Team3", x => 3);
			var team4 = new Team("Team4", x => 4);
			var team5 = new Team("Team5", x => 5);
			var team6 = new Team("Team6", x => 6);

			var basket = new BasketSix(
				"Test",
				1,
				1
			);

			basket.AddTeam(1, team1);
			basket.AddTeams(
				new Dictionary<int, ITeam>
				{
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

			// tohle poradi by melo byt t6, t4, t5, t3, t2, t1
			Assert.AreEqual(team6, basketResult[1]);
			Assert.AreEqual(team4, basketResult[2]);
			Assert.AreEqual(team5, basketResult[3]);
			Assert.AreEqual(team3, basketResult[4]);
			Assert.AreEqual(team2, basketResult[5]);
			Assert.AreEqual(team1, basketResult[6]);
		}
	}
}