using NUnit.Framework;
using static MarsRover.Tests.RoverBuilder;

namespace MarsRover.Tests;

public class RoverEqualityTest
{
    [Test]
    public void Equal_Rovers()
    {
        Assert.That(ANASARover().Build(), Is.EqualTo(ANASARover().Build()));
    }

    [Test]
    public void Not_Equal_Rovers()
    {
        Assert.That(ANASARover().Facing("N").Build(), Is.Not.EqualTo(ANASARover().Facing("S").Build()));
        Assert.That(ANASARover().WithCoordinates(x: 1, y:1).Build(), Is.Not.EqualTo(ANASARover().WithCoordinates(x: 1, y: 2).Build()));
        Assert.That(ANASARover().WithCoordinates(x: 0, y: 1).Build(), Is.Not.EqualTo(ANASARover().WithCoordinates(x: 2, y: 1).Build()));
    }
}