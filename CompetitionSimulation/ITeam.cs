namespace CompetitionSimulation
{
	public interface ITeam
	{
		decimal GetCurrentPower(int round);
		string Name { get; }
	}
}