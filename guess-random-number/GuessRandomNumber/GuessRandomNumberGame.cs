namespace GuessRandomNumber;

public class GuessRandomNumberGame
{
    private readonly RandomGenerator _randomGenerator;
    private readonly UserNotification _userNotification;
    private readonly UserResponse _userResponse;
    private int _numberToGuess;

    public GuessRandomNumberGame(RandomGenerator randomGenerator, UserNotification userNotification, UserResponse userResponse)
    {
        _randomGenerator = randomGenerator;
        _userNotification = userNotification;
        _userResponse = userResponse;
    }

    public void Run()
    {
        var number = GetRandomNumber();
        var selectedNumber = _userResponse.Get();

        if (number > selectedNumber)
        {
            _userNotification.Notify("The number is higher");
        }
        else if (number < selectedNumber)
        {
            _userNotification.Notify("The number is lower");
        }
        else
        {
            _userNotification.Notify("win game");
        }
    }

    private int GetRandomNumber()
    {
        _numberToGuess = _randomGenerator.GenerateRandomNumberFromOneToTwelve();
        return _numberToGuess;
    }
}