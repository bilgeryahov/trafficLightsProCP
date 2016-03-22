using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    public class Car : Component
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

<<<<<<< Updated upstream
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

        public @enum State
=======
        public Lane CurrentLane
>>>>>>> Stashed changes
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
