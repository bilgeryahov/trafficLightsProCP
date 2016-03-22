using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    public class Crosswalk:Component
    {
        public Pedestrian PedestrianFlowSide1 { get; private set; }
        public Pedestrian PedestrianFlowSide2 { get; private set; }
        public bool HasPedestriansCrossing { get { return PedestrianFlowSide1 != null || PedestrianFlowSide2 != null; } }

        public List<Lane> Lanes { get; private set; }

        public Crosswalk(List<Lane> lanes)
        {
            this.Lanes = lanes;
        }

        public void ActivateSensor(Direction direction)
        {
           // if (PedestrianFlowSide1 == null) PedestrianFlowSide1 = new Pedestrian(this, direction);
           // else
           //     if (PedestrianFlowSide1.Direction == direction) return;
           //     else if (direction == direction.Inverse()) PedestrianFlowSide2 = new Pedestrian(this, direction);
            throw new System.NotImplementedException();
        }

        public override void Update(float seconds)
        {
            throw new NotImplementedException();
        }

        public override void Draw(Bitmap image)
        {
            throw new NotImplementedException();
        }

        public Trafficlight Light { get; private set; }

        public IEnumerable<Lane> Entrylanes
        {
            get
            {
                return this.Lanes.Where(x => x.IsFeeder);
            }
        }

        public IEnumerable<Lane> ExitLanes
        {
            get
            {
                return this.Lanes.Where(x => !x.IsFeeder);
            }
        }
    }
}
