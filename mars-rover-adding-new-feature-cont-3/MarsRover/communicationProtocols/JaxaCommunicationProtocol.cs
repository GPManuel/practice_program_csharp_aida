using MarsRover.commands;
using System.Collections.Generic;

namespace MarsRover.communicationProtocols;

public class JaxaCommunicationProtocol : CommunicationProtocol
{
    private static readonly List<string> Commands = new(){"del", "at", "iz", "der"};

    public JaxaCommunicationProtocol() : base(new ListCommandExtractor(Commands))
    {
    }

    protected override Command CreateCommand(int displacement, string commandRepresentation)
    {
        return commandRepresentation switch
        {
            "at" => new MovementBackward(displacement),
            "iz" => new RotationLeft(),
            "der" => new RotationRight(),
            _ => new MovementForward(displacement)
        };
    }
}