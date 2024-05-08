using MarsRover.communicationProtocols;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MarsRover.Tests;

public class ListCommandExtractorTest
{
    /*
     * ✅: del -> Secuencia 1 comando del 
     * ✅ deldeldel -> Secuencia 3 comandos de del 
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

    [Test]
    public void extract_command_with_many_of_the_same_command_sequence()
    {
        var commands = new List<string>() { "del" };
        ListCommandExtractor commandExtractor = new(commands);

        var result = commandExtractor.Extract("deldeldel");

        Assert.That(result, Is.EqualTo(new List<string>() { "del", "del", "del" }));
    }

    [Test]
    public void extract_command_with_many_of_the_different_command_sequence()
    {
        var commands = new List<string>() { "del", "at", "iz" };
        ListCommandExtractor commandExtractor = new(commands);

        var result = commandExtractor.Extract("delatatdel");

        Assert.That(result, Is.EqualTo(new List<string>() { "del", "at","at", "del" }));
    }

    [Test]
    public void extract_command_with_unknow_characters_sequence()
    {
        var commands = new List<string>() { "del", "at", "iz" };
        ListCommandExtractor commandExtractor = new(commands);

        var result = commandExtractor.Extract("delYYatatXXdel");

        Assert.That(result, Is.EqualTo(new List<string>() { "del", "at", "at", "del" }));
    }
}