using System;
using System.Linq;

namespace MarsRover
{
    public class Rover
    {
        private const int Displacement = 1;
        private Location _location;

        public Rover(int x, int y, string direction)
        {
            var direction1 = DirectionMapper.Create(direction);
            var coordinates = new Coordinates(x, y);
            _location = new Location(direction1, coordinates);
        }

        public void Receive(string commandsSequence)
        {
            var commandsRepresentations = commandsSequence.Select(Char.ToString).ToList();
            foreach (var commandRepresentation in commandsRepresentations.ToList())
            {
                Command command;
                if (commandRepresentation.Equals("l"))
                {
                    command = new RotationLeft();
                }
                else if (commandRepresentation.Equals("r"))
                {
                    command = new RotationRight();
                }
                else if (commandRepresentation.Equals("f"))
                {
                    command = new MovementForward(Displacement);
                }
                else
                {
                    command = new MovementBackward(Displacement);
                }
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