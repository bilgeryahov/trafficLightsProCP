using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficLights
{
    [Serializable]
    class CrossingBRotated : CrossingB
    {
        public CrossingBRotated(TrafficManager owner) : base(owner) { }
        protected override Crosswalk[] GenerateCrosswalks
        {
            get
            {
                return new Crosswalk[]
                    {
                new Crosswalk
                    (Direction.Left, true, 0, 60,30, 140,
                        new Lane(Direction.Right,Direction.Left, false, 0, 75),
                        new Lane(Direction.Left,Direction.Right | Direction.Down, true, 0, 110)
                    )
                ,
                new Crosswalk
                    (Direction.Down, false,60, 200,140, 140,
                        new Lane(Direction.Up,   Direction.Down, false, 65, 135),
                        new Lane(Direction.Down, Direction.Left, true, 90, 135),
                        new Lane(Direction.Down, Direction.Right | Direction.Up , true, 112, 135)
                    )
                ,
                new Crosswalk
                    (Direction.Right, true,200, 60,140, 0,
                        new Lane(Direction.Right,Direction.Left | Direction.Up, true, 135, 75),
                        new Lane(Direction.Left,Direction.Right, false, 135, 110)
                    )
                ,
                new Crosswalk
                    (Direction.Up, false,60, 0,40, 0,
                        new Lane(Direction.Down, Direction.Up, false, 112, 0),
                        new Lane(Direction.Up, Direction.Right, true, 90, 0 ),
                        new Lane(Direction.Up, Direction.Left | Direction.Down, true, 65, 0)
                    )
             };
                
            }
        }

        public override System.Drawing.Image Image { get { return new System.ComponentModel.ComponentResourceManager(typeof(MainForm)).GetObject("PicBoxTypeC.BackgroundImage") as System.Drawing.Image; } }
    }
}
