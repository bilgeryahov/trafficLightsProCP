using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    /// <summary>
    /// a component that moves on the sidewalks and crosswalks of the roads
    /// </summary>
    /// <seealso cref="TrafficLights.Moveable" />
    public class Pedestrian : Moveable
    {

        /// <summary>
        /// Gets or sets the current crosswalk.
        /// </summary>
        /// <value>The current crosswalk.</value>
        public Crosswalk CurrentCrosswalk
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
        /// Gets the direction.
        /// </summary>
        /// <value>The direction.</value>
        public Direction Direction { get; private set; }

        //todo go in crosswalk, check direction, return from simulation the next crossing available's croswalk
        /// <summary>
        /// Gets the next crosswalk.
        /// </summary>
        /// <value>The next crosswalk.</value>
        public Crosswalk NextCrosswalk
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        public Pedestrian(int startX, int startY, params System.Drawing.Point[] path) : base(startX, startY, path) { }

        /// <summary>
        /// Updates the specified seconds.
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        public override void Update(float seconds)
        {
            //moves the location of the pedestrians based on the elapsed time
            throw new NotImplementedException();
        }

        /// <summary>
        /// Draws the when normal.
        /// </summary>
        /// <param name="image">The image.</param>
        protected override void DrawWhenNormal(System.Drawing.Bitmap image)
        {
            //draws a dot on the crosswalk
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
