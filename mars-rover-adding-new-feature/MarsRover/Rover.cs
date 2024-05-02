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
            var commands = commandsSequence.Select(Char.ToString).ToList();
            foreach (var command in commands.ToList())
            {
                if (command.Equals("l"))
                {
                    _location = _location.RotateLeft();
                }
                else if (command.Equals("r"))
                {
                    _location = _location.RotateRight();
                }
                else if (command.Equals("f"))
                {
                    _location = _location.Move(Displacement);
                }
                else
                {
                    _location = _location.Move(-Displacement);
                }
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
}