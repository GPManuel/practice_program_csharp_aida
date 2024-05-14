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
    }

    public interface UserResponse
    {
        int Get();
    }

    public interface UserNotification
    {
        void Notify(string winGame);
    }

    public interface RandomGenerator
    {
        int GenerateRandomNumber();
    }

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
}