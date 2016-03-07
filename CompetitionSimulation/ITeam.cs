namespace CompetitionSimulation
{
	using System.Collections.Generic;

	public interface ITeam
	{
		string Name { get; }
		decimal GetCurrentPower(int round);

		List<int> OrganizerRound();
	}
}