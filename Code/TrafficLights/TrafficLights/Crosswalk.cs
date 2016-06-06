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
    [Serializable]
    public class Crosswalk:Component
    {
        /// <summary>
        /// Gets the pedestrian flow side1.
        /// </summary>
        /// <value>The pedestrian flow side1.</value>
        public Pedestrian PedestrianFlowFrom { get; private set; }
        /// <summary>
        /// Gets the pedestrian flow side2.
        /// </summary>
        /// <value>The pedestrian flow side2.</value>
        public Pedestrian PedestrianFlowTo { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this instance has pedestrians crossing.
        /// </summary>
        /// <value><c>true</c> if this instance has pedestrians crossing; otherwise, <c>false</c>.</value>
        public bool HasPedestriansCrossing { get { return PedestrianFlowFrom != null || PedestrianFlowTo != null; } }

        public Direction From { get; private set; }
        public Direction To { get { return From.Inverse(); } }
        public bool CanHavePedestrians { get; private set; }

        public Crossing Owner;

        /// <summary>
        /// Gets the lanes.
        /// </summary>
        /// <value>The lanes.</value>
        public List<Lane> Lanes { get; private set; }
        static int pos = 1;
        /// <summary>
        /// Initializes a new instance of the <see cref="Crosswalk"/> class.
        /// </summary>
        /// <param name="lanes">The lanes.</param>
        public Crosswalk(List<Lane> lanes, Direction from, bool canHavePedestrians, int x, int y, int trafficLightX, int trafficLightY) : base(x,y)
        {
            this.Lanes = lanes;
            foreach (Lane lane in this.Lanes)
            {
                lane.SetOwner(this);
            }
            this.CanHavePedestrians = canHavePedestrians;
            this.From = from;
            this.Light = new Trafficlight(trafficLightX, trafficLightY,pos);
            pos++;
            if (pos >4) pos = 1;
        }
        

        public Crosswalk(Direction from, bool canHavePedestrians,int x, int y, int trafficLightX, int trafficLightY, params Lane[] lanes)
            : this(lanes.ToList(), from, canHavePedestrians, x, y, trafficLightX, trafficLightY)
        {
        }

        /// <summary>
        /// Activates the sensor.
        /// </summary>
        /// <param name="atWhichLocation">The direction of the traffic light, compared to the crosswalk in which it was activated.</param>
        public void ActivateSensor(Direction atWhichLocation)
        {
            if (!CanHavePedestrians) return;
            if (atWhichLocation == this.To)
                if (this.PedestrianFlowTo == null)
                    CreatePedestrianForTo(atWhichLocation);
            if (atWhichLocation == this.From)
                if (this.PedestrianFlowFrom == null)
                    CreatePedestrianForFrom(atWhichLocation);
        }

        private Pedestrian CreatePedestrian(Direction from, Direction targetDirection)
        {
            System.Drawing.Point start = GetPointFromDirection(from, targetDirection);
            System.Drawing.Point end = GetPointFromDirection(from, targetDirection.Inverse());

            System.Drawing.Point[] path = new Point[] { start, end };
            return new Pedestrian(start.X, start.Y, path);
        }

        private System.Drawing.Point GetPointFromDirection(Direction from, Direction startLocation)
        {
            int x = this.X;
            int y = this.Y;

            if (from == Direction.Left)
                x += 30;
            if (from == Direction.Right)
                x -= 30;
            if (from == Direction.Up)
                y += 30;
            if (from == Direction.Down)
                y += 30;

            if (from == Direction.Left || from == Direction.Right)
                if (startLocation == Direction.Up)
                    y -= 30;
                else if (startLocation == Direction.Down)
                    y += 100;
            if (from == Direction.Down || from == Direction.Up)
                if (startLocation == Direction.Left)
                    x -= 30;
                else if (startLocation == Direction.Right)
                    x += 100;

            return new Point(x, y);
        }

        private void CreatePedestrianForTo(Direction targetDirection)
        {
            this.PedestrianFlowTo = CreatePedestrian(this.To, targetDirection);
        }

        private void CreatePedestrianForFrom(Direction targetDirection)
        {
            this.PedestrianFlowFrom = CreatePedestrian(this.From, targetDirection);
        }
        private static int spins = 0;
        private static float AccumilatedTime = 0;
        public float TotalActiveSeconds { get;private set; }
        /// <summary>
        /// Updates the specified seconds.
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        public override void Update(float seconds)
        {
            //if (!this.Owner.IntervalsSet)
            //{
            //    this.Owner.SetStartIntervals();
            //}
            if (PedestrianFlowFrom != null)
                PedestrianFlowFrom.Update(seconds);
            if (PedestrianFlowTo != null)
                PedestrianFlowTo.Update(seconds);
            
            foreach (Lane lane in this.Lanes)
            {
                lane.Update(seconds);
            }
            this.Light.Update(seconds);
            spins++;
        }

        /// <summary>
        /// Draws the when normal.
        /// </summary>
        /// <param name="image">The image.</param>
        protected override void DrawWhenNormal(Graphics image)
        {
            if (PedestrianFlowFrom != null)
                PedestrianFlowFrom.Draw(image);
            if (PedestrianFlowTo != null)
                PedestrianFlowTo.Draw(image);
            this.Light.Draw(image);

            foreach (Lane lane in this.Lanes)
            {
                lane.Draw(image);
            }
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
        protected override void DrawWhenActive(Graphics image)
        {
            DrawWhenActive(image);
        }
        public void Reset()
        {
            foreach (Lane lane in this.Lanes)
            {
                lane.Reset();
            }

            this.Light.Reset();
        }
    }
}
