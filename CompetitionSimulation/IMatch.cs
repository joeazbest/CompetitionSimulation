namespace CompetitionSimulation
{
	public interface IMatch
	{
		int HomeScore { get; }
		int ForeignScore { get; }

		ITeam HomeTeam { get; }
		ITeam ForeignTeam { get; }

		int HomePoint { get; }
		int ForeignPoint { get; }

		MatchState GetMatchState();
	}
}