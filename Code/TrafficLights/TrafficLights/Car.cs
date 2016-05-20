﻿using System;
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

            if(false)
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
            System.Drawing.Point mid = new System.Drawing.Point(end.X, start.Y);

            if (next.To == Direction.Up)
            { }
            else if (next.To == Direction.Down)
                end.Y += 60;
            else if (next.To == Direction.Left)
            { }
            else if (next.To == Direction.Right)
                end.X += 60;


            return new System.Drawing.Point[] { start, mid, end };
        }

        const float DefaultTimeFromStartToEnd = 1;
        private float currentPassed = 0;

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
            currentPassed += seconds;
            if (currentPassed >= DefaultTimeFromStartToEnd)
            {
                //set next lane
                currentPassed = 0;
            }
            else
            {
                float avrg = DefaultTimeFromStartToEnd / (Path.Count - 1);
                int leftIndex = (int)(currentPassed / avrg);
                int rightIndex = leftIndex + 1;
                System.Drawing.Point leftPoint = Path[leftIndex];
                System.Drawing.Point rightPoint = Path[rightIndex];

                float percentPassed = currentPassed / avrg;

                if (percentPassed.ToString().IndexOf('.') != -1)
                    percentPassed = float.Parse("0."+percentPassed.ToString().Split('.').Last());
                else
                    percentPassed = 1;

                this.X = leftPoint.X + (int)((rightPoint.X - leftPoint.X) * percentPassed);
                this.Y = leftPoint.Y + (int)((rightPoint.Y - leftPoint.Y) * percentPassed);
            }

            return;

            if (CurrentPoint != this.Path[this.Path.Count-1])
            {
                if (CurrentPoint == PathFromLane(CurrentLane)[0])
                {
                    X = PathFromLane(CurrentLane)[1].X;
                    Y = PathFromLane(CurrentLane)[1].Y;
                }
                else if (CurrentPoint == PathFromLane(CurrentLane)[1])
                { X = PathFromLane(CurrentLane)[2].X;
                Y = PathFromLane(CurrentLane)[2].Y;
                }              
            }
            else 
            {
                CurrentLane = CurrentLane.Next; 
                // if no lane -> out of circuit
            }
            if (CurrentLane != null)
            { CurrentLane.IncreaseAccumulatedFlow(); }
            else
            {
                //car is out of circuit += 1 @ simulation
            }
        }

        /// <summary>
        /// Draws the when normal.
        /// </summary>
        /// <param name="image">The image.</param>
        protected override void DrawWhenNormal(System.Drawing.Graphics image)
        {
            //draws a rectangle on the car's location
            image.FillEllipse(System.Drawing.Brushes.Black, this.X-2, this.Y-2, 4, 4);
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
