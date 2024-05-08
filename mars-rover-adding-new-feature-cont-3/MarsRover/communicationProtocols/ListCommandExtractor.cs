using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
        var commandsFound = new List<string>();

        RecursiveExtractCommands(commandsSequence, commandsFound);

        return commandsFound;
    }

    private void RecursiveExtractCommands(string commandsSequence, List<string> commandsFound)
    {
        foreach (var command in _commands)
        {
            if (commandsSequence.StartsWith(command))
            {
                commandsFound.Add(command);
                commandsSequence = commandsSequence.Substring(command.Length, commandsSequence.Length - command.Length);
            }
        }

        if (!String.IsNullOrEmpty(commandsSequence))
        {
            RecursiveExtractCommands(commandsSequence, commandsFound);
        }
    }
}