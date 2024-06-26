namespace Tennis.GameStates;

internal class FinalGameState : GameState
{
    public FinalGameState(Player player1, Player player2) : base(player1, player2)
    {
    }

    public override GameState Player1Scored()
    {
        _player1.ScorePoint();
        return this;

    }

    public override GameState Player2Scored()
    {
        _player2.ScorePoint();
        return this;
    }

    public override string CurrentScore()
    {
        string winner = "1";
        if (_player2.Won(_player1))
        {
            winner = "2";
        }
        return $"Player {winner} has won!!\nIt was a nice game.\nBye now";
    }

    public override bool Finish()
    {
        return true;
    }
}