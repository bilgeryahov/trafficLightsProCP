using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    public class Crosswalk
    {
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
