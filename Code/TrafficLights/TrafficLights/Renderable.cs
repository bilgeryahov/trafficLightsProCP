using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    /// <summary>
    /// abstract class providing with the core for a dynamic object to be rendered
    /// </summary>
    public abstract class Renderable
    {
        /// <summary>
        /// Gets a value indicating whether this instance is active.
        /// </summary>
        /// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
        public bool isActive { get; private set; }
        /// <summary>
        /// Updates the specified seconds.
        /// </summary>
        public abstract void Update(float seconds);

        /// <summary>
        /// Draws the when normal.
        /// </summary>
        protected abstract void DrawWhenNormal(System.Drawing.Graphics image);
        /// <summary>
        /// Draws the when active.
        /// </summary>
        protected abstract void DrawWhenActive(System.Drawing.Graphics image);
        /// <summary>
        /// Draws the specified image.
        /// </summary>
        public void Draw(System.Drawing.Graphics image)
        {
            if (isActive) { DrawWhenActive(image); }
            else { DrawWhenNormal(image); }
        }
        /// <summary>
        /// Sets the active.
        /// </summary>
        /// <param name="state">if set to <c>true</c> [state].</param>
        public void SetActive(bool state)
        {
            this.isActive = state;
        }
    }
}
