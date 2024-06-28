using System.Reflection;

namespace Tennis.GameStates;

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

    public abstract string CurrentScore();

    public virtual bool Finish()
    {
        return false;
    }
}