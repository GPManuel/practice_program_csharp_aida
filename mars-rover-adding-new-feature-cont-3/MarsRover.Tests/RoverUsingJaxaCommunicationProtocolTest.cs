using MarsRover.Tests.helpers;
using System.Collections.Generic;
using static MarsRover.Tests.helpers.RoverBuilder;

namespace MarsRover.Tests
{
    internal class RoverUsingJaxaCommunicationProtocolTest : RoverUsingCommunicationProtocolTest
    {
        protected override RoverBuilder GetRover()
        {
            return JaxaRover();
        }

        protected override Dictionary<Commands, string> ConfigureRepresentationCommands()
        {
            return new Dictionary<Commands, string>()
            {
                { Commands.ForwardCommand , "del"},
                { Commands.BackwardCommand , "at"},
                { Commands.RotateLeftCommand , "iz"},
                { Commands.RotateRightCommand , "der"},
            };
        }
    }
}
