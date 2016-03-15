using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    public class Crosswalk
    {
        public Pedestrian PedestrianFlowSide1 { get; private set; }
        public Pedestrian PedestrianFlowSide2 { get; private set; }
        public bool HasPedestrians { get { return PedestrianFlowSide1 != null || PedestrianFlowSide2 != null; } }

        public void ActivateSensor(Direction direction)
        {
           // if (PedestrianFlowSide1 == null) PedestrianFlowSide1 = new Pedestrian(this, direction);
           // else
           //     if (PedestrianFlowSide1.Direction == direction) return;
           //     else if (direction == direction.Inverse()) PedestrianFlowSide2 = new Pedestrian(this, direction);
            throw new System.NotImplementedException();
        }

        public Trafficlight Light
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public List<Road> Entrylanes
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public List<Road> ExitLanes
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public List<Road> Lanes
        {
            get
            {
                List<Road> result = new List<Road>();
                result.AddRange(Entrylanes);
                result.AddRange(ExitLanes);
                return result;
            }
            set
            {
            }
        }
    }
}
