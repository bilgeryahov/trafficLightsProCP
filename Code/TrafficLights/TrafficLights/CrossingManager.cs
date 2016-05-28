using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{[Serializable]
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
            this.crossings.Add(crossing);
        }

        /// <summary>
        /// Removes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Remove(int id)
        {
            if (this.crossings.Count > id)
                this.crossings.RemoveAt(id);
        }

        /// <summary>
        /// Removes the specified identifier.
        /// </summary>
        /// <param name="id">The crossing.</param>
        public void Remove(Crossing crossing)
        {
            for (int i = 0; i < this.crossings.Count; i++)
            {
                if(this.crossings[i] == crossing)
                {
                    Remove(i);
                    break;
                }
            }
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            this.crossings.Clear();
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
