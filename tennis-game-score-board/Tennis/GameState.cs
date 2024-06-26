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

    public virtual bool Finish()
    {
        return false;
    }
}

public class InitialGameState : GameState
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

        if (_player1.HasAdvantageOver(_player2))
        {
            return new AdvantageGameState(_player1, _player2);
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

        if (_player2.HasAdvantageOver(_player2))
        {
            return new AdvantageGameState(_player1, _player2);
        }

        return this;
    }
}

public class DeuceGameState : GameState
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
}

public class AdvantageGameState : GameState
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

        return new InitialGameState(_player1, _player2);
    }

    public override GameState Player2Scored()
    {
        _player2.ScorePoint();
        if (_player2.Won(_player1))
        {
            return new FinalGameState(_player1, _player2);
        }

        return new InitialGameState(_player1, _player2);
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