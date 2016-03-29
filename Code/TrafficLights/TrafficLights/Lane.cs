using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    /// <summary>A lane on a crosswalk</summary>
    public class Lane : Component
    {
        public bool IsFeeder { get; private set; }
        public Direction From { get; private set; }

        public Direction To { get; private set; }

        public int CurrentFlow
        {
            get { throw new NotImplementedException(); }
        }

        public int Flow
        {
            get;
            private set;
        }

        public override void Update(float seconds)
        {
            //update cars
            throw new NotImplementedException();
        }

        protected override void DrawWhenNormal(System.Drawing.Bitmap image)
        {
            //draw road
            //draw cars
            throw new NotImplementedException();
        }

        public void UpdateFlow(int value)
        {
            throw new System.NotImplementedException();
        }

        protected override void DrawWhenActive(System.Drawing.Bitmap image)
        {
            throw new NotImplementedException();
        }
    }
}
