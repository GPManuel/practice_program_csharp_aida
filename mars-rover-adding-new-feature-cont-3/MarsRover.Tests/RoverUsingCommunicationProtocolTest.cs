using MarsRover.Tests.helpers;
using NUnit.Framework;

namespace MarsRover.Tests;

public abstract class RoverUsingCommunicationProtocolTest
{
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

        rover.Receive(GetForwardRepresentationCommand());

        Assert.That(rover, Is.EqualTo(GetRover().WithCoordinates(0, 1).Facing("N").Build()));
    }

    [Test]
    public void Backward_Commands()
    {
        var rover = GetRover().WithCoordinates(3, 3).Facing("E").Build();

        rover.Receive(GetBackwardRepresentationCommand());

        Assert.That(rover, Is.EqualTo(GetRover().WithCoordinates(2, 3).Facing("E").Build()));
    }

    [Test]
    public void Rotate_Left_Commands()
    {
        var rover = GetRover().Facing("E").Build();

        rover.Receive(GetRotateLeftRepresentationCommand());

        Assert.That(rover, Is.EqualTo(GetRover().Facing("N").Build()));
    }

    [Test]
    public void Rotate_Right_Commands()
    {
        var rover = GetRover().Facing("W").Build();

        rover.Receive(GetRotateRightRepresentationCommand());

        Assert.That(rover, Is.EqualTo(GetRover().Facing("N").Build()));
    }

    [Test]
    public void Two_Commands()
    {
        var rover = GetRover().Facing("W").Build();

        rover.Receive(GetRotateLeftRepresentationCommand() + GetRotateRightRepresentationCommand());

        Assert.That(rover, Is.EqualTo(GetRover().Facing("W").Build()));
    }

    protected abstract RoverBuilder GetRover();
    protected abstract string GetForwardRepresentationCommand();
    protected abstract string GetBackwardRepresentationCommand();
    protected abstract string GetRotateLeftRepresentationCommand();
    protected abstract string GetRotateRightRepresentationCommand();
}