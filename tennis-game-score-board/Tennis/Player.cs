namespace Tennis;

public class Player
{
    private int score;

    public Player()
    {
        score = 0;
    }

    public void ScorePoint()
    {
        score++;
    }

    public int GetScore()
    {
        return score;
    }

    public bool IsDeuce(Player otherPlayer)
    {
        return score == otherPlayer.score && IsOverThirty();
    }

    public bool HasAdvantageOver(Player otherPlayer)
    {
        return otherPlayer.IsOverThirty() && score == otherPlayer.score + 1;
    }

    public bool IsPlayingInitialPhase(Player otherPlayer)
    {
        return !IsOverForty() && !otherPlayer.IsOverForty();
    }

    public bool Won(Player otherPlayer)
    {
        return IsOverForty() && score > otherPlayer.score + 1;
    }

    private bool IsOverThirty()
    {
        return score > 2;
    }

    private bool IsOverForty()
    {
        return score > 3;
    }
}