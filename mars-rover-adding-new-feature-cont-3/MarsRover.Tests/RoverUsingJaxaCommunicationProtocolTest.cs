using MarsRover.Tests.helpers;
using static MarsRover.Tests.helpers.RoverBuilder;

namespace MarsRover.Tests
{
    internal class RoverUsingJaxaCommunicationProtocolTest : RoverUsingCommunicationProtocolTest
    {
        protected override RoverBuilder GetRover()
        {
            return JaxaRover();
        }

        protected override string GetForwardRepresentationCommand()
        {
            return "del";
        }

        protected override string GetBackwardRepresentationCommand()
        {
            return "at";
        }

        protected override string GetRotateLeftRepresentationCommand()
        {
            return "iz";
        }

        protected override string GetRotateRightRepresentationCommand()
        {
            return "der";
        }
    }
}
