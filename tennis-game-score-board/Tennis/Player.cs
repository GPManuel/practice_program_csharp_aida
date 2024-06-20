namespace Tennis;

public class Player
{
    private int score;
    public Player()
    {
        score = 0;
    }

    public void WinPoint()
    {
        score++;
    }

    public bool IsOverForty()
    {
        return score > 3;
    }

    public string GetScoreDisplayBeforeDeuce()
    {
        if (score == 0)
        {
            return "Love";
        }
        if (score == 1)
        {
            return "Fifteen";
        }
        if (score == 2)
        {
            return "Thirty";
        }

        return "Forty";
    }

    public int GetScore()
    {
        return score;
    }

    public bool PlayerScoreIsForty()
    {
        return GetScore() == 3;
    }
}