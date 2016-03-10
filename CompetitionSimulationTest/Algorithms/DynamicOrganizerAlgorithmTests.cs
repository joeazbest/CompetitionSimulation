namespace CompetitionSimulationTest.Algorithms
{
	using CompetitionSimulation.Algorithms;
	using CompetitionSimulation.Teams;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using System.Collections.Generic;

	[TestClass]
	public class DynamicOrganizerAlgorithmTests
	{
		[TestMethod]
		public void RunningTroughtTest()
		{
			var teams = new List<ITeam>()
			{
				new Team("11", x => 11, AddOrganizer(1, "1")),
				new Team("2", x => 2, AddOrganizer(2, "1")),
				new Team("15", x => 15, AddOrganizer(3, "1")),
				new Team("4", x => 4, AddOrganizer(4, "1")),
				new Team("5", x => 4, AddOrganizer(5, "1")),
				new Team("6", x => 6, AddOrganizer(6, "1")),

				new Team("7", x => 7, AddOrganizer(1, "2")),
				new Team("8", x => 8, AddOrganizer(2, "2")),
				new Team("3", x => 3, AddOrganizer(3, "2")),
				new Team("10", x => 10, AddOrganizer(4, "2")),
				new Team("17", x => 17, AddOrganizer(5, "2")),
				new Team("12", x => 12, AddOrganizer(6, "2")),

				new Team("13", x => 13, AddOrganizer(1, "3")),
				new Team("14", x => 14, AddOrganizer(2, "3")),
				new Team("9", x => 9, AddOrganizer(3, "3")),
				new Team("16", x => 16, AddOrganizer(4, "3")),
				new Team("1", x => 1, AddOrganizer(5, "3")),
				new Team("18", x => 18, AddOrganizer(6, "3"))
			};

			// mam 6 kol a 6 organizatoru
			var alg = new DynamicOrganizerAlgorithm(teams);

			var round1 = alg.CreateInitialBasket();
			Assert.AreEqual(3, round1.Count);
			// poradatel na poslednim miste
			Assert.AreEqual(round1[0].GetBasketIntitialOrder()[6], teams[0]);
			Assert.AreEqual(round1[0].GetBasketIntitialOrder()[1], teams[1]);
			Assert.AreEqual(round1[1].GetBasketIntitialOrder()[6], teams[6]);
			Assert.AreEqual(round1[1].GetBasketIntitialOrder()[1], teams[7]);
			Assert.AreEqual(round1[2].GetBasketIntitialOrder()[6], teams[12]);
			Assert.AreEqual(round1[2].GetBasketIntitialOrder()[1], teams[13]);

			var round2 = alg.GetNextBasketComposition(round1);
			var round3 = alg.GetNextBasketComposition(round2);
			var round4 = alg.GetNextBasketComposition(round3);
			var round5 = alg.GetNextBasketComposition(round4);
			var round6 = alg.GetNextBasketComposition(round5);

			var finalOrder = alg.GetTeamFinalOrder(round6);

			// rucne spocitanej seznam pro danej primitini algorimus s timto poradim - beru vsechy ale poradi je trochu zadrhel je treba tomu trochu verit :-)
			// t18, t17, t16, t14, t15, t12 - t13, t11, t10, t8, t9, t6 - t7, t5, t4, t2, t3, t1

			Assert.AreEqual(teams[17], finalOrder[0]);
			Assert.AreEqual(teams[10], finalOrder[1]);
			Assert.AreEqual(teams[15], finalOrder[2]);
			Assert.AreEqual(teams[13], finalOrder[3]);
			Assert.AreEqual(teams[2], finalOrder[4]);
			Assert.AreEqual(teams[11], finalOrder[5]);

			Assert.AreEqual(teams[12], finalOrder[6]);
			Assert.AreEqual(teams[0], finalOrder[7]);
			Assert.AreEqual(teams[9], finalOrder[8]);
			Assert.AreEqual(teams[7], finalOrder[9]);
			Assert.AreEqual(teams[14], finalOrder[10]);
			Assert.AreEqual(teams[5], finalOrder[11]);

			Assert.AreEqual(teams[6], finalOrder[12]);
			Assert.AreEqual(teams[4], finalOrder[13]);
			Assert.AreEqual(teams[3], finalOrder[14]);
			Assert.AreEqual(teams[1], finalOrder[15]);
			Assert.AreEqual(teams[8], finalOrder[16]);
			Assert.AreEqual(teams[16], finalOrder[17]);
		}

		private List<Organizer> AddOrganizer(
			int round,
			string basket
		)
		{
			return new List<Organizer> { new Organizer(round, basket) };
		}
	}
}