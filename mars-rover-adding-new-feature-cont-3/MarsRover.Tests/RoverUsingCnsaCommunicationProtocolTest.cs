using MarsRover.Tests.helpers;
using NUnit.Framework;
using static MarsRover.Tests.helpers.RoverBuilder;

namespace MarsRover.Tests;

public class RoverUsingCnsaCommunicationProtocolTest : RoverUsingCommunicationProtocolTest
{
    protected override RoverBuilder GetRover()
    {
        return CsnaRover();
    }

    protected override string GetForwardRepresentationCommand()
    {
        return "bx";
    }

    protected override string GetBackwardRepresentationCommand()
    {
        return "tf";
    }

    protected override string GetRotateLeftRepresentationCommand()
    {
        return "ah";
    }

    protected override string GetRotateRightRepresentationCommand()
    {
        return "pl";
    }
}