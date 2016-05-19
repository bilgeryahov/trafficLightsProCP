using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    [Serializable]
    /// <summary>
    /// abstract class defining the components within the system that can move
    /// </summary>
    /// <seealso cref="TrafficLights.Component" />
    public abstract class Moveable : Component
    {
        /// <summary>
        /// Gets the path.
        /// </summary>
        /// <value>The path.</value>
        public List<System.Drawing.Point> Path { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Moveable"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public Moveable(int startX, int startY, params System.Drawing.Point[] path) : base(startX, startY)
        {
            this.Path = new List<System.Drawing.Point>(path);
        }
        /// <summary>
        /// Gets the index of the current point.
        /// </summary>
        /// <value>The index of the current point.</value>
        public int CurrentPointIndex { get; private set; }

        /// <summary>
        /// Gets the current point.
        /// </summary>
        /// <value>The current point.</value>
        public System.Drawing.Point CurrentPoint { get { return new System.Drawing.Point(X, Y); } }
        /// <summary>
        /// Updates the specified seconds.
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        public override void Update(float seconds)
        {
            foreach (System.Drawing.Point p in Path)
            {
                if (p == CurrentPoint)
                { CurrentPointIndex = Path.IndexOf(p); }
            }
         }

        /// <summary>
        /// Draws the when normal.
        /// </summary>
        /// <param name="image">The image.</param>
        protected override void DrawWhenNormal(System.Drawing.Graphics image)
        {
            throw new NotImplementedException();
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
