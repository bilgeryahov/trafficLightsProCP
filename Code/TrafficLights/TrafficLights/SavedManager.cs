using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    /// <summary>Holds saved crossings</summary>
    public class SavedManager : CrossingContainer
    {
        public Crossing Duplicate(int id)
        {
            return this[id].CreateCopy();
        }
    }
}
