using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficLights
{[Serializable]
    class CrossingBRotated : CrossingB
    {
        public CrossingBRotated(TrafficManager owner) : base(owner) { }
        public override System.Drawing.Image Image
        {
            get
            {
                return new System.ComponentModel.ComponentResourceManager(typeof(MainForm)).GetObject("PicBoxTypeC.BackgroundImage") as System.Drawing.Image;
            }
        }
    }
}
