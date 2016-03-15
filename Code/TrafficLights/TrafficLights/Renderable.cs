using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    public abstract class Renderable
    {
        public abstract void Update();

        public abstract void Draw();
    }
}
