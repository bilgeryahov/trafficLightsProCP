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
    [Serializable]
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
            get
            {
                return new Crosswalk[]
                {
                    new Crosswalk
                        (Direction.Left, false, 0, 60,30, 140,
                        new Lane(Direction.Right, Direction.Left, false, 0, 75),
                        new Lane(Direction.Left, Direction.Up | Direction.Right, true, 0, 100 ),
                        new Lane(Direction.Left, Direction.Down, true, 0, 120)
                        )
                        ,
                    new Crosswalk
                        (Direction.Down, false,60, 200,140, 140,
                        new Lane(Direction.Up, Direction.Down, false, 75, 200),
                        new Lane(Direction.Down, Direction.Up | Direction.Left, true, 100, 200),
                        new Lane(Direction.Down, Direction.Right, true, 120, 200)
                        )
                        ,
                    new Crosswalk
                        (Direction.Right, false,200, 60,140, 0,
                        new Lane(Direction.Left, Direction.Right, false, 200, 120),
                        new Lane(Direction.Right, Direction.Left | Direction.Down, true, 200, 100 ),
                        new Lane(Direction.Right, Direction.Up, true, 200, 75)
                        )
                        ,
                    new Crosswalk
                        (Direction.Up, false,60, 0,40, 0,
                        new Lane(Direction.Down, Direction.Up, false, 120, 0),
                        new Lane(Direction.Up, Direction.Up | Direction.Left, true, 100, 0 ),
                        new Lane(Direction.Up, Direction.Right, true, 75, 0)
                        )
                };
            }
        }


        /// <summary>
        /// Updates the specified seconds.
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        public override void Update(float seconds)
        {
            foreach (Crosswalk walk in this.Crosswalks)
            {
                walk.Update(seconds);
            }
        }

        /// <summary>
        /// Draws the when normal.
        /// </summary>
        /// <param name="image">The image.</param>
        protected override void DrawWhenNormal(System.Drawing.Graphics image)
        {
            foreach (Crosswalk walk in this.Crosswalks)
            {
                walk.Draw(image);
            }
        }

        /// <summary>
        /// Draws the when active.
        /// </summary>
        /// <param name="image">The image.</param>
        protected override void DrawWhenActive(System.Drawing.Graphics image)
        {
            DrawWhenNormal(image);
        }


        public override System.Drawing.Image Image
        {
            get { return TrafficLights.Properties.Resources.cross_1; }
        }
    }
}
