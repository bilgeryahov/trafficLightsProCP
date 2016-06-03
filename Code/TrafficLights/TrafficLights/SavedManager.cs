using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    /// <summary>
    /// holds saved crossings
    /// </summary>
    /// <seealso cref="TrafficLights.CrossingManager" />
    [Serializable]
    public class SavedManager : CrossingManager
    {
        /// <summary>
        /// Duplicates the specified identifier.
        /// </summary>
        public Crossing Duplicate(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
