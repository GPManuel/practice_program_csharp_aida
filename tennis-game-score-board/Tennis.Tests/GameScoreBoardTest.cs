using NSubstitute;
using NUnit.Framework;

namespace Tennis.Tests;

public class GameScoreBoardTest
{
    private RefereeInput _refereeInput;
    private Display _display;
    private GameScoreBoard _gameScoreBoard;
    private const string Player1Scored = "score 1";
    private const string Player2Scored = "score 2";

    [SetUp]
    public void SetUp()
    {
        _refereeInput = Substitute.For<RefereeInput>();
        _display = Substitute.For<Display>();
        _gameScoreBoard = new GameScoreBoard(_refereeInput, _display);
    }

    [Test]
    public void player_1_win_4_consecutive_points_and_win_the_game() {
        _refereeInput.GetScore().Returns(Player1Scored, Player1Scored, Player1Scored, Player1Scored);

        _gameScoreBoard.StartGame();

        _display.Received(1).Show("Fifteen Love");
        _display.Received(1).Show("Thirty Love");
        _display.Received(1).Show("Forty Love");
        _display.Received(1).Show("Player 1 has won!!\nIt was a nice game.\nBye now");
    }

    [Test]
    public void player_2_win_4_consecutive_points_and_win_the_game()
    {
        _refereeInput.GetScore().Returns(Player2Scored, Player2Scored, Player2Scored, Player2Scored);

        _gameScoreBoard.StartGame();

        _display.Received(1).Show("Love Fifteen");
        _display.Received(1).Show("Love Thirty");
        _display.Received(1).Show("Love Forty");
        _display.Received(1).Show("Player 2 has won!!\nIt was a nice game.\nBye now");
    }

    [Test]
    public void player_1_win_1_point_and_player_2_win_4_points_and_win_the_match()
    {
        _refereeInput.GetScore().Returns(Player1Scored, Player2Scored, Player2Scored, Player2Scored, Player2Scored);

        _gameScoreBoard.StartGame();

        _display.Received(1).Show("Fifteen Love");
        _display.Received(1).Show("Fifteen Fifteen");
        _display.Received(1).Show("Fifteen Thirty");
        _display.Received(1).Show("Fifteen Forty");
        _display.Received(1).Show("Player 2 has won!!\nIt was a nice game.\nBye now");
    }

    [Test]
    public void player_1_won_with_advantage()
    {
        _refereeInput.GetScore().Returns(Player2Scored, Player1Scored, Player2Scored, Player1Scored, Player2Scored, Player1Scored, Player1Scored, Player1Scored);

        _gameScoreBoard.StartGame();

        _display.Received(1).Show("Love Fifteen");
        _display.Received(1).Show("Fifteen Fifteen");
        _display.Received(1).Show("Fifteen Thirty");
        _display.Received(1).Show("Thirty Thirty");
        _display.Received(1).Show("Thirty Forty");
        _display.Received(1).Show("Deuce");
        _display.Received(1).Show("Advantage Forty");
        _display.Received(1).Show("Player 1 has won!!\nIt was a nice game.\nBye now");
        _display.Received(8).Show(Arg.Any<string>());
    }
}