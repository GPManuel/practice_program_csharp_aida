namespace Tennis;

public abstract class GameState
{
    protected readonly Player _player1;
    protected readonly Player _player2;

    protected GameState(Player player1, Player player2)
    {
        _player1 = player1;
        _player2 = player2;
    }

    public abstract GameState Player1Scored();
    public abstract GameState Player2Scored();
    public abstract bool Finish();
}

public class InitialGameState : GameState
{
    public InitialGameState(Player player1, Player player2) : base(player1, player2)
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

    public override bool Finish()
    {
        return false;
    }
}

public class FinalGameState : GameState
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

    public override bool Finish()
    {
        return true;
    }
}