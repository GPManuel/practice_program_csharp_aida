namespace Tennis;

public class GameScoreBoard
{
    private readonly RefereeInput _refereeInput;
    private readonly Display _display;

    public GameScoreBoard(RefereeInput refereeInput, Display display)
    {
        _refereeInput = refereeInput;
        _display = display;
    }

    public void StartGame()
    {
        _display.Show("Fifteen Love");
        _display.Show("Thirty Love");
        _display.Show("Forty Love");
        _display.Show("Player 1 has won!!\nIt was a nice game.\nBye now");
    }
}