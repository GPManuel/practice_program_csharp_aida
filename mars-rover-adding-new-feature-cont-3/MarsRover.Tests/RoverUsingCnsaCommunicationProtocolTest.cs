using MarsRover.Tests.helpers;
using static MarsRover.Tests.helpers.RoverBuilder;

namespace MarsRover.Tests;

public class RoverUsingCnsaCommunicationProtocolTest : RoverUsingCommunicationProtocolTest
{
    protected override RoverBuilder GetRover()
    {
        return CsnaRover();
    }

    protected override string GetRepresentationFor(Commands command)
    {
        return command switch
        {
            Commands.ForwardCommand => "bx",
            Commands.BackwardCommand => "tf",
            Commands.RotateLeftCommand => "ah",
            Commands.RotateRightCommand => "pl",
        };
    }
}