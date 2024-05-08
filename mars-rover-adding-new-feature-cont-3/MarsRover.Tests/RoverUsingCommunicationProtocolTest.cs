using MarsRover.Tests.helpers;
using NUnit.Framework;
using System.Collections.Generic;

namespace MarsRover.Tests;

public abstract class RoverUsingCommunicationProtocolTest
{
    protected enum Commands
    {
        BackwardCommand,
        ForwardCommand,
        RotateLeftCommand,
        RotateRightCommand
    }

    [Test]
    public void No_Commands()
    {
        var rover = GetRover().Build();

        rover.Receive("");

        Assert.That(rover, Is.EqualTo(GetRover().Build()));
    }

    [Test]
    public void Forward_Commands()
    {
        var rover = GetRover().WithCoordinates(0, 0).Facing("N").Build();

        rover.Receive(GetRepresentationFor(Commands.ForwardCommand));

        Assert.That(rover, Is.EqualTo(GetRover().WithCoordinates(0, 1).Facing("N").Build()));
    }

    [Test]
    public void Backward_Commands()
    {
        var rover = GetRover().WithCoordinates(3, 3).Facing("E").Build();

        rover.Receive(GetRepresentationFor((Commands.BackwardCommand)));

        Assert.That(rover, Is.EqualTo(GetRover().WithCoordinates(2, 3).Facing("E").Build()));
    }

    [Test]
    public void Rotate_Left_Commands()
    {
        var rover = GetRover().Facing("E").Build();

        rover.Receive(GetRepresentationFor(Commands.RotateLeftCommand));

        Assert.That(rover, Is.EqualTo(GetRover().Facing("N").Build()));
    }

    [Test]
    public void Rotate_Right_Commands()
    {
        var rover = GetRover().Facing("W").Build();

        rover.Receive(GetRepresentationFor(Commands.RotateRightCommand));

        Assert.That(rover, Is.EqualTo(GetRover().Facing("N").Build()));
    }

    [Test]
    public void Two_Commands()
    {
        var rover = GetRover().Facing("W").Build();

        var commandSequence = GetRepresentationFor(Commands.RotateLeftCommand)
                                    + GetRepresentationFor(Commands.RotateRightCommand);

        rover.Receive(commandSequence);

        Assert.That(rover, Is.EqualTo(GetRover().Facing("W").Build()));
    }

    protected abstract RoverBuilder GetRover();
    protected abstract Dictionary<Commands, string> ConfigureRepresentationCommands();

    private string GetRepresentationFor(Commands command)
    {
        return ConfigureRepresentationCommands()[command];
    }
}