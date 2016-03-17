﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    public class Crosswalk
    {
        public Pedestrian PedestrianFlowSide1 { get; private set; }
        public Pedestrian PedestrianFlowSide2 { get; private set; }
        public bool HasPedestriansCrossing { get { return PedestrianFlowSide1 != null || PedestrianFlowSide2 != null; } }

        public List<Road> Lanes { get; private set; }

        public Crosswalk(List<Road> lanes)
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

        public Trafficlight Light { get; private set; }

        public IEnumerable<Road> Entrylanes
        {
            get
            {
                return this.Lanes.Where(x => x.IsFeeder);
            }
        }

        public IEnumerable<Road> ExitLanes
        {
            get
            {
                return this.Lanes.Where(x => !x.IsFeeder);
            }
        }
    }
}
