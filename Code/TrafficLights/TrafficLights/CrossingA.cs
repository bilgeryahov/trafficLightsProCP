﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    public class CrossingA : Crossing
    {
        public CrossingA(TrafficManager owner):base(owner)
        {
        }
        public override Crosswalk[] Crosswalks
        {
            get { throw new NotImplementedException(); }
        }

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
