using MarsRover.Tests.helpers;
using NUnit.Framework;
using static MarsRover.Tests.helpers.RoverBuilder;

namespace MarsRover.Tests
{
    internal class RoverUsingJaxaCommunicationProtocolTest
    {
        [Test]
        public void No_Commands()
        {
            var rover = JaxaRover().Build();

            rover.Receive("");

            Assert.That(rover, Is.EqualTo(JaxaRover().Build()));
        }

        [Test]
        public void Forward_Commands()
        {
            var rover = JaxaRover().WithCoordinates(0, 0).Facing("N").Build();

            rover.Receive("del");

            Assert.That(rover, Is.EqualTo(JaxaRover().WithCoordinates(0, 1).Facing("N").Build()));
        }

        [Test]
        public void Backward_Commands()
        {
            var rover = JaxaRover().WithCoordinates(3, 3).Facing("E").Build();

            rover.Receive("at");

            Assert.That(rover, Is.EqualTo(JaxaRover().WithCoordinates(2, 3).Facing("E").Build()));
        }

        [Test]
        public void Rotate_Left_Commands()
        {
            var rover = JaxaRover().Facing("E").Build();

            rover.Receive("iz");

            Assert.That(rover, Is.EqualTo(JaxaRover().Facing("N").Build()));
        }

        [Test]
        public void Rotate_Right_Commands()
        {
            var rover = JaxaRover().Facing("W").Build();

            rover.Receive("der");

            Assert.That(rover, Is.EqualTo(JaxaRover().Facing("N").Build()));
        }
    }
}
