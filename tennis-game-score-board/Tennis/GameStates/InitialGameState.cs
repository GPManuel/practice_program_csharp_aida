using System.Numerics;

namespace Tennis.GameStates;

internal class InitialGameState : GameState
{
    public InitialGameState(Player player1, Player player2) : base(player1, player2)
    {
    }

    public override GameState Player1Scored()
    {
        _player1.ScorePoint();
        if (_player1.Won(_player2))
        {
            return new FinalGameState(_player1, _player2);
        }

        if (_player1.IsDeuce(_player2))
        {
            return new DeuceGameState(_player1, _player2);
        }

        return this;

    }

    public override GameState Player2Scored()
    {
        _player2.ScorePoint();
        if (_player2.Won(_player1))
        {
            return new FinalGameState(_player1, _player2);
        }

        if (_player2.IsDeuce(_player1))
        {
            return new DeuceGameState(_player1, _player2);
        }

        return this;
    }

    public override string CurrentScore()
    {
        return $"{GetScoreDisplayBeforeDeuce(_player1)} {GetScoreDisplayBeforeDeuce(_player2)}";
    }

    private string GetScoreDisplayBeforeDeuce(Player player)
    {
        if (player.GetScore() == 0)
        {
            return "Love";
        }
        if (player.GetScore() == 1)
        {
            return "Fifteen";
        }
        if (player.GetScore() == 2)
        {
            return "Thirty";
        }

        return "Forty";
    }
}