using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRover;

public class NASACommunicationProtocol
{
    public List<Command> CreateCommands(string commandsSequence, int displacement)
    {
        return commandsSequence
            .Select(char.ToString)
            .Select(commandRepresentation => CreateCommand(displacement, commandRepresentation))
            .ToList();
    }

    private Command CreateCommand(int displacement, string commandRepresentation)
    {
        Command command;
        if (commandRepresentation.Equals("l"))
        {
            command = new RotationLeft();
        }
        else if (commandRepresentation.Equals("r"))
        {
            command = new RotationRight();
        }
        else if (commandRepresentation.Equals("f"))
        {
            command = new MovementForward(displacement);
        }
        else
        {
            command = new MovementBackward(displacement);
        }

        return command;
    }
}