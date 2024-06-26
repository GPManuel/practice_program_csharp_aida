using MarsRover.commands;

namespace MarsRover.communicationProtocols;

public class EsaCommunicationProtocol : CommunicationProtocol
{
    public EsaCommunicationProtocol() : base(new SizeCommandExtractor(1))
    {
    }

    protected override Command CreateCommand(int displacement, string commandRepresentation)
    {
        return commandRepresentation switch
        {
            "b" => new MovementForward(displacement),
            "x" => new MovementBackward(displacement),
            "f" => new RotationLeft(),
            _ => new RotationRight()
        };
    }
}