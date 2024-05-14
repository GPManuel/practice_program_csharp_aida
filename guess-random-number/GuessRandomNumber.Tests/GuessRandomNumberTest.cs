using NSubstitute;
using NUnit.Framework;

namespace GuessRandomNumber.Tests
{
    public class GuessRandomNumberTest
    {
        private UserResponse _userResponse; //TODO CAMBIAR NOMBRE
        private UserNotification _userNotification;
        private RandomGenerator _randomGenerator;

        [SetUp]
        public void SetUp()
        {
            _userResponse = Substitute.For<UserResponse>();
            _userNotification = Substitute.For<UserNotification>();
            _randomGenerator = Substitute.For<RandomGenerator>();
        }

        [Test]
        public void user_win_at_first_try()
        {
            GuessRandomNumberGame guessRandomNumberGame = new(_randomGenerator, _userNotification, _userResponse);
            _randomGenerator.GenerateRandomNumber().Returns(3);
            _userResponse.Get().Returns(3);

            guessRandomNumberGame.Run();

            _userNotification.Received(1).Notify("win game");
        }

        [Test]
        public void notify_user_when_number_is_wrong_and_is_lower_than_random_number()
        {
            GuessRandomNumberGame guessRandomNumberGame = new(_randomGenerator, _userNotification, _userResponse);
            _randomGenerator.GenerateRandomNumber().Returns(10);
            _userResponse.Get().Returns(5);

            guessRandomNumberGame.Run();

            _userNotification.Received(1).Notify("The number is higher");
        }
    }
}