using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRover
{
    public class Rover
    {
        private const int Displacement = 1;
        private Direction _direction;
        private Coordinates _coordinates;

        public Rover(int x, int y, string direction)
        {
            var direction1 = DirectionMapper.Create(direction);
            var coordinates = new Coordinates(x, y);
            SetLocation(direction1, coordinates);
        }

        private void SetLocation(Direction direction, Coordinates coordinates)
        {
            _direction = direction;
            _coordinates = coordinates;
        }

        public void Receive(string commandsSequence)
        {
            var commands = commandsSequence.Select(Char.ToString).ToList();
            foreach (var command in commands.ToList())
            {
                if (command.Equals("l"))
                {
                    SetLocation(_direction.RotateLeft(), _coordinates);
                }
                else if (command.Equals("r"))
                {
                    SetLocation(_direction.RotateRight(), _coordinates);

                }
                else if (command.Equals("f"))
                {
                    SetLocation(_direction, _direction.Move(_coordinates, Displacement));
                }
                else
                {
                    SetLocation(_direction, _direction.Move(_coordinates, -Displacement));
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Rover)obj);
        }

        protected bool Equals(Rover other)
        {
            return Equals(_direction, other._direction) && Equals(_coordinates, other._coordinates);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_direction, _coordinates);
        }

        public override string ToString()
        {
            return $"{nameof(_direction)}: {_direction}, {nameof(_coordinates)}: {_coordinates}";
        }
    }
}