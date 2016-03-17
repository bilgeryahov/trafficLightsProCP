using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    public class CrossingB : Crossing
    {
        public CrossingB(TrafficManager owner):base(owner)
        {
        }

        
        public override Crosswalk[] Crosswalks
        {
            get { throw new NotImplementedException(); }
        }

        public override void Update(float seconds)
        {
            throw new NotImplementedException();
        }

        public override void Draw(System.Drawing.Bitmap image)
        {
            throw new NotImplementedException();
        }
    }
}
