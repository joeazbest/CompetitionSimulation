namespace CompetitionSimulationTest.Algorithms
{
	using CompetitionSimulation.Algorithms;
	using CompetitionSimulation.Teams;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using System;
	using System.Collections.Generic;
	using System.IO;

	[TestClass]
	public class NonOrganizerAlgorithmTests
	{
		[TestMethod]
		public void RunningTroughtTest()
		{
			var teams = new List<ITeam>();
			for (var i = 1; i <= 18; i++)
			{
				var value = i;  // nutne jinak jsou vsechny funkce stejne
				var team = new Team(i.ToString(), x => value);
				teams.Add(team);
			}

			var alg = new NonOrganizerAlgorithm(teams);

			var innitialBasket = alg.CreateInitialBasket();
			Assert.AreEqual(3, innitialBasket.Count);

			var firstRoundBasket = alg.GetNextBasketComposition(innitialBasket);
			var secondRoundBasket = alg.GetNextBasketComposition(firstRoundBasket);

			var finalOrder = alg.GetTeamFinalOrder(secondRoundBasket);

			// rucne spocitanej seznam pro danej primitini algorimus s timto poradim - vezmu kazdy druhy
			// t18, t12, t6, t5, t4, t17, t3, t11, t10, t9, t8, t16, t2, t15, t14, t13, t7, t1
			Assert.AreEqual(teams[17], finalOrder[0]);
			Assert.AreEqual(teams[5], finalOrder[2]);
			Assert.AreEqual(teams[3], finalOrder[4]);

			Assert.AreEqual(teams[2], finalOrder[6]);
			Assert.AreEqual(teams[9], finalOrder[8]);
			Assert.AreEqual(teams[7], finalOrder[10]);

			Assert.AreEqual(teams[1], finalOrder[12]);
			Assert.AreEqual(teams[13], finalOrder[14]);
			Assert.AreEqual(teams[6], finalOrder[16]);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void CreateInitialBasketArgumentNullExceptionTest()
		{
			var alg = new NonOrganizerAlgorithm(null);
			alg.CreateInitialBasket();
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidDataException))]
		public void CreateInitialBasketInvalidDataExceptionExceptionTest()
		{
			var alg = new NonOrganizerAlgorithm(
				new List<ITeam>
				{
					new Team("1", x => 1)
				}
			);
			alg.CreateInitialBasket();
		}
	}
}