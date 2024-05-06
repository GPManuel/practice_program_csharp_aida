using System.Collections.Generic;
using MarsRover.commands;

namespace MarsRover;

public class EsaCommunicationProtocol : CommunicationProtocol
{
    public List<Command> CreateCommands(string commandsSequence, int displacement)
    {
        if (commandsSequence == "")
        {
            return new List<Command>();
        }
        
        return new List<Command>() { new MovementForward(displacement) };
    }
}