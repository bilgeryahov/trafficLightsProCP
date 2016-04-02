using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    /// <summary>
    /// a component that cars drive on
    /// </summary>
    /// <seealso cref="TrafficLights.Component" />
    [Serializable]
    public class Lane : Component
    {
        /// <summary>
        /// Gets a value indicating whether this instance is feeder.
        /// </summary>
        /// <value><c>true</c> if this instance is feeder; otherwise, <c>false</c>.</value>
        public bool IsFeeder { get; private set; }
        /// <summary>
        /// Gets from.
        /// </summary>
        /// <value>From.</value>
        public Direction From { get; private set; }

        /// <summary>
        /// Gets to.
        /// </summary>
        /// <value>To.</value>
        public Direction To { get; private set; }

        /// <summary>
        /// Gets the current flow.
        /// </summary>
        /// <value>The current flow.</value>
        public int CurrentFlow
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the flow.
        /// </summary>
        /// <value>The flow.</value>
        public int Flow
        {
            get;
            private set;
        }

        /// <summary>
        /// Updates the specified seconds.
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        public override void Update(float seconds)
        {
            //update cars
            throw new NotImplementedException();
        }

        /// <summary>
        /// Draws the when normal.
        /// </summary>
        /// <param name="image">The image.</param>
        protected override void DrawWhenNormal(System.Drawing.Bitmap image)
        {
            //draw road
            //draw cars
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the flow.
        /// </summary>
        /// <param name="value">The value.</param>
        public void UpdateFlow(int value)
        {
            throw new System.NotImplementedException();
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
