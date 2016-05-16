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
        protected override Crosswalk[] GenerateCrosswalks
        {
            get { throw new NotImplementedException(); }
        }

        public override System.Drawing.Image Image
        {
            get { return TrafficLights.Properties.Resources.cross_2; }
        }
    }
}
