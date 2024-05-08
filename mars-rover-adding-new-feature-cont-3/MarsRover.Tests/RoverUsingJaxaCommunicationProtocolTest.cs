using MarsRover.Tests.helpers;
using static MarsRover.Tests.helpers.RoverBuilder;

namespace MarsRover.Tests;

internal class RoverUsingJaxaCommunicationProtocolTest : RoverUsingCommunicationProtocolTest
{
    protected override RoverBuilder GetRover()
    {
        return JaxaRover();
    }

    protected override string GetRepresentationFor(Commands command)
    {
        return command switch
        {
            Commands.ForwardCommand => "del",
            Commands.BackwardCommand => "at",
            Commands.RotateLeftCommand => "iz",
            Commands.RotateRightCommand => "der",
        };
    }
}
