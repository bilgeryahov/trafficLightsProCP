using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    public abstract class CrossingContainer
    {
        private List<Crossing> crossings = new List<Crossing>();

        IEnumerable<Crossing> Crossings { get { return crossings.ToArray(); } }

        public void Add(Crossing crossing)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }
    }
}
