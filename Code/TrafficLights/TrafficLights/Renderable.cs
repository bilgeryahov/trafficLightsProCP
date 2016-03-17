using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    /// <summary>abstract class providing with the core for a dynamic object to be rendered</summary>
    public abstract class Renderable
    {
        public abstract void Update(float seconds);

        public abstract void Draw(System.Drawing.Bitmap image);
    }
}
