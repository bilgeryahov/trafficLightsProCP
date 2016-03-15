using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    public class Road : Renderable
    {
        public Direction From
        {
            get
            {
                throw new System.NotImplementedException();
            }
            private set
            {
            }
        }

        public Direction To
        {
            get
            {
                throw new System.NotImplementedException();
            }
            private set
            {
            }
        }

        public int CarsCurrentlyOn
        {
            get
            {
                throw new System.NotImplementedException();
            }
            private set
            {
            }
        }

        public int CarsInitialyOn
        {
            get
            {
                throw new System.NotImplementedException();
            }
             private set
            {
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
