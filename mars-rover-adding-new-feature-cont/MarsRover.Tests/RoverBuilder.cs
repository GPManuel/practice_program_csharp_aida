namespace MarsRover.Tests;

public class RoverBuilder
{
    private int _x;
    private int _y;
    private string _direction;

    private RoverBuilder(int x, int y, string direction)
    {
        _x = x;
        _y = y;
        _direction = direction;
    }

    public Rover Build()
    {
        return new Rover(_x, _y, _direction);
    }

    public RoverBuilder Facing(string direction)
    {
        _direction = direction;
        return this;
    }

    public RoverBuilder WithCoordinates(int x, int y)
    {
        _x = x;
        _y = y;
        return this;
    }

    public static RoverBuilder ANASARover()
    {
        return new RoverBuilder(0,0,"N");
    }
}