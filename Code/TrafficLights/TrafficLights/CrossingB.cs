using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    /// <summary>
    /// implements Crossing, similar to type A but has 2 parralel crosswalks on each side
    /// </summary>
    /// <seealso cref="TrafficLights.Crossing" />
    [Serializable]
    public class CrossingB : Crossing
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CrossingB"/> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        public CrossingB(TrafficManager owner):base(owner)
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

        public override System.Drawing.Image Image
        {
            get { return TrafficLights.Properties.Resources.cross_2; }
        }
    }
}
