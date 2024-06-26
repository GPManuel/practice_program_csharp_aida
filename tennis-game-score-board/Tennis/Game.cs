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
    }

    private bool IsOver()
    {
        return _gameState.Finish();
    }

    private void ScorePoint()
    {
        AssignPointToPlayer();
        PrintScore();
    }

    private void PrintScore()
    {
        _display.Show(_gameState.CurrentScore());
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
}