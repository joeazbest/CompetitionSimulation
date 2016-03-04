namespace CompetitionSimulation.Algorithms.Tests
{
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using System.Collections.Generic;

	[TestClass]
	public class PrimitiveAlgorithmTests
	{
		[TestMethod]
		public void CreateInitialBasketTest()
		{
			var teams = new List<ITeam>();
			for (var i = 1; i <= 18; i++)
			{
				var value = i;  // nutne jinak jsou vsechny funkce stejne
				var team = new Team(i.ToString(), x => value);
				teams.Add(team);
			}

			var alg = new PrimitiveAlgorithm();

			var innitialBasket = alg.CreateInitialBasket(teams);
			Assert.AreEqual(3, innitialBasket.Count);

			var firstRoundBasket = alg.GetNextBasketComposition(innitialBasket);
			var secondRoundBasket = alg.GetNextBasketComposition(firstRoundBasket);

			var finalOrder = alg.GetTeamFinalOrder(secondRoundBasket);

			// rucne spocitanej seznam pro danej primitini algorimus s timto poradim - vezmu kazdy druhy
			// t18, t12, t6, t5, t4, t17, t3, t10, t11, t9, t8, t16, t2, t15, t14, t13, t7, t1
			Assert.AreEqual(teams[17], finalOrder[0]);
			Assert.AreEqual(teams[5], finalOrder[2]);
			Assert.AreEqual(teams[3], finalOrder[4]);

			Assert.AreEqual(teams[2], finalOrder[6]);
			Assert.AreEqual(teams[10], finalOrder[8]);
			Assert.AreEqual(teams[7], finalOrder[10]);

			Assert.AreEqual(teams[1], finalOrder[12]);
			Assert.AreEqual(teams[13], finalOrder[14]);
			Assert.AreEqual(teams[6], finalOrder[16]);
		}
	}
}