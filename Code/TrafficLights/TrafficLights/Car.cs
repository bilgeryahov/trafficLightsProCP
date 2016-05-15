using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
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
        /// Gets or sets from.
        /// </summary>
        /// <value>From.</value>
        public Lane From
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        /// <summary>
        /// Gets or sets the current road.
        /// </summary>
        /// <value>The current road.</value>
        public Lane CurrentRoad
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

       // public @enum State

        /// <summary>
        /// Gets or sets the current lane.
        /// </summary>
        /// <value>The current lane.</value>
        public Lane CurrentLane
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        /// <summary>
        /// Updates the specified seconds.
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        public override void Update(float seconds)
        {
            //moves the car based on the elapsed time
            throw new NotImplementedException();
        }

        /// <summary>
        /// Draws the when normal.
        /// </summary>
        /// <param name="image">The image.</param>
        protected override void DrawWhenNormal(System.Drawing.Bitmap image)
        {
            //draws a rectangle on the car's location
            throw new NotImplementedException();
        }

        /// <summary>
        /// Draws the when active.
        /// </summary>
        /// <param name="image">The image.</param>
        protected override void DrawWhenActive(System.Drawing.Bitmap image)
        {
            throw new NotImplementedException();
        }
    }
}
