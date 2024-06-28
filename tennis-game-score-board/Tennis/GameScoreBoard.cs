using Tennis.GameStates;

namespace Tennis;

public class GameScoreBoard
{
    private readonly Display _display;
    private readonly RefereeInput _refereeInput;
    private readonly Player _player1;
    private readonly Player _player2;
    public GameState _gameState;

    public GameScoreBoard(RefereeInput refereeInput, Display display)
    {
        _display = display;
        _refereeInput = refereeInput;
        _player1 = new Player();
        _player2 = new Player();
        _gameState = new InitialGameState(_player1, _player2);
    }

    public void StartGame()
    {
        while (!IsOver(_gameState))
        {
            ScorePoint();
        }
    }

    public bool IsOver(GameState gameState)
    {
        return gameState.Finish();
    }

    public void ScorePoint()
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
        if (score.Equals("score 1"))
        {
            _gameState = _gameState.Player1Scored();
            return;
        }

        _gameState = _gameState.Player2Scored();

    }
}