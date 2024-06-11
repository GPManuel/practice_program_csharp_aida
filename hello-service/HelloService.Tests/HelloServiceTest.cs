using NSubstitute;
using NUnit.Framework;

namespace Hello.Tests
{
    public class HelloServiceTest
    {
        [Test]
        public void greet_good_night()
        {
            var notifier = Substitute.For<Notifier>();
            var clock = Substitute.For<Clock>();
            var helloService = new HelloService(notifier, clock);

            helloService.Hello();

            notifier.Received(1).Notify("Buenas noches!");
        }

        [Test]
        public void greet_good_morning_between_6am_and_12am()
        {
            var notifier = Substitute.For<Notifier>();
            var clock = Substitute.For<Clock>();
            var helloService = new HelloService(notifier, clock);
            clock.WhatTimeItIs().Returns(new TimeSpan(6, 0, 0));

            helloService.Hello();

            notifier.Received(1).Notify("Buenos días!");
        }
    }
}