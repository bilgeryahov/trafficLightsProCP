using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    public class Car : Moveable
    {
        public Direction Direction { get; set; }
        public bool InMiddleOfCrosswalk { get; set; }
        public Lane From
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Lane CurrentRoad
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

       // public @enum State

        public Lane CurrentLane
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

        protected override void DrawWhenNormal(System.Drawing.Bitmap image)
        {
            //draws a rectangle on the car's location
            throw new NotImplementedException();
        }

        protected override void DrawWhenActive(System.Drawing.Bitmap image)
        {
            throw new NotImplementedException();
        }
    }
}
