namespace CompetitionSimulation.Teams
{
	using System.Collections.Generic;

	public interface ITeam
	{
		string Name { get; }
		int OptimalOrder { get; }

		decimal GetCurrentPower(int round);

		void AddOrganizer(Organizer organizerRound);

		List<Organizer> GetOrganizer();
	}
}