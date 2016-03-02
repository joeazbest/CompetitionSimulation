namespace CompetitionSimulation.Algorithm.Tests
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
				teams.Add(new Team(i.ToString(), arg => i));
			}

			var alg = new PrimitiveAlgorithm();

			var innitialBasket = alg.CreateInitialBasket(teams);
			Assert.AreEqual(3, innitialBasket.Count);
		}
	}
}