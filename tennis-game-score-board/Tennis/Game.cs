using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;

namespace Tennis;

public class Game
{
    private readonly Display _display;
    private readonly RefereeInput _refereeInput;
    private readonly Player _player1;
    private readonly Player _player2;

    public Game(Display display, RefereeInput refereeInput)
    {
        _display = display;
        _refereeInput = refereeInput;
        _player1 = new Player();
        _player2 = new Player();
    }

    public void Start()
    {
        while (!IsOver())
        {
            ScorePoint();
        }

        ShowGameOverMessage();
    }

    private void ScorePoint()
    {
        AssignPointToPlayer();
        ShowCurrentScore();
    }

    private void AssignPointToPlayer()
    {
        var score = _refereeInput.GetScore();
        if (score.Equals("score 1"))
        {
            _player1.WinPoint();
        }
        else
        {
            _player2.WinPoint();
        }
    }

    private void ShowGameOverMessage()
    {
        var winner = DecideWinner();

        _display.Show($"Player {winner} has won!!\nIt was a nice game.\nBye now");
    }

    private string DecideWinner()
    {
        string winner;
        if (_player1.IsOverForty())
        {
            winner = "1";
        }
        else
        {
            winner = "2";
        }

        return winner;
    }

    private bool IsOver()
    {
        return _player1.IsOverForty()|| _player2.IsOverForty();
    }

    private void ShowCurrentScore()
    {
        if (_player1.GetScore() == _player2.GetScore() && _player1.PlayerScoreIsForty())
        {
            _display.Show("Deuce");
        }
        else
        {
            if (!_player1.IsOverForty() && !_player2.IsOverForty())
            {
                _display.Show($"{_player1.GetScoreDisplayBeforeDeuce()} {_player2.GetScoreDisplayBeforeDeuce()}");
            }
        }
    }
}