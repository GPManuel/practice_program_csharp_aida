namespace Tennis;

public class GameScoreBoard
{
    private Game _game;

    public GameScoreBoard(RefereeInput refereeInput, Display display)
    {
        _game = new Game(display, refereeInput);
    }

    public void StartGame()
    {
        _game.Start();
    }
}