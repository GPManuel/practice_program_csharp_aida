using System.Collections.Generic;

namespace MarsRover
{
    public class Rover
    {
        private const int Displacement = 1;
        private Location _location;
        private readonly NASACommunicationProtocol _communicationProtocol;

        public Rover(int x, int y, string direction)
        {
            _location = new Location(DirectionMapper.Create(direction), new Coordinates(x, y));
            _communicationProtocol = new NASACommunicationProtocol();
        }

        public void Receive(string commandsSequence)
        {
            var commands = _communicationProtocol.CreateCommands(commandsSequence, Displacement);

            Execute(commands);
        }

        private void Execute(List<Command> commands)
        {
            foreach (var command in commands)
            {
                _location = command.Execute(_location);
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Rover)obj);
        }

        protected bool Equals(Rover other)
        {
            return Equals(_location, other._location);
        }

        public override int GetHashCode()
        {
            return (_location != null ? _location.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return $"{nameof(_location)}: {_location}";
        }
    }

    public class MovementBackward : Command
    {
        private readonly int _displacement;

        public MovementBackward(int displacement)
        {
            _displacement = displacement;
        }

        public Location Execute(Location location)
        {
            return location.Move(-_displacement);
        }
    }

    public class MovementForward : Command
    {
        private readonly int _displacement;

        public MovementForward(int displacement)
        {
            _displacement = displacement;
        }

        public Location Execute(Location location)
        {
            return location.Move(_displacement);
        }
    }

    public class RotationRight : Command
    {
        public Location Execute(Location location)
        {
            return location.RotateRight();
        }
    }

    public class RotationLeft : Command
    {
        public Location Execute(Location location)
        {
            return location.RotateLeft();
        }
    }

    public interface Command
    {
        public Location Execute(Location location);
    }
}