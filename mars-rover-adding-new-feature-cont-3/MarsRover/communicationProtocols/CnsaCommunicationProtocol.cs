using MarsRover.commands;

namespace MarsRover.communicationProtocols;

public class CnsaCommunicationProtocol : CommunicationProtocol
{
    public CnsaCommunicationProtocol() : base(new SizeCommandExtractor(2))
    {
    }

    protected override Command CreateCommand(int displacement, string commandRepresentation)
    {
        return commandRepresentation switch
        {
            "bx" => new MovementForward(displacement),
            "tf" => new MovementBackward(displacement),
            "ah" => new RotationLeft(),
            _ => new RotationRight()
        };
    }
}