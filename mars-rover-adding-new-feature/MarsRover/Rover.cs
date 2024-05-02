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
            SetLocation(direction1, coordinates);
        }

        private void SetLocation(Direction direction, Coordinates coordinates)
        {
            _location = new Location(direction, coordinates);
        }

        public void Receive(string commandsSequence)
        {
            var commands = commandsSequence.Select(Char.ToString).ToList();
            foreach (var command in commands.ToList())
            {
                if (command.Equals("l"))
                {
                    SetLocation(_location.GetDirection().RotateLeft(), _location.GetCoordinates());
                }
                else if (command.Equals("r"))
                {
                    SetLocation(_location.GetDirection().RotateRight(), _location.GetCoordinates());

                }
                else if (command.Equals("f"))
                {
                    SetLocation(_location.GetDirection(), _location.GetDirection().Move(_location.GetCoordinates(), Displacement));
                }
                else
                {
                    SetLocation(_location.GetDirection(), _location.GetDirection().Move(_location.GetCoordinates(), -Displacement));
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

    internal class Location
    {
        private readonly Direction _direction;
        private readonly Coordinates _coordinates;

        public Location(Direction direction, Coordinates coordinates)
        {
            _direction = direction;
            _coordinates = coordinates;
        }

        protected bool Equals(Location other)
        {
            return Equals(_direction, other._direction) && Equals(_coordinates, other._coordinates);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Location)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_direction, _coordinates);
        }

        public override string ToString()
        {
            return $"{nameof(_direction)}: {_direction}, {nameof(_coordinates)}: {_coordinates}";
        }

        public Direction GetDirection()
        {
            return _direction;
        }

        public Coordinates GetCoordinates()
        {
            return _coordinates;
        }
    }
}