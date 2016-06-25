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
        public CrossingB(TrafficManager owner) : base(owner)
        {
        }


        /// <summary>
        /// Gets the crosswalks.
        /// </summary>
        /// <value>The crosswalks.</value>
        protected override Crosswalk[] GenerateCrosswalks
        {
            get
            {
                return new Crosswalk[]
                    {
                new Crosswalk
                        (Direction.Left, false, 0, 60, 30, 140,
                        new Lane(Direction.Right, Direction.Left, false, 0, 65),
                        new Lane(Direction.Left, Direction.Up, true, 0, 90),
                        new Lane(Direction.Left, Direction.Down | Direction.Right, true, 0, 115)
                        )
                        ,
                    new Crosswalk
                        (Direction.Down, true, 60, 200, 140, 140,
                        new Lane(Direction.Up, Direction.Down, false, 75, 135),
                        new Lane(Direction.Down, Direction.Up | Direction.Right, true, 110, 135)
                        )
                        ,
                    new Crosswalk
                        (Direction.Right, false, 200, 60, 140, 0,
                        new Lane(Direction.Left, Direction.Right, false, 135, 115),
                        new Lane(Direction.Right, Direction.Down, true, 135, 90),
                        new Lane(Direction.Right, Direction.Up | Direction.Left, true, 135, 65)
                        )
                        ,
                    new Crosswalk
                        (Direction.Up, true, 60, 0, 40, 0,
                        new Lane(Direction.Down, Direction.Up, false, 110, 0),
                        new Lane(Direction.Up, Direction.Down | Direction.Left, true, 75, 0)

                        )
        };
            }
        }

        public override System.Drawing.Image Image { get { return Properties.Resources.cross_2; } }
    }
}