using MarsRover.Tests.helpers;
using NUnit.Framework;
using static MarsRover.Tests.helpers.RoverBuilder;

namespace MarsRover.Tests;

public class RoverUsingEsaCommunicationProtocolTest : RoverUsingCommunicationProtocolTest
{
    protected override RoverBuilder GetRover()
    {
        return EsaRover();
    }

    protected override string GetForwardRepresentationCommand()
    {
        return "b";
    }

    protected override string GetBackwardRepresentationCommand()
    {
        return "x";
    }

    protected override string GetRotateLeftRepresentationCommand()
    {
        return "f";
    }

    protected override string GetRotateRightRepresentationCommand()
    {
        return "l";
    }
}