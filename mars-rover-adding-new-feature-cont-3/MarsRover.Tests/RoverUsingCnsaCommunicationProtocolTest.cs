using MarsRover.Tests.helpers;
using System.Collections.Generic;
using static MarsRover.Tests.helpers.RoverBuilder;

namespace MarsRover.Tests;

public class RoverUsingCnsaCommunicationProtocolTest : RoverUsingCommunicationProtocolTest
{
    protected override RoverBuilder GetRover()
    {
        return CsnaRover();
    }

    protected override Dictionary<Commands, string> ConfigureRepresentationCommands()
    {
        return new Dictionary<Commands, string>()
        {
            { Commands.ForwardCommand , "bx"},
            { Commands.BackwardCommand , "tf"},
            { Commands.RotateLeftCommand , "ah"},
            { Commands.RotateRightCommand , "pl"},
        };
    }
}