using NSubstitute;
using NUnit.Framework;

namespace Hello.Tests
{
    public class HelloServiceTest
    {
        private Notifier _notifier;
        private Clock _clock;
        private HelloService _helloService;

        [SetUp]
        public void SetUp()
        {
            _notifier = Substitute.For<Notifier>();
            _clock = Substitute.For<Clock>();
            _helloService = new HelloService(_notifier, _clock);
        }

        [Test]
        public void greet_good_night()
        {
            _helloService.Hello();

            _notifier.Received(1).Notify("Buenas noches!");
        }

        [TestCase(6)]
        [TestCase(9)]
        [TestCase(12)]
        public void greet_good_morning_between_6am_and_12am(int hour)
        {
            _clock.WhatTimeItIs().Returns(new TimeOnly(hour, 0, 0));

            _helloService.Hello();

            _notifier.Received(1).Notify("Buenos d�as!"); 
            _notifier.Received(1).Notify(Arg.Any<string>());
        }

        [Test]
        public void greet_good_afternoon_between_12am_and_8pm()
        {
            _clock.WhatTimeItIs().Returns(new TimeOnly(12, 0, 1));

            _helloService.Hello();

            _notifier.Received(1).Notify("Buenas tardes!");
        }
    }
}