using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    [Serializable]
    /// <summary>
    /// a component that controls the movement of cars and pedestrians
    /// </summary>
    /// <seealso cref="TrafficLights.Component" />
    public class Trafficlight : Component
    {
        /// <summary>
        /// Enum State
        /// </summary>
        public enum State
        {
            /// <summary>
            /// The red
            /// </summary>
            Red, Yellow, YellowBlink, Green, None
        }

        /// <summary>
        /// Gets the default state order.
        /// </summary>
        /// <value>The default state order.</value>
        public static State[] DefaultStateOrder { get { return new State[] { State.Red, State.Yellow, State.Green }; } }

        /// <summary>
        /// Gets the time spent in green in seconds.
        /// </summary>
        public float GreenSeconds { get; set; }
        /// <summary>
        /// Gets the red to green seconds.
        /// </summary>
        /// <value>The red to green seconds.</value>
        public float RedToGreenSeconds { get; private set; }
        /// <summary>
        /// Gets the yellow seconds.
        /// </summary>
        /// <value>The yellow seconds.</value>
        public float YellowSeconds { get; private set; }

        /// <summary>
        /// Gets the state of the current.
        /// </summary>
        /// <value>The state of the current.</value>
        public State CurrentState { get; private set; }
        /// <summary>
        /// Gets the state of the previous.
        /// </summary>
        /// <value>The state of the previous.</value>
        public State PreviousState { get; private set; }

        /// <summary>
        /// Gets the state of the on override.
        /// </summary>
        /// <value>The state of the on override.</value>
        public State OnOverrideState{get; private set;}
        /// <summary>
        /// Gets the state of the on override cancel next.
        /// </summary>
        /// <value>The state of the on override cancel next.</value>
        public State OnOverrideCancelNextState
        {
            get
            {
                if (OnOverrideState == State.YellowBlink) return State.YellowBlink;
                if (OnOverrideState == State.Red) return State.Green;
                if (OnOverrideState == State.Yellow) return State.Green;
                if (OnOverrideState == State.Green) return State.Red;
                return State.None;
            }
        }
        /// <summary>
        /// Gets a value indicating whether this instance is overidden.
        /// </summary>
        /// <value><c>true</c> if this instance is overidden; otherwise, <c>false</c>.</value>
        public bool IsOveridden{get{return OnOverrideState != State.None;}}

        /// <summary>
        /// Gets the state of the next.
        /// </summary>
        /// <value>The state of the next.</value>
        public State NextState
        {
            get
            {
                if (CurrentState == State.Red) return State.Green;
                if (CurrentState == State.Green) return State.Yellow;
                if (CurrentState == State.Yellow) return State.Red;
                if (CurrentState == State.YellowBlink) return State.YellowBlink;

                return State.None;
            }
        }

        public Trafficlight(int x, int y) : base(x, y) { }

        /// <summary>
        /// Nexts this instance.
        /// </summary>
        public void Next()
        {
            //todo set amount of time for YEllow

            this.PreviousState = CurrentState;
            this.CurrentState = NextState;
        }

        /// <summary>
        /// Overrides the specified state.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="time">The time.</param>
        public void Override(State state, int time)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Overrides the specified state.
        /// </summary>
        /// <param name="state">The state.</param>
        public void Override(State state)
        {
            Override(state, int.MaxValue);
        }

        /// <summary>
        /// Cancels the override.
        /// </summary>
        public void CancelOverride()
        {
            this.CurrentState = OnOverrideCancelNextState;
            this.OnOverrideState = State.None;
        }

        /// <summary>
        /// Updates the specified seconds.
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        public override void Update(float seconds)
        {
            //time passed += seconds
            //if passed > lastupdate -> change light
            throw new NotImplementedException();
        }

        /// <summary>
        /// Draws the when normal.
        /// </summary>
        /// <param name="image">The image.</param>
        protected override void DrawWhenNormal(System.Drawing.Graphics image)
        {
            //draw the circles
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Draws the when active.
        /// </summary>
        /// <param name="image">The image.</param>
        protected override void DrawWhenActive(System.Drawing.Graphics image)
        {
            throw new NotImplementedException();
        }
    }
}
