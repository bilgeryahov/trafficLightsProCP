﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    public abstract class Crossing : Renderable
    {
        public Simulation Parent { get; private set; }

        //todo: iterate parent and get corresponding UP DOWN LEFT RIGHT
        public Crossing LeftCrossing{get { throw new NotImplementedException();}}
        public Crossing RightCrossing{get { throw new NotImplementedException();}}
        public Crossing TopCrossing{get { throw new NotImplementedException();}}
        public Crossing BotCrossing{get { throw new NotImplementedException();}}

        public Crosswalk[] Crosswalks
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public IEnumerable<Road> Roads
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public System.Collections.Generic.IEnumerable<TrafficLights.Road> TopFeeders
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public System.Collections.Generic.IEnumerable<TrafficLights.Road> BotFeeders
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public System.Collections.Generic.IEnumerable<TrafficLights.Road> LeftFeeders
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public System.Collections.Generic.IEnumerable<TrafficLights.Road> RightFeeders
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public void ActivatePedestrianSensor(Crosswalk crosswalk)
        {
            throw new System.NotImplementedException();
        }
    }
}
