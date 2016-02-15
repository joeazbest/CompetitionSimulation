namespace CompetitionSimulation
{
	internal interface IMatch
	{
		ITeam GetHomeTeam();

		ITeam GetForeignTeam();

		MatchState GetMasMatchState();
	}
}