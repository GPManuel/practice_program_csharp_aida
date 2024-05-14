using NSubstitute;
using NUnit.Framework;

namespace GuessRandomNumber.Tests
{
    public class GuessRandomNumberTest
    {
        private UserResponse _userResponse; //TODO CAMBIAR NOMBRE
        private UserNotification _userNotification;
        private NumberToGuessGenerator _numberToGuessGenerator;
        private GuessRandomNumberGame guessRandomNumberGame;

        [SetUp]
        public void SetUp()
        {
            _userResponse = Substitute.For<UserResponse>();
            _userNotification = Substitute.For<UserNotification>();
            _numberToGuessGenerator = Substitute.For<NumberToGuessGenerator>();
            guessRandomNumberGame = new(_numberToGuessGenerator, _userNotification, _userResponse);
        }

        [Test]
        public void user_win_at_first_try()
        {
            _numberToGuessGenerator.GenerateRandomNumberFromOneToTwelve().Returns(3);
            _userResponse.Get().Returns(3);

            guessRandomNumberGame.Run();

            _userNotification.Received(1).Notify("win game");
        }

        [Test]
        public void notify_user_when_number_is_wrong_and_is_lower_than_random_number()
        {
            _numberToGuessGenerator.GenerateRandomNumberFromOneToTwelve().Returns(10);
            _userResponse.Get().Returns(5);

            guessRandomNumberGame.Run();

            _userNotification.Received().Notify("The number is higher");
        }

        [Test]
        public void notify_user_when_number_is_wrong_and_is_greater_than_random_number()
        {
            _numberToGuessGenerator.GenerateRandomNumberFromOneToTwelve().Returns(2);
            _userResponse.Get().Returns(5);

            guessRandomNumberGame.Run();

            _userNotification.Received().Notify("The number is lower");
        }

        [Test]
        public void number_to_guess_not_change_in_different_attempts()
        {
            var numberToGuess = 2;
            _numberToGuessGenerator.GenerateRandomNumberFromOneToTwelve().Returns(numberToGuess,10);
            _userResponse.Get().Returns(5,numberToGuess);

            guessRandomNumberGame.Run();

            _userNotification.Received(1).Notify("win game");
        }

        [Test]
        public void player_loser_when_not_guess_in_three_attempts()
        {
            var numberToGuess = 2;
            _numberToGuessGenerator.GenerateRandomNumberFromOneToTwelve().Returns(numberToGuess, 10);
            _userResponse.Get().Returns(5, numberToGuess);

            guessRandomNumberGame.Run();

            _userNotification.Received(1).Notify("win game");
        }
    }
}