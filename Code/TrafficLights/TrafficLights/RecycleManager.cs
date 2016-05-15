using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    /// <summary>
    /// holds all crossings that have been deleted and could be undone
    /// </summary>
    /// <seealso cref="TrafficLights.CrossingManager" />
    public class RecycleManager : CrossingManager
    {
        /// <summary>
        /// Takes the out.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public Crossing TakeOut(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
