namespace CompetitionSimulation
{
	internal interface IMatch
	{
		string GetHomeTeam();

		string GetForeignTeam();

		MatchState GetMasMatchState();
	}
}