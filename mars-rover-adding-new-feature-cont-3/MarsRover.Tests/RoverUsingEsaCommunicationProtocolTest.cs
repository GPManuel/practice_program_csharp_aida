using MarsRover.Tests.helpers;
using System.Collections.Generic;
using static MarsRover.Tests.helpers.RoverBuilder;

namespace MarsRover.Tests;

public class RoverUsingEsaCommunicationProtocolTest : RoverUsingCommunicationProtocolTest
{
    protected override RoverBuilder GetRover()
    {
        return EsaRover();
    }

    protected override Dictionary<Commands, string> ConfigureRepresentationCommands()
    {
        return new Dictionary<Commands, string>()
        {
            { Commands.ForwardCommand , "b"},
            { Commands.BackwardCommand , "x"},
            { Commands.RotateLeftCommand , "f"},
            { Commands.RotateRightCommand , "l"},
        };
    }
}