using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    /// <summary>abstract class providing with the core for a dynamic object to be rendered</summary>
    public abstract class Renderable
    {
        public bool isActive { get; private set; }
        public abstract void Update(float seconds);
        
        protected abstract void DrawWhenNormal(System.Drawing.Bitmap image);
        protected abstract void DrawWhenActive(System.Drawing.Bitmap image);
        public void Draw(System.Drawing.Bitmap image)
        {
            if (isActive) { DrawWhenActive(image); }
            else { DrawWhenNormal(image); }
        }
        public void SetActive(bool state)
        {
            this.isActive = state;
        }
    }
}
