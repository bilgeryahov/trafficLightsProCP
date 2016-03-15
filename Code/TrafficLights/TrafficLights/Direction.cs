using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficLights
{
    public enum Direction
    {
        Up, Down, Left, Right
    }

    public static class DirectionExtention
    {
        public static Direction Inverse(this Direction d)
        {
            if (d == Direction.Up) return Direction.Down;
            if (d == Direction.Down) return Direction.Up;
            if (d == Direction.Left) return Direction.Right;
            if (d == Direction.Right) return Direction.Left;
            throw new NotImplementedException("UNKNOWN DIRECTION " + d);
        }
    }
}
