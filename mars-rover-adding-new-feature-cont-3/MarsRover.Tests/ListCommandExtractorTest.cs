using MarsRover.communicationProtocols;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MarsRover.Tests;

public class ListCommandExtractorTest
{
    /*
     * del -> Secuencia 1 comando del 
     * deldeldel -> Secuencia 3 comandos de del 
     * atdel -> Secuencia dos comandos at y del
     * izdelat -> Secuencia 3 comandos iz, del, at 
     * derdelatiz -> Secuencia 4 comandos der, del, at, iz
     *
     * delXZYat -> Oops: Dos comandos del y at
     * ✅: "" -> Zero: no comands
     */

    [Test]
    public void extract_command_empty_sequence()
    {
        var commands = new List<string>() { "del" };
        ListCommandExtractor commandExtractor = new(commands);

        var result = commandExtractor.Extract("");

        Assert.That(result, Is.EqualTo(Enumerable.Empty<List<string>>()));
    }

    [Test]
    public void extract_command_with_one_command_sequence()
    {
        var commands = new List<string>() { "del" };
        ListCommandExtractor commandExtractor = new(commands);

        var result = commandExtractor.Extract("del");

        Assert.That(result, Is.EqualTo(new List<string>() { "del" }));
    }
}

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
