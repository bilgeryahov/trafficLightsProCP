using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    /// <summary>Represents a 'pedestrian' within the system</summary>
    public class Pedestrian : Renderable
    {
        public Crosswalk CurrentCrosswalk
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Direction Direction { get; private set; }

        //todo go in crosswalk, check direction, return from simulation the next crossing available's croswalk
        public Crosswalk NextCrosswalk
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        public override void Update(float seconds)
        {
            //moves the location of the pedestrians based on the elapsed time
            throw new NotImplementedException();
        }

        public override void Draw(System.Drawing.Bitmap image)
        {
            //draws a dot on the crosswalk
            throw new NotImplementedException();
        }
    }
}
