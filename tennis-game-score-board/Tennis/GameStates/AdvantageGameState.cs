namespace Tennis.GameStates;

internal class AdvantageGameState : GameState
{
    public AdvantageGameState(Player player1, Player player2) : base(player1, player2)
    {
    }

    public override GameState Player1Scored()
    {
        _player1.ScorePoint();
        if (_player1.Won(_player2))
        {
            return new FinalGameState(_player1, _player2);
        }

        return new DeuceGameState(_player1, _player2);
    }

    public override GameState Player2Scored()
    {
        _player2.ScorePoint();
        if (_player2.Won(_player1))
        {
            return new FinalGameState(_player1, _player2);
        }

        return new DeuceGameState(_player1, _player2);
    }

    public override string CurrentScore()
    {
        if (_player1.HasAdvantageOver(_player2))
        {
            return "Advantage Forty";
        }

        return "Forty Advantage";
    }
}