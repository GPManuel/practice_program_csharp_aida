using MarsRover.Tests.helpers;
using static MarsRover.Tests.helpers.RoverBuilder;

namespace MarsRover.Tests;

public class RoverUsingEsaCommunicationProtocolTest : RoverUsingCommunicationProtocolTest
{
    protected override RoverBuilder GetRover()
    {
        return EsaRover();
    }

    protected override string GetRepresentationFor(Commands command)
    {
        return command switch
        {
            Commands.ForwardCommand => "b",
            Commands.BackwardCommand => "x",
            Commands.RotateLeftCommand => "f",
            Commands.RotateRightCommand => "l",
        };
    }
}