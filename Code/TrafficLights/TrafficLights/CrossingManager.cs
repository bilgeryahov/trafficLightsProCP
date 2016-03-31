using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    /// <summary>
    /// is responsible for the crossings
    /// </summary>
    public abstract class CrossingManager
    {
        /// <summary>
        /// The crossings
        /// </summary>
        private List<Crossing> crossings = new List<Crossing>();

        /// <summary>
        /// Gets the crossings.
        /// </summary>
        /// <value>The crossings.</value>
        public Crossing[] Crossings { get { return crossings.ToArray(); } }

        /// <summary>
        /// Adds the specified crossing.
        /// </summary>
        /// <param name="crossing">The crossing.</param>
        public void Add(Crossing crossing)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Removes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets the <see cref="Crossing"/> with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Crossing.</returns>
        public Crossing this[int id]
        {
            get
            {
                if (crossings.Count < id && id > -1)
                    return crossings[id];
                return null;
            }
        }
    }
}
