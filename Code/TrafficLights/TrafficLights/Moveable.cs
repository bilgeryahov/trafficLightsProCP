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
        const float DefaultTimeFromStartToEnd = 1;
        private float currentPassed = 0;
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
            /*foreach (System.Drawing.Point p in Path)
            {
                if (p == CurrentPoint)
                { CurrentPointIndex = Path.IndexOf(p); }
            }*/
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
                float passed = currentPassed;
                while (passed >= avrg)
                    passed -= avrg;
                float percentPassed = passed / avrg;

                if (percentPassed.ToString().IndexOf('.') != -1)
                    percentPassed = float.Parse("0." + percentPassed.ToString().Split('.').Last());
                else
                    if(percentPassed != 0)
                        percentPassed = 1;

                this.X = leftPoint.X + (int)((rightPoint.X - leftPoint.X) * percentPassed);
                this.Y = leftPoint.Y + (int)((rightPoint.Y - leftPoint.Y) * percentPassed);
            }

         }

        /// <summary>
        /// Draws the when normal.
        /// </summary>
        /// <param name="image">The image.</param>
        protected override void DrawWhenNormal(System.Drawing.Graphics image)
        {
        }

        /// <summary>
        /// Draws the when active.
        /// </summary>
        /// <param name="image">The image.</param>
        protected override void DrawWhenActive(System.Drawing.Graphics image)
        {
        }
    }
}
