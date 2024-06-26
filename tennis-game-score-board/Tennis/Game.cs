namespace Tennis;



public class Game
{
    private readonly Display _display;
    private readonly RefereeInput _refereeInput;
    private readonly Player _player1;
    private readonly Player _player2;
    private GameState _gameState;

    public Game(Display display, RefereeInput refereeInput)
    {
        _display = display;
        _refereeInput = refereeInput;
        _player1 = new Player();
        _player2 = new Player();
        _gameState = new InitialGameState(_player1, _player2);
    }

    public void Start()
    {
        while (!IsOver())
        {
            ScorePoint();
        }

        ShowGameOverMessage();
    }

    private bool IsOver()
    {
        return _gameState.Finish();
    }

    private void ScorePoint()
    {
        AssignPointToPlayer();
        ShowCurrentScore();
    }

    private void AssignPointToPlayer()
    {
        var score = _refereeInput.GetScore();
        if (InputIsPlayer1Score(score))
        {
            _gameState = _gameState.Player1Scored();
            return;
        }

        _gameState = _gameState.Player2Scored();

    }

    private static bool InputIsPlayer1Score(string score)
    {
        return score.Equals("score 1");
    }

    private void ShowGameOverMessage()
    {
        var winner = GetWinnerIdentifier();

        _display.Show($"Player {winner} has won!!\nIt was a nice game.\nBye now");
    }

    private string GetWinnerIdentifier()
    {
        if (_player1.Won(_player2))
        {
            return "1";
        }

        return "2";
    }

    private void ShowCurrentScore()
    {
        if (_player1.IsDeuce(_player2))
        {
            _display.Show("Deuce");
            return;
        }

        if (_player1.HasAdvantageOver(_player2))
        {
            _display.Show("Advantage Forty");
            return;
        }

        if (_player2.HasAdvantageOver(_player1))
        {
            _display.Show("Forty Advantage");
            return;
        }

        if (_player1.IsPlayingInitialPhase(_player2))
        {
            _display.Show($"{GetScoreDisplayBeforeDeuce(_player1)} {GetScoreDisplayBeforeDeuce(_player2)}");
        }
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