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
    }
}