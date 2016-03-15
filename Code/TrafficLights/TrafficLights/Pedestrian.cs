using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
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

        public override void Update()
        {
            throw new NotImplementedException();
        }

        public override void Draw()
        {
            throw new NotImplementedException();
        }
    }
}
