using System.Collections.Generic;

namespace Tennis;

public class GameScoreBoard
{
    private readonly RefereeInput _refereeInput;
    private readonly Display _display;
    private int _playerOneScore;
    private int _playerTwoScore;
    private readonly List<string> _scoreDisplays = new(){ "Love", "Fifteen", "Thirty", "Forty" };

    public GameScoreBoard(RefereeInput refereeInput, Display display)
    {
        _refereeInput = refereeInput;
        _display = display;
        _playerOneScore = 0;
        _playerTwoScore = 0;
    }

    public void StartGame() {
        Score();
        _display.Show($"{GetPlayerScoreDisplay(_playerOneScore)} {GetPlayerScoreDisplay(_playerTwoScore)}");
        Score();
        _display.Show($"{GetPlayerScoreDisplay(_playerOneScore)} {GetPlayerScoreDisplay(_playerTwoScore)}");
        Score();
        _display.Show($"{GetPlayerScoreDisplay(_playerOneScore)} {GetPlayerScoreDisplay(_playerTwoScore)}");
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

    private string GetPlayerScoreDisplay(int playerScore)
    {
        return _scoreDisplays[playerScore];
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