namespace Tennis;

internal class DeuceGameState : GameState
{
    public DeuceGameState(Player player1, Player player2) : base(player1, player2)
    {
    }

    public override GameState Player1Scored()
    {
        _player1.ScorePoint();
        return new AdvantageGameState(_player1, _player2);
    }

    public override GameState Player2Scored()
    {
        _player2.ScorePoint();
        return new AdvantageGameState(_player1, _player2);
    }

    public override string CurrentScore()
    {
        return "Deuce";
    }
}