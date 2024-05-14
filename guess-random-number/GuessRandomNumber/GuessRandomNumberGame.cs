namespace GuessRandomNumber;

public class GuessRandomNumberGame
{
    private readonly NumberToGuessGenerator _numberToGuessGenerator;
    private readonly UserNotification _userNotification;
    private readonly UserResponse _userResponse;
    private int _numberToGuess;

    public GuessRandomNumberGame(NumberToGuessGenerator numberToGuessGenerator, UserNotification userNotification, UserResponse userResponse)
    {
        _numberToGuessGenerator = numberToGuessGenerator;
        _userNotification = userNotification;
        _userResponse = userResponse;
    }

    public void Run()
    {
        var number = _numberToGuessGenerator.GenerateRandomNumberFromOneToTwelve();

        for (int i = 0; i < 2; i++)
        {
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
                return;
            }
        }
    }
}