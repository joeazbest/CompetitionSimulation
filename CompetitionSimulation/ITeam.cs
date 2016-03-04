namespace CompetitionSimulation
{
	public interface ITeam
	{
		string Name { get; }
		decimal GetCurrentPower(int round);
	}
}