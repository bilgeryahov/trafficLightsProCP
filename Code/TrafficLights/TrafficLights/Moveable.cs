using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    public abstract class Moveable : Component
    {
        public List<System.Drawing.Point> Path { get; private set; }

        public Moveable(params System.Drawing.Point[] path)
        {
            this.Path = new List<System.Drawing.Point>(path);
        }
        public int CurrentPointIndex { get; private set; }

        public System.Drawing.Point CurrentPoint { get { throw new NotImplementedException(); } }
        public override void Update(float seconds)
        {
            throw new NotImplementedException();
        }

        protected override void DrawWhenNormal(System.Drawing.Bitmap image)
        {
            throw new NotImplementedException();
        }

        protected override void DrawWhenActive(System.Drawing.Bitmap image)
        {
            throw new NotImplementedException();
        }
    }
}
