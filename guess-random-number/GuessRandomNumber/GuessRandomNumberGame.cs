namespace GuessRandomNumber;

public class GuessRandomNumberGame
{
    private readonly RandomGenerator _randomGenerator;
    private readonly UserNotification _userNotification;
    private readonly UserResponse _userResponse;

    public GuessRandomNumberGame(RandomGenerator randomGenerator, UserNotification userNotification, UserResponse userResponse)
    {
        _randomGenerator = randomGenerator;
        _userNotification = userNotification;
        _userResponse = userResponse;
    }

    public void Run()
    {
        _userNotification.Notify("win game");
    }
}