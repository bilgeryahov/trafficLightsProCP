using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    public abstract class Component:Renderable
    {
        public int X
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int Y
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }
}
