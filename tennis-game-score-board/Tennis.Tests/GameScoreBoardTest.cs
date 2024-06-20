using NSubstitute;
using NUnit.Framework;

namespace Tennis.Tests;

public class GameScoreBoardTest
{
    [Test]
    public void player_1_win_1_point()
    {
        var refereeInput = Substitute.For<RefereeInput>();
        var display = Substitute.For<Display>();
        var gameScoreBoard = new GameScoreBoard(refereeInput, display);
        refereeInput.GetScore().Returns("score 1");

        gameScoreBoard.StartGame();

        display.Received(1).Show("Fifteen Love");
    }

    [Test]
    public void player_1_win_2_points()
    {
        var refereeInput = Substitute.For<RefereeInput>();
        var display = Substitute.For<Display>();
        var gameScoreBoard = new GameScoreBoard(refereeInput, display);
        refereeInput.GetScore().Returns("score 1", "score 1");

        gameScoreBoard.StartGame();

        display.Received(1).Show("Fifteen Love");
        display.Received(1).Show("Thirty Love");
    }

    [Test]
    public void player_1_win_3_points() {
        var refereeInput = Substitute.For<RefereeInput>();
        var display = Substitute.For<Display>();
        var gameScoreBoard = new GameScoreBoard(refereeInput, display);
        refereeInput.GetScore().Returns("score 1", "score 1", "score 1");

        gameScoreBoard.StartGame();

        display.Received(1).Show("Fifteen Love");
        display.Received(1).Show("Thirty Love");
        display.Received(1).Show("Forty Love");
    }

    [Test]
    public void player_1_win_4_points_and_win_the_match() {
        var refereeInput = Substitute.For<RefereeInput>();
        var display = Substitute.For<Display>();
        var gameScoreBoard = new GameScoreBoard(refereeInput, display);
        refereeInput.GetScore().Returns("score 1", "score 1", "score 1", "score 1");

        gameScoreBoard.StartGame();

        display.Received(1).Show("Fifteen Love");
        display.Received(1).Show("Thirty Love");
        display.Received(1).Show("Forty Love");
        display.Received(1).Show("Player 1 has won!!\nIt was a nice game.\nBye now");
    }

    [Test]
    public void player_2_win_4_points_and_win_the_match()
    {
        var refereeInput = Substitute.For<RefereeInput>();
        var display = Substitute.For<Display>();
        var gameScoreBoard = new GameScoreBoard(refereeInput, display);
        refereeInput.GetScore().Returns("score 2", "score 2", "score 2", "score 2");

        gameScoreBoard.StartGame();

        display.Received(1).Show("Love Fifteen");
        display.Received(1).Show("Love Thirty");
        display.Received(1).Show("Love Forty");
        display.Received(1).Show("Player 2 has won!!\nIt was a nice game.\nBye now");
    }
}