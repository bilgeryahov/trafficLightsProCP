using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    [Serializable]
    /// <summary>
    /// a renderable component within the simulation that moves on a road
    /// </summary>
    /// <seealso cref="TrafficLights.Moveable" />
    public class Car : Moveable
    {
        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        /// <value>The direction.</value>
        public Direction Direction { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [in middle of crosswalk].
        /// </summary>
        /// <value><c>true</c> if [in middle of crosswalk]; otherwise, <c>false</c>.</value>
        public bool InMiddleOfCrosswalk { get; set; }

        /// <summary>
        /// Gets or sets the current lane.
        /// </summary>
        /// <value>The current lane.</value>
        public Lane CurrentLane { get; private set; }

        public Car(int startX, int startY, Lane lane) : base(startX, startY, PathFromLane(lane)) { this.CurrentLane = lane; }

        static System.Drawing.Point[] PathFromLane(Lane lane)
        {
            Lane next = lane.Next;

            int midX = lane.X;
            int midY = next.Y;

            if (lane.To.HasFlag(next.From))
                if (lane.To.HasFlag(Direction.Left))
                    midX = lane.X / 2;
                else if (lane.To.HasFlag(Direction.Right))
                    midX = next.X / 2;
                else if (lane.To.HasFlag(Direction.Down))
                    midY = lane.Y / 2;
                else if (lane.To.HasFlag(Direction.Up))
                    midY = next.Y / 2;

            System.Drawing.Point start = new System.Drawing.Point(lane.X, lane.Y);
            System.Drawing.Point end = new System.Drawing.Point(next.X, next.Y);
            System.Drawing.Point mid = new System.Drawing.Point(start.X, end.Y);
            return new System.Drawing.Point[] { start, mid, end };
        }

        /// <summary>
        /// Updates the specified seconds.
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        public override void Update(float seconds)
        {
            //move to end; if at end -> get next lane on the Crossing
            //if no lane found -> nothing happens
            //if lane found - Lane.IncreaseAccumlatedFlow
            //moves the car based on the elapsed time
            throw new NotImplementedException();
        }

        /// <summary>
        /// Draws the when normal.
        /// </summary>
        /// <param name="image">The image.</param>
        protected override void DrawWhenNormal(System.Drawing.Graphics image)
        {
            //draws a rectangle on the car's location
            throw new NotImplementedException();
        }

        /// <summary>
        /// Draws the when active.
        /// </summary>
        /// <param name="image">The image.</param>
        protected override void DrawWhenActive(System.Drawing.Graphics image)
        {
            throw new NotImplementedException();
        }
    }
}
