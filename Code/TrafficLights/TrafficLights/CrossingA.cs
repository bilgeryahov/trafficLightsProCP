using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    /// <summary>
    /// implements Crossing and is the basic type, with no crosswalks
    /// </summary>
    /// <seealso cref="TrafficLights.Crossing" />
    public class CrossingA : Crossing
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CrossingA"/> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        public CrossingA(TrafficManager owner):base(owner)
        {
        }
        /// <summary>
        /// Gets the crosswalks.
        /// </summary>
        /// <value>The crosswalks.</value>
        public override Crosswalk[] Crosswalks
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Updates the specified seconds.
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        public override void Update(float seconds)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Draws the when normal.
        /// </summary>
        /// <param name="image">The image.</param>
        protected override void DrawWhenNormal(System.Drawing.Bitmap image)
        {
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
