namespace MarsRover.Tests;

public class RoverBuilder
{
    private int _x;
    private int _y;
    private string _direction;
    private CommunicationProtocol _communicationProtocol;

    private RoverBuilder(int x, int y, string direction, CommunicationProtocol communicationProtocol)
    {
        _x = x;
        _y = y;
        _direction = direction;
        _communicationProtocol = communicationProtocol;
    }

    public Rover Build()
    {
        return new Rover(_x, _y, _direction, _communicationProtocol);
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
        return new RoverBuilder(0,0,"N", new NasaCommunicationProtocol());
    }

    public static RoverBuilder AnESARover()
    {
        return new RoverBuilder(0, 0, "N", new EsaCommunicationProtocol());
    }
}