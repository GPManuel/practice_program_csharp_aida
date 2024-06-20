namespace Tennis;

public class GameScoreBoard
{
    private readonly RefereeInput _refereeInput;
    private readonly Display _display;
    private int _playerOneScore;

    public GameScoreBoard(RefereeInput refereeInput, Display display)
    {
        _refereeInput = refereeInput;
        _display = display;
        _playerOneScore = 0;
    }

    public void StartGame() {
        Score();
        _display.Show($"{GetPlayerOneScoreDisplay()} {GetPlayerTwoScoreDisplay()}");
        Score();
        _display.Show($"{GetPlayerOneScoreDisplay()} {GetPlayerTwoScoreDisplay()}");
        Score();
        _display.Show($"{GetPlayerOneScoreDisplay()} {GetPlayerTwoScoreDisplay()}");
        Score();
        _display.Show("Player 1 has won!!\nIt was a nice game.\nBye now");
    }

    private string GetPlayerTwoScoreDisplay() {
        return "Love";
    }

    private string GetPlayerOneScoreDisplay() {
        if (_playerOneScore == 3) return "Forty";
        if (_playerOneScore == 2) return "Thirty";
        return "Fifteen";
    }

    private void Score() {
        var score = _refereeInput.GetScore();
        _playerOneScore++;
    }
}