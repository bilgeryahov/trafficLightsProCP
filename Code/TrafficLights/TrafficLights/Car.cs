using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    public class Car : Renderable
    {
        public Road From
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Road CurrentRoad
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public override void Update(float seconds)
        {
            //moves the car based on the elapsed time
            throw new NotImplementedException();
        }

        public override void Draw(System.Drawing.Bitmap image)
        {
            //draws a rectangle on the car's location
            throw new NotImplementedException();
        }
    }
}
