using System.Collections.Generic;
using System.Linq;
using MarsRover.communicationProtocols;

namespace MarsRover;

public abstract class CommunicationProtocol
{
    private readonly CommandExtractor _sizeCommandExtractor;

    protected CommunicationProtocol(CommandExtractor sizeCommandExtractor) {
        _sizeCommandExtractor = sizeCommandExtractor;
    }

    public List<Command> CreateCommands(string commandsSequence, int displacement) {
        var commandRepresentations = _sizeCommandExtractor.Extract(commandsSequence);
        return commandRepresentations
            .Select(commandRepresentation => CreateCommand(displacement, commandRepresentation))
            .ToList();
    }

    protected abstract Command CreateCommand(int displacement, string commandRepresentation);
}