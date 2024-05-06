using System.Collections.Generic;
using MarsRover.commands;

namespace MarsRover;

public class EsaCommunicationProtocol : CommunicationProtocol
{
    public List<Command> CreateCommands(string commandsSequence, int displacement)
    {
        var commands = new List<Command>();
        foreach (var commandRepresentation in commandsSequence)
        {
            if (commandRepresentation == 'b') {
                commands.Add(new MovementForward(displacement));
            }
            else if (commandRepresentation == 'x') {
                commands.Add(new MovementBackward(displacement));
            }

            else if (commandRepresentation == 'f') {
                commands.Add(new RotationLeft());
            }
            else {
                commands.Add(new RotationRight());
            }
        }
        return commands;
    }
}