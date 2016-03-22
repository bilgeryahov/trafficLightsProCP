using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    /// <summary>Holds all crossings that have been deleted and could be undone</summary>
    public class RecycleManager : CrossingManager
    {
        public Crossing TakeOut(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
