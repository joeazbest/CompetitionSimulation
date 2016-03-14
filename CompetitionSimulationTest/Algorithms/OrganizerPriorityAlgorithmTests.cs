namespace CompetitionSimulationTest.Algorithms
{
	using CompetitionSimulation.Algorithms;
	using CompetitionSimulation.Teams;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using System.Collections.Generic;

	[TestClass]
	public class OrganizerPriorityAlgorithmTests
	{
		/// <summary>
		/// zakladni test 18 tymu po 6ti kolech
		/// </summary>
		[TestMethod]
		public void RunningTrought18x6Test()
		{
			var teams = new List<ITeam>()
			{
				new Team("11", x => 11, AddOrganizer("1", 1)),
				new Team("2", x => 2, AddOrganizer("1", 2)),
				new Team("15", x => 15, AddOrganizer("1", 3)),
				new Team("4", x => 4, AddOrganizer("1", 4)),
				new Team("5", x => 4, AddOrganizer("1", 5)),
				new Team("6", x => 6, AddOrganizer("1", 6)),

				new Team("7", x => 7, AddOrganizer("2", 1)),
				new Team("8", x => 8, AddOrganizer("2", 2)),
				new Team("3", x => 3, AddOrganizer("2", 3)),
				new Team("10", x => 10, AddOrganizer("2", 4)),
				new Team("17", x => 17, AddOrganizer("2", 5)),
				new Team("12", x => 12, AddOrganizer("2", 6)),

				new Team("13", x => 13, AddOrganizer("3", 1)),
				new Team("14", x => 14, AddOrganizer("3", 2)),
				new Team("9", x => 9, AddOrganizer("3", 3)),
				new Team("16", x => 16, AddOrganizer("3", 4)),
				new Team("1", x => 1, AddOrganizer("3", 5)),
				new Team("18", x => 18, AddOrganizer("3", 6))
			};

			// mam 6 kol a 6 organizatoru
			var alg = new OrganizerPriorityAlgorithm(teams);

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

		/// <summary>
		/// zakladni test 36 tymu po 12ti kolech
		/// </summary>
		[TestMethod]
		public void RunningTrought36x12Test()
		{
			var teams = new List<ITeam>()
			{
				new Team("18", x => 18 , AddOrganizer("1",1,7)),
				new Team("9", x => 9 , AddOrganizer("1",2,8)),
				new Team("10", x => 10 , AddOrganizer("1",3,9)),
				new Team("30", x => 30 , AddOrganizer("1",4,10)),
				new Team("15", x => 15 , AddOrganizer("1",5,11)),
				new Team("17", x => 17 , AddOrganizer("1",6,12)),

				new Team("4", x => 4 , AddOrganizer("2",1,7)),
				new Team("36", x => 36 , AddOrganizer("2",2,8)),
				new Team("14", x => 14 , AddOrganizer("2",3,9)),
				new Team("35", x => 35 , AddOrganizer("2",4,10)),
				new Team("32", x => 32 , AddOrganizer("2",5,11)),
				new Team("21", x => 21 , AddOrganizer("2",6,12)),

				new Team("34", x => 34 , AddOrganizer("3",1,7)),
				new Team("7", x => 7 , AddOrganizer("3",2,8)),
				new Team("11", x => 11 , AddOrganizer("3",3,9)),
				new Team("24", x => 24 , AddOrganizer("3",4,10)),
				new Team("19", x => 19 , AddOrganizer("3",5,11)),
				new Team("3", x => 3 , AddOrganizer("3",6,12)),

				new Team("25", x => 25 , AddOrganizer("4",1,7)),
				new Team("20", x => 20 , AddOrganizer("4",2,8)),
				new Team("12", x => 12 , AddOrganizer("4",3,9)),
				new Team("26", x => 26 , AddOrganizer("4",4,10)),
				new Team("1", x => 1 , AddOrganizer("4",5,11)),
				new Team("29", x => 29 , AddOrganizer("4",6,12)),

				new Team("31", x => 31 , AddOrganizer("5",1,7)),
				new Team("8", x => 8 , AddOrganizer("5",2,8)),
				new Team("27", x => 27 , AddOrganizer("5",3,9)),
				new Team("5", x => 5 , AddOrganizer("5",4,10)),
				new Team("28", x => 28 , AddOrganizer("5",5,11)),
				new Team("13", x => 13 , AddOrganizer("5",6,12)),

				new Team("23", x => 23 , AddOrganizer("6",1,7)),
				new Team("2", x => 2 , AddOrganizer("6",2,8)),
				new Team("6", x => 6 , AddOrganizer("6",3,9)),
				new Team("16", x => 16 , AddOrganizer("6",4,10)),
				new Team("22", x => 22 , AddOrganizer("6",5,11)),
				new Team("33", x => 33 , AddOrganizer("6",6,12))
			};

			var alg = new OrganizerPriorityAlgorithm(teams);
			var round1 = alg.CreateInitialBasket();
			Assert.AreEqual(6, round1.Count);
			var round2 = alg.GetNextBasketComposition(round1);
			var round3 = alg.GetNextBasketComposition(round2);
			var round4 = alg.GetNextBasketComposition(round3);
			var round5 = alg.GetNextBasketComposition(round4);
			var round6 = alg.GetNextBasketComposition(round5);

			var finalOrder = alg.GetTeamFinalOrder(round6);

			// rucne spocitanej seznam pro danej primitini algorimus s timto poradim - beru vsechy ale poradi je trochu zadrhel je treba tomu trochu verit :-)
			Assert.AreEqual("33", finalOrder[0].Name);
			Assert.AreEqual("36", finalOrder[1].Name);
			Assert.AreEqual("35", finalOrder[2].Name);
			Assert.AreEqual("34", finalOrder[3].Name);
			Assert.AreEqual("30", finalOrder[4].Name);
			Assert.AreEqual("32", finalOrder[5].Name);

			Assert.AreEqual("29", finalOrder[6].Name);
			Assert.AreEqual("18", finalOrder[7].Name);
			Assert.AreEqual("31", finalOrder[8].Name);
			Assert.AreEqual("26", finalOrder[9].Name);
			Assert.AreEqual("24", finalOrder[10].Name);
			Assert.AreEqual("28", finalOrder[11].Name);

			Assert.AreEqual("15", finalOrder[12].Name);
			Assert.AreEqual("17", finalOrder[13].Name);
			Assert.AreEqual("21", finalOrder[14].Name);
			Assert.AreEqual("19", finalOrder[15].Name);
			Assert.AreEqual("27", finalOrder[16].Name);
			Assert.AreEqual("14", finalOrder[17].Name);

			Assert.AreEqual("11", finalOrder[18].Name);
			Assert.AreEqual("20", finalOrder[19].Name);
			Assert.AreEqual("25", finalOrder[20].Name);
			Assert.AreEqual("10", finalOrder[21].Name);
			Assert.AreEqual("12", finalOrder[22].Name);
			Assert.AreEqual("23", finalOrder[23].Name);

			Assert.AreEqual("9", finalOrder[24].Name);
			Assert.AreEqual("16", finalOrder[25].Name);
			Assert.AreEqual("22", finalOrder[26].Name);
			Assert.AreEqual("13", finalOrder[27].Name);
			Assert.AreEqual("8", finalOrder[28].Name);
			Assert.AreEqual("6", finalOrder[29].Name);

			Assert.AreEqual("7", finalOrder[30].Name);
			Assert.AreEqual("3", finalOrder[31].Name);
			Assert.AreEqual("5", finalOrder[32].Name);
			Assert.AreEqual("4", finalOrder[33].Name);
			Assert.AreEqual("2", finalOrder[34].Name);
			Assert.AreEqual("1", finalOrder[35].Name);
		}

		private List<Organizer> AddOrganizer(
			string basket,
			params int[] round
		)
		{
			var output = new List<Organizer>();
			foreach (var r in round)
			{
				output.Add(new Organizer(r, basket));
			}
			return output;
		}
	}
}