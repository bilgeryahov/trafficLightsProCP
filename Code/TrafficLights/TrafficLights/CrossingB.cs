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
        public override Crosswalk[] Crosswalks => new[]
        {
            new Crosswalk
                (Direction.Left, true, 0, 60,30, 140,
                    new Lane(Direction.Right,Direction.Left, false, 0, 60),
                    new Lane(Direction.Right,Direction.Left, false, 0, 120)
                )
            ,
            new Crosswalk
                (Direction.Down, false,60, 200,140, 140,
                    new Lane(Direction.Up,   Direction.Down, false, 75, 200),
                    new Lane(Direction.Down, Direction.Left, true, 100, 200),
                    new Lane(Direction.Down, Direction.Right | Direction.Up , true, 120, 200)
                )
            ,
            new Crosswalk
                (Direction.Right, true,200, 60,140, 0,
                    new Lane(Direction.Left,Direction.Right, false, 200, 60),
                    new Lane(Direction.Left,Direction.Right, false, 200, 120)                  
                )
            ,
            new Crosswalk
                (Direction.Up, false,60, 0,40, 0,
                    new Lane(Direction.Down, Direction.Up, false, 120, 0),
                    new Lane(Direction.Up, Direction.Right, true, 100, 0 ),
                    new Lane(Direction.Up, Direction.Left | Direction.Down, true, 75, 0)
                )
        };

        public override void Update(float seconds)
        {
            foreach (var walk in this.Crosswalks)
            {
                walk.Update(seconds);
            }
        }

        protected override void DrawWhenNormal(System.Drawing.Graphics image)
        {
            foreach (var walk in this.Crosswalks)
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


        public override System.Drawing.Image Image => Properties.Resources.cross_1;
    }
}
