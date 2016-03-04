namespace CompetitionSimulationTest.Tables
{
	using System.Collections.Generic;
	using System.Linq;
	using CompetitionSimulation;
	using CompetitionSimulation.Tables;
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class CompetitionTableTests
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
				new Team("Team3", x => 1),
				new Team("Team4", x => 1)
			};

			var table1 = new CompetitionTable(teamList);
			var table2 = new CompetitionTable(teamList);
			var table3 = new CompetitionTable(teamList);
			var table4 = new CompetitionTable(teamList);

			// minitabulka vzajemnych zapasu poradi by melo byt t4, t3, t1, t2
			table1.AddMatches(
				new List<IMatch>
				{
					new Match(teamList[0], teamList[1], 2, 0),
					new Match(teamList[0], teamList[2], 1, 6),
					new Match(teamList[0], teamList[3], 1, 4),

					new Match(teamList[1], teamList[2], 1, 0),
					new Match(teamList[1], teamList[3], 1, 5),

					new Match(teamList[2], teamList[3], 2, 2)
				}
			);

			ExpectedOrder(
				table1.GetTableResult(),
				teamList[3],
				teamList[2],
				teamList[0],
				teamList[1]
			);

			// vyšší počet vstřelených branek v minitabulce -> poradi t1, t4, t3, t2
			table2.AddMatches(
				new List<IMatch>
				{
					new Match(teamList[0], teamList[1], 2, 0),
					new Match(teamList[0], teamList[2], 7, 6),
					new Match(teamList[0], teamList[3], 9, 4),

					new Match(teamList[1], teamList[2], 1, 0),
					new Match(teamList[1], teamList[3], 1, 5),

					new Match(teamList[2], teamList[3], 3, 2)
				}
			);

			ExpectedOrder(
				table2.GetTableResult(),
				teamList[0],
				teamList[3],
				teamList[2],
				teamList[1]
			);

			// kladnější celkový rozdíl -> poradi t1, t3, t2, t4
			table3.AddMatches(
				new List<IMatch>
				{
					new Match(teamList[0], teamList[1], 2, 0),
					new Match(teamList[0], teamList[2], 7, 6),
					new Match(teamList[0], teamList[3], 9, 4),

					new Match(teamList[1], teamList[2], 1, 0),
					new Match(teamList[1], teamList[3], 0, 1),

					new Match(teamList[2], teamList[3], 1, 0)
				}
			);

			ExpectedOrder(
				table3.GetTableResult(),
				teamList[0],
				teamList[2],
				teamList[1],
				teamList[3]
			);

			// kladnější celkový rozdíl -> poradi t1, t4, t3, t2
			table4.AddMatches(
				new List<IMatch>
				{
					new Match(teamList[0], teamList[1], 2, 0),
					new Match(teamList[0], teamList[2], 7, 5),
					new Match(teamList[0], teamList[3], 9, 7),

					new Match(teamList[1], teamList[2], 1, 0),
					new Match(teamList[1], teamList[3], 0, 1),

					new Match(teamList[2], teamList[3], 1, 0)
				}
			);

			ExpectedOrder(
				table4.GetTableResult(),
				teamList[0],
				teamList[3],
				teamList[2],
				teamList[1]
			);
		}

		private static void ExpectedOrder(
			IDictionary<int, ITeam> orderResult,
			params ITeam[] expectedTeamOrder
		)
		{
			// tise predpokladam ze pocet clenu je shodny
			var order = 0;
			foreach (var team in expectedTeamOrder)
			{
				Assert.AreEqual(team, orderResult[order + 1]);
				order++;
			}
		}
	}
}