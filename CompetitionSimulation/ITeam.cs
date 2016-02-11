namespace CompetitionSimulation
{
	internal interface ITeam
	{
		decimal GetCurrentPower(int round);
		string GetName();
	}
}