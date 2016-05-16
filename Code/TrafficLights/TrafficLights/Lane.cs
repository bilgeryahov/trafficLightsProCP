using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    /// <summary>
    /// a component that cars drive on
    /// </summary>
    /// <seealso cref="TrafficLights.Component" />
    [Serializable]
    public class Lane : Component
    {
        /// <summary>
        /// Gets a value indicating whether this instance is feeder.
        /// </summary>
        /// <value><c>true</c> if this instance is feeder; otherwise, <c>false</c>.</value>
        public bool IsFeeder { get; private set; }
        /// <summary>
        /// Gets from.
        /// </summary>
        /// <value>From.</value>
        public Direction From { get; private set; }

        /// <summary>
        /// Gets to.
        /// </summary>
        /// <value>To.</value>
        public Direction To { get; private set; }

        static Random r = new Random();

        public Lane Next
        {
            get
            {
                IEnumerable<Lane> result = Owner.Lanes.Where(x => x.IsFeeder != this.IsFeeder && this.To.HasFlag(x.From));
                if (result.Count() == 0) return null;
                if (result.Count() == 1) return result.First();
                else
                {
                    int current = r.Next(result.Count());
                    if (current == 0) current = 1;

                    foreach (Lane lane in result)
                    {
                        if (--current == 0)
                            return lane;
                    }
                    throw new InvalidProgramException("Unable to obtain next lane");
                }
            }
        }

        private List<Car> currentCarsOn = new List<Car>();

        /// <summary>
        /// Gets the current flow.
        /// </summary>
        /// <value>The current flow.</value>
        public int CurrentFlow
        {
<<<<<<< Updated upstream
            get { return currentCarsOn.Count; }
=======
            //returns List<Car>.Count 
            get { throw new NotImplementedException(); }
>>>>>>> Stashed changes
        }

        /// <summary>
        /// Gets the flow.
        /// </summary>
        /// <value>The flow.</value>
        public int Flow
        {
            get;
            private set;
        }

        private int flowReleased = 0;
        private int flowAccumulated = 0;
        public Crosswalk Owner { get; private set; }

        public Lane(Direction from, Direction to, bool isFeeder, int x, int y) : base(x,y)
        {
            this.From = from;
            this.To = to;
            this.IsFeeder = IsFeeder;
        }

        public void IncreaseAccumulatedFlow()
        {
            flowAccumulated += 1;
        }

        public void SetOwner(Crosswalk owner)
        {
            this.Owner = owner;
        }

        /// <summary>
        /// Updates the specified seconds.
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        public override void Update(float seconds)
        {
            if (flowReleased < Flow)
            {
                this.currentCarsOn.Add(new Car(this.X, this.Y, this));
            }
            else if (flowAccumulated > 0)
            {
                this.currentCarsOn.Add(new Car(this.X, this.Y, this));
                flowAccumulated -= 1;
            }
            foreach (Car car in this.currentCarsOn)
            {
                car.Update(seconds);
            }
        }

        /// <summary>
        /// Draws the when normal.
        /// </summary>
        /// <param name="image">The image.</param>
        protected override void DrawWhenNormal(System.Drawing.Graphics image)
        {
            foreach (Car car in this.currentCarsOn)
            {
                car.Draw(image);
            }
        }

        /// <summary>
        /// Updates the flow.
        /// </summary>
        /// <param name="value">The value.</param>
        public void UpdateFlow(int value)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Draws the when active.
        /// </summary>
        /// <param name="image">The image.</param>
        protected override void DrawWhenActive(System.Drawing.Graphics image)
        {
            DrawWhenNormal(image);
        }
    }
}
