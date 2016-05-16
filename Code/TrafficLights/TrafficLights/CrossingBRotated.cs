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
        public override Crosswalk[] Crosswalks => new[]
           {

            new Crosswalk
                        (Direction.Left, false, 0, 60,30, 140,
                        new Lane(Direction.Right, Direction.Left, false, 0, 75),
                        new Lane(Direction.Left, Direction.Up , true, 0, 100 ),
                        new Lane(Direction.Left, Direction.Down | Direction.Right, true, 0, 120)
                        )
                        ,
                    new Crosswalk
                        (Direction.Down, true,60, 200,140, 140,
                        new Lane(Direction.Up, Direction.Down, false, 75, 200),
                        new Lane(Direction.Up, Direction.Down, false, 120, 200)
                        )
                        ,
                    new Crosswalk
                        (Direction.Right, false,200, 60,140, 0,
                        new Lane(Direction.Left, Direction.Right, false, 200, 120),
                        new Lane(Direction.Right, Direction.Down, true, 200, 100 ),
                        new Lane(Direction.Right, Direction.Up | Direction.Left, true, 200, 75)
                        )
                        ,
                    new Crosswalk
                        (Direction.Up, true,60, 0,40, 0,
                        new Lane(Direction.Down, Direction.Up, false, 120, 0),
                        new Lane(Direction.Down, Direction.Up, false, 75, 0 )
                        
                        )

             };

        public override System.Drawing.Image Image => new System.ComponentModel.ComponentResourceManager(typeof(MainForm)).GetObject("PicBoxTypeC.BackgroundImage") as System.Drawing.Image;
    }
}
