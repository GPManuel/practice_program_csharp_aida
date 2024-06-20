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
}