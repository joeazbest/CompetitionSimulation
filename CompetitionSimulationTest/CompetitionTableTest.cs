namespace CompetitionSimulationTest
{
	using CompetitionSimulation;
	using CompetitionSimulation.CompetitionTable;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using System.Collections.Generic;
	using System.Linq;
	[TestClass]
	public class CompetitionTableTest
	{
		[TestMethod]
		public void CreateSimpleOrderTest()
		{
			// poradi by melo byt team1, team2, team3 - vse by melo byt jednoduche
			var team1 = new Team("Team1", x => 1);
			var team2 = new Team("Team2", x => 1);
			var team3 = new Team("Team3", x => 1);

			var match1 = new Match(team1, team2, 4, 2);
			var match2 = new Match(team2, team3, 7, 5);
			var match3 = new Match(team3, team1, 1, 2);

			var table = new CompetitionTable(
				new List<ITeam>
				{
					team1, team2, team3
				}
			);

			// vyzkousim oboji pridavani
			table.AddMatch(match1);
			table.AddMatches(new List<IMatch> { match2, match3 });

			Assert.AreEqual(3, table.GetMatches().Count());

			var orderResult = table.GetTableResult();
			Assert.AreEqual(team1, orderResult[1]);
			Assert.AreEqual(team2, orderResult[2]);
			Assert.AreEqual(team3, orderResult[3]);
		}

		[TestMethod]
		public void CreateSpecialOrderTest()
		{
			var teamList = new List<ITeam>
			{
				new Team("Team1", x => 1),
				new Team("Team2", x => 1),
				new Team("Team3", x => 1)
			};

			var table1 = new CompetitionTable(teamList);
			var table2 = new CompetitionTable(teamList);
			var table3 = new CompetitionTable(teamList);
			var table4 = new CompetitionTable(teamList);

			// poradi t1, t2, t3
			table1.AddMatches(
				new List<IMatch>
				{
					new Match(teamList[0], teamList[1], 0, 0),
					new Match(teamList[1], teamList[2], 1, 0),
					new Match(teamList[2], teamList[0], 0, 2)
				}
			);
		}

	}
}