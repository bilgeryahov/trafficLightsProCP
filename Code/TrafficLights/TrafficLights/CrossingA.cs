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
        protected override Crosswalk[] GenerateCrosswalks
        {
            get
            {
                return new Crosswalk[]
                {
                    new Crosswalk
                        (Direction.Left, false, 0, 60,30, 140,
                        new Lane(Direction.Right, Direction.Left, false, 0, 62),
                        new Lane(Direction.Left, Direction.Up | Direction.Right, true, 0, 88 ),
                        new Lane(Direction.Left, Direction.Down, true, 0, 110)
                        )
                        ,
                    new Crosswalk
                        (Direction.Down, false,60, 200,140, 140,
                        new Lane(Direction.Up, Direction.Down, false, 65, 135),
                        new Lane(Direction.Down, Direction.Up | Direction.Left, true, 90, 135),
                        new Lane(Direction.Down, Direction.Right, true, 112, 135)
                        )
                        ,
                    new Crosswalk
                        (Direction.Right, false,200, 60,140, 0,
                        new Lane(Direction.Left, Direction.Right, false, 135, 110),
                        new Lane(Direction.Right, Direction.Left | Direction.Down, true, 135, 88 ),
                        new Lane(Direction.Right, Direction.Up, true, 135, 62)
                        )
                        ,
                    new Crosswalk
                        (Direction.Up, false,60, 0,40, 0,
                        new Lane(Direction.Down, Direction.Up, false, 112, 0),
                        new Lane(Direction.Up, Direction.Down | Direction.Right, true, 90, 0 ),
                        new Lane(Direction.Up, Direction.Left, true, 65, 0)
                        )
                };
            }
        }

        public override System.Drawing.Image Image
        {
            get { return TrafficLights.Properties.Resources.cross_1; }
        }
    }
}
