using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    /// <summary>
    /// a component that the pedestrians walk on to cross a lane
    /// </summary>
    /// <seealso cref="TrafficLights.Component" />
    public class Crosswalk:Component
    {
        /// <summary>
        /// Gets the pedestrian flow side1.
        /// </summary>
        /// <value>The pedestrian flow side1.</value>
        public Pedestrian PedestrianFlowSide1 { get; private set; }
        /// <summary>
        /// Gets the pedestrian flow side2.
        /// </summary>
        /// <value>The pedestrian flow side2.</value>
        public Pedestrian PedestrianFlowSide2 { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this instance has pedestrians crossing.
        /// </summary>
        /// <value><c>true</c> if this instance has pedestrians crossing; otherwise, <c>false</c>.</value>
        public bool HasPedestriansCrossing { get { return PedestrianFlowSide1 != null || PedestrianFlowSide2 != null; } }

        /// <summary>
        /// Gets the lanes.
        /// </summary>
        /// <value>The lanes.</value>
        public List<Lane> Lanes { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Crosswalk"/> class.
        /// </summary>
        /// <param name="lanes">The lanes.</param>
        public Crosswalk(List<Lane> lanes)
        {
            this.Lanes = lanes;
        }

        /// <summary>
        /// Activates the sensor.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public void ActivateSensor(Direction direction)
        {
           // if (PedestrianFlowSide1 == null) PedestrianFlowSide1 = new Pedestrian(this, direction);
           // else
           //     if (PedestrianFlowSide1.Direction == direction) return;
           //     else if (direction == direction.Inverse()) PedestrianFlowSide2 = new Pedestrian(this, direction);
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Updates the specified seconds.
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        public override void Update(float seconds)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Draws the when normal.
        /// </summary>
        /// <param name="image">The image.</param>
        protected override void DrawWhenNormal(Bitmap image)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the light.
        /// </summary>
        /// <value>The light.</value>
        public Trafficlight Light { get; private set; }

        /// <summary>
        /// Gets the entrylanes.
        /// </summary>
        /// <value>The entrylanes.</value>
        public IEnumerable<Lane> Entrylanes
        {
            get
            {
                return this.Lanes.Where(x => x.IsFeeder);
            }
        }

        /// <summary>
        /// Gets the exit lanes.
        /// </summary>
        /// <value>The exit lanes.</value>
        public IEnumerable<Lane> ExitLanes
        {
            get
            {
                return this.Lanes.Where(x => !x.IsFeeder);
            }
        }

        /// <summary>
        /// Draws the when active.
        /// </summary>
        /// <param name="image">The image.</param>
        protected override void DrawWhenActive(Bitmap image)
        {
            throw new NotImplementedException();
        }
    }
}
