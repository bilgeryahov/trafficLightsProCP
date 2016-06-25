using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
            int startX = lane.X;
            int startY = lane.Y;

            //single
            if (lane.To.HasFlag(Direction.Left) && lane.To.HasFlag(Direction.Down) && next.To.HasFlag(Direction.Left))
            {
                midX = next.X + 100;
                midY = lane.Y - 15;
            }
            if (lane.To.HasFlag(Direction.Right) && next.To.HasFlag(Direction.Right) && next.From.HasFlag(Direction.Left))
            {
                midX = lane.X;
                midY = next.Y;
            }
            if (lane.To.HasFlag(Direction.Up) && next.To.HasFlag(Direction.Up) && next.From.HasFlag(Direction.Down))
            {
                midX = next.X;
                midY = lane.Y;
            }
            if (lane.To.HasFlag(Direction.Left) && next.To.HasFlag(Direction.Left) && next.From.HasFlag(Direction.Right))
            {
                midX = lane.X;
                midY = next.Y;
            }
            if (lane.To.HasFlag(Direction.Down) && next.To.HasFlag(Direction.Down) && next.From.HasFlag(Direction.Up))
            {
                midX = next.X;
                midY = lane.Y;
            }
            //double
            if (lane.To.HasFlag(Direction.Down) && lane.To.HasFlag(Direction.Right) && next.To.HasFlag(Direction.Down) && next.From.HasFlag(Direction.Up))
            {
                if (lane.Owner.Owner is CrossingA)
                {
                    midX = lane.X - 20;
                    midY = next.Y - 30;
                }
                else
                {
                    midX = next.X;
                    midY = next.Y - 30;
                }
            }
            if (lane.To.HasFlag(Direction.Left) && lane.To.HasFlag(Direction.Up) && next.To.HasFlag(Direction.Up) && next.From.HasFlag(Direction.Right))
            {
                midX = lane.X;
                midY = next.Y;
            }
            if (lane.To.HasFlag(Direction.Right) && lane.To.HasFlag(Direction.Up) && next.To.HasFlag(Direction.Up) && next.From.HasFlag(Direction.Down))
            {
                midX = next.X;
                midY = lane.Y;

            }
            if (lane.To.HasFlag(Direction.Left) && lane.To.HasFlag(Direction.Up) && next.To.HasFlag(Direction.Up) && next.From.HasFlag(Direction.Down))
            {
                if (lane.Owner.Owner is CrossingA)
                {
                    midX = next.X - 5;
                    midY = lane.Y - 80;
                }
                else
                {
                    midX = next.X;
                    midY = lane.Y;
                }
            }
            if (lane.To.HasFlag(Direction.Down) && lane.To.HasFlag(Direction.Left) && next.To.HasFlag(Direction.Down) && next.From.HasFlag(Direction.Up))
            {
                if (lane.Owner.Owner is CrossingA)
                {
                    midX = next.X;
                    midY = lane.Y;
                }
                else
                {
                    midX = lane.X;
                    midY = next.Y;
                }
            }
            if (lane.To.HasFlag(Direction.Down) && lane.To.HasFlag(Direction.Left) && next.To.HasFlag(Direction.Left) && next.From.HasFlag(Direction.Right))
            {
                if (lane.Owner.Owner is CrossingA)
                {
                    midX = next.X + 10;
                    midY = next.Y - 5;
                }
                else
                {
                    midX = lane.X;
                    midY = next.Y;
                }
            }
            if (lane.To.HasFlag(Direction.Up) && lane.To.HasFlag(Direction.Right) && next.To.HasFlag(Direction.Right) && next.From.HasFlag(Direction.Left))
            {
                if (lane.Owner.Owner is CrossingA)
                {
                    midX = lane.X + 160;
                    midY = next.Y;
                }
                else
                {
                    midX = lane.X;
                    midY = next.Y;
                }
            }


            if (lane.To.HasFlag(Direction.Left) && lane.To.HasFlag(Direction.Up))
            { startY += 30; }
            if (lane.To.HasFlag(Direction.Left) && lane.To.HasFlag(Direction.Down))
            { startX += 30; }
            if (lane.To.HasFlag(Direction.Down) && lane.To.HasFlag(Direction.Right))
            { startY -= 30; }
            System.Drawing.Point start = new System.Drawing.Point(startX, startY);
            System.Drawing.Point end = new System.Drawing.Point(next.X, next.Y);
            System.Drawing.Point mid = new System.Drawing.Point(midX, midY);

            if (next.To == Direction.Up)
            { end.Y -= 60; }
            else if (next.To == Direction.Down)
            { end.Y += 60; }
            else if (next.To == Direction.Left)
            { end.X -= 50; }
            else if (next.To == Direction.Right)
            { end.X += 50; }

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
            base.Update(seconds);
            if (OneCycleHasPassed)
            {
                CurrentLane = CurrentLane.Next;

                // Increase the number of crossed times of the crossing owner.
                this.CurrentLane.Owner.Owner.XTimesCrossed++;

                if (CurrentLane != null)
                {
                    if (!CurrentLane.IsFeeder)
                    {
                        CurrentLane = CurrentLane.Next;
                    }
                    if (CurrentLane != null)
                    {
                        CurrentLane.IncreaseAccumulatedFlow();
                    }
                }
                else
                {
                    //car is out of circuit += 1 @ simulation
                }
            }
        }

        /// <summary>
        /// Draws the when normal.
        /// </summary>
        /// <param name="image">The image.</param>
        protected override void DrawWhenNormal(System.Drawing.Graphics image)
        {
            //draws a rectangle on the car's location
            image.FillEllipse(System.Drawing.Brushes.Black, this.X + 8, this.Y + 8, 8, 8);
        }

        /// <summary>
        /// Draws the when active.
        /// </summary>
        /// <param name="image">The image.</param>
        protected override void DrawWhenActive(System.Drawing.Graphics image)
        {
            DrawWhenNormal(image);
        }
    }
}
