namespace CompetitionSimulationTest.Algorithms
{
	using CompetitionSimulation;
	using CompetitionSimulation.Algorithms;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using System.Collections.Generic;

	[TestClass]
	public class OrganizerPriorityAlgorithmTests
	{
		[TestMethod]
		public void RunningTrougntTest()
		{
			var teams = new List<ITeam>()
			{
				new Team("11", x => 11, new List<int> { 1 }),
				new Team("2", x => 2, new List<int> { 2 }),
				new Team("15", x => 15, new List<int> { 3 }),
				new Team("4", x => 4, new List<int> { 4 }),
				new Team("5", x => 4, new List<int> { 5 }),
				new Team("6", x => 6, new List<int> { 6 }),

				new Team("7", x => 7, new List<int> { 1 }),
				new Team("8", x => 8, new List<int> { 2 }),
				new Team("3", x => 3, new List<int> { 3 }),
				new Team("10", x => 10, new List<int> { 4 }),
				new Team("17", x => 17, new List<int> { 5 }),
				new Team("12", x => 12, new List<int> { 6 }),

				new Team("13", x => 13, new List<int> { 1 }),
				new Team("14", x => 14, new List<int> { 2 }),
				new Team("9", x => 9, new List<int> { 3 }),
				new Team("16", x => 16, new List<int> { 4 }),
				new Team("1", x => 1, new List<int> { 5 }),
				new Team("18", x => 18, new List<int> { 6 })
			};

			// mam 6 kol a 6 organizatoru
			var alg = new OrganizerPriorityAlgorithm();

			var innitialBasket = alg.CreateInitialBasket(teams);
			Assert.AreEqual(3, innitialBasket.Count);

			var round1 = alg.GetNextBasketComposition(innitialBasket);
			var round2 = alg.GetNextBasketComposition(round1);
			var round3 = alg.GetNextBasketComposition(round2);
			var round4 = alg.GetNextBasketComposition(round3);
			var round5 = alg.GetNextBasketComposition(round4);
			var round6 = alg.GetNextBasketComposition(round5);

			var finalOrder = alg.GetTeamFinalOrder(round6);

			// rucne spocitanej seznam pro danej primitini algorimus s timto poradim - beru vsechy ale poradi je trochu zadrhel je treba tomu trochu verit :-)
			// t18, t17, t16, t15, t11, t13 - t6, t14, t10, t12, t9, t8 - t7, t4, t5, t2, t3, t1
			Assert.AreEqual(teams[17], finalOrder[0]);
			Assert.AreEqual(teams[10], finalOrder[1]);
			Assert.AreEqual(teams[15], finalOrder[2]);
			Assert.AreEqual(teams[2], finalOrder[3]);
			Assert.AreEqual(teams[0], finalOrder[4]);
			Assert.AreEqual(teams[12], finalOrder[5]);

			Assert.AreEqual(teams[5], finalOrder[6]);
			Assert.AreEqual(teams[13], finalOrder[7]);
			Assert.AreEqual(teams[9], finalOrder[8]);
			Assert.AreEqual(teams[11], finalOrder[9]);
			Assert.AreEqual(teams[14], finalOrder[10]);
			Assert.AreEqual(teams[7], finalOrder[11]);

			Assert.AreEqual(teams[6], finalOrder[12]);
			Assert.AreEqual(teams[3], finalOrder[13]);
			Assert.AreEqual(teams[4], finalOrder[14]);
			Assert.AreEqual(teams[1], finalOrder[15]);
			Assert.AreEqual(teams[8], finalOrder[16]);
			Assert.AreEqual(teams[16], finalOrder[17]);
		}
	}
}