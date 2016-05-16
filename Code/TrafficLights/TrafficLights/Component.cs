using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    /// <summary>
    /// abstract class containing coordinates for various objects within the simulation
    /// </summary>
    /// <seealso cref="TrafficLights.Renderable" />
    public abstract class Component:Renderable
    {
        public Component(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        /// <value>The x.</value>
        public int X
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        /// <value>The y.</value>
        public int Y
        {
            get;
            protected set;
        }
    }
}
