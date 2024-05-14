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
        var number = _randomGenerator.GenerateRandomNumber();
        var selectedNumber = _userResponse.Get();

        if (number != selectedNumber)
        {
            _userNotification.Notify("The number is higher");
        }
        else
        {
            _userNotification.Notify("win game");
        }
    }
}