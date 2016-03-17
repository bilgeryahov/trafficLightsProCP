using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    /// <summary>A lane on a crosswalk</summary>
    public class Road : Component
    {
        public bool IsFeeder { get; private set; }
        public Direction From { get; private set; }

        public Direction To { get; private set; }

        public int CarsCurrentlyOn
        {
            get;
            private set;
        }

        public int CarsInitialyOn
        {
            get;
            private set;
        }

        public override void Update(float seconds)
        {
            //update cars
            throw new NotImplementedException();
        }

        public override void Draw(System.Drawing.Bitmap image)
        {
            //draw road
            //draw cars
            throw new NotImplementedException();
        }
    }
}
