using System.Collections.Generic;
using System.Linq;

namespace MarsRover.communicationProtocols;

public class ListCommandExtractor : CommandExtractor
{
    private readonly List<string> _commands;

    public ListCommandExtractor(List<string> commands)
    {
        _commands = commands;
    }

    public List<string> Extract(string commandsSequence)
    {
        if (commandsSequence.Equals(_commands.First()))
        {
            return new List<string>() { commandsSequence };
        }

        return new List<string>();
    }
}