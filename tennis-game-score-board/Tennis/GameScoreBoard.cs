namespace Tennis;

public class GameScoreBoard
{
    private readonly RefereeInput _refereeInput;
    private readonly Display _display;
    private int _playerOneScore;
    private int _playerTwoScore;

    public GameScoreBoard(RefereeInput refereeInput, Display display)
    {
        _refereeInput = refereeInput;
        _display = display;
        _playerOneScore = 0;
        _playerTwoScore = 0;
    }

    public void StartGame() {
        Score();
        _display.Show($"{GetPlayerOneScoreDisplay()} {GetPlayerTwoScoreDisplay()}");
        Score();
        _display.Show($"{GetPlayerOneScoreDisplay()} {GetPlayerTwoScoreDisplay()}");
        Score();
        _display.Show($"{GetPlayerOneScoreDisplay()} {GetPlayerTwoScoreDisplay()}");
        Score();
        var winner = GetWinner();
        _display.Show($"Player {winner} has won!!\nIt was a nice game.\nBye now");
    }

    private string GetWinner()
    {
        if (_playerOneScore > _playerTwoScore)
        {
            return "1";
        }

        return "2";
    }

    private string GetPlayerTwoScoreDisplay()
    {
        if (_playerTwoScore == 3) return "Forty";
        if (_playerTwoScore == 2) return "Thirty";
        if (_playerTwoScore == 1) return "Fifteen";
        return "Love";
    }

    private string GetPlayerOneScoreDisplay() {
        if (_playerOneScore == 3) return "Forty";
        if (_playerOneScore == 2) return "Thirty";
        if (_playerOneScore == 1) return "Fifteen";
        return "Love";
    }

    private void Score() {
        var score = _refereeInput.GetScore();
        if (score.Equals("score 1"))
        {
            _playerOneScore++;
        }
        else
        {
            _playerTwoScore++;
        }
    }
}