
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficLights
{
    /// <summary>
    /// the direction an object can go within the simulation
    /// </summary>
    [Flags] // Allows Direction d = Direction.UP | Direction. Down; then to compare call d.HasFlag(Direction.Left)
    public enum Direction
    {
        Up, Down, Left, Right
    }

    /// <summary>
    /// holds extension methods for the base Direction enum
    /// </summary>
    public static class DirectionExtention
    {
        /// <summary>
        /// Inverses the specified direction.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns>Direction.</returns>
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
