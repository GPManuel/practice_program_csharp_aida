using System.Collections.Generic;
using System.Linq;

namespace MarsRover.communicationProtocols;

public class SizeCommandExtractor : CommandExtractor
{
    private readonly uint _length;

    public SizeCommandExtractor(uint length)
    {
        _length = length;
    }

    public List<string> Extract(string commandsSequence)
    {
        return commandsSequence
            .Chunk((int)_length)
            .Select(arr => string.Join("", arr)).ToList();
    }
}
