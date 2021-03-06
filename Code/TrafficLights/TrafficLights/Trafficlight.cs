﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TrafficLights
{
    [Serializable]
    /// <summary>
    /// a component that controls the movement of cars and pedestrians
    /// </summary>
    /// <seealso cref="TrafficLights.Component" />
    public class Trafficlight : Component
    {
        private float timePassed = 0;
        
        public float TimeoutSeconds = 0;
        [NonSerialized]
        private System.Drawing.Brush brushGreen = System.Drawing.Brushes.White;
        [NonSerialized]
        private System.Drawing.Brush brushYellow = System.Drawing.Brushes.White;
        [NonSerialized]
        private System.Drawing.Brush brushRed = System.Drawing.Brushes.White;
        public int Position { get;private set; }
        private int intervalX { get; set; }
        private int intervalY { get; set; }
        public Crossing Owner { get; private set; }
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
        public float RedSeconds { get; set; }
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

        public bool Enabled = true;
        
        public Trafficlight(int x, int y, int position) : base(x, y) 
        {
            this.CurrentState = State.None;
            this.GreenSeconds = 2;
            this.YellowSeconds = 1;
            this.Position = position;
            brushGreen = System.Drawing.Brushes.White;
            brushYellow = System.Drawing.Brushes.White;
            brushRed = System.Drawing.Brushes.White;
    }

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
            CurrentState = OnOverrideState;
        }
        public void SetOwner(Crossing owner)
        {
            this.Owner = owner;
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
        private void ChangeLight(State state)
        {
            if (state == State.Green)
            {
                brushGreen = System.Drawing.Brushes.Green;
                brushYellow = System.Drawing.Brushes.White;
                brushRed = System.Drawing.Brushes.White;
            }
            else if (state == State.Yellow)
            {
                brushGreen = System.Drawing.Brushes.White;
                brushYellow = System.Drawing.Brushes.Yellow;
                brushRed = System.Drawing.Brushes.White;
            }
            else if (state == State.Red)
            {
                brushGreen = System.Drawing.Brushes.White;
                brushYellow = System.Drawing.Brushes.White;
                brushRed = System.Drawing.Brushes.Red;
            }
            CurrentState = state;
        }
        
        /// <summary>
        /// Updates the specified seconds.
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        public override void Update(float seconds)
        {
            TimeoutSeconds -= seconds;
            if (CurrentState == State.None)
            {
                ChangeLight(State.Red);
            }
            if(TimeoutSeconds<=0)
            {
                if (CurrentState == State.Red)
                {
                    ChangeLight(State.Green);
                    TimeoutSeconds = GreenSeconds;
                }
                else if (CurrentState == State.Green)
                {
                    ChangeLight(State.Yellow);
                    TimeoutSeconds = YellowSeconds;
                }
                else if (CurrentState == State.Yellow)
                {
                    ChangeLight(State.Red);
                    TimeoutSeconds = RedSeconds;
                }
            }
            

        }
        
        /// <summary>
        /// Draws the when normal.
        /// </summary>
        /// <param name="image">The image.</param>
        protected override void DrawWhenNormal(System.Drawing.Graphics image)
        {
            if(brushGreen == null && brushRed == null && brushYellow == null)
            {
                brushGreen = System.Drawing.Brushes.White;
                brushRed = System.Drawing.Brushes.White;
                brushYellow = System.Drawing.Brushes.White;
            }
            if (this.Position == 1 || this.Position == 4)
            {
                this.intervalX = this.X - 22; 
                this.intervalY = this.Y + 10;
            }
            else
            {
                this.intervalX = this.X + 22;
                this.intervalY = this.Y + 10;
            }
            image.DrawString(this.GreenSeconds.ToString(), new System.Drawing.Font(System.Drawing.FontFamily.GenericSansSerif, 14), System.Drawing.Brushes.Black, this.intervalX, this.intervalY);
            image.FillRectangle(System.Drawing.Brushes.Black, this.X + 2, this.Y + 2, 16, 46);
            image.FillEllipse(brushRed, this.X + 3, this.Y + 4, 12, 12);
            image.FillEllipse(brushYellow, this.X + 3, this.Y + 19, 12, 12);
            image.FillEllipse(brushGreen, this.X + 3, this.Y + 34, 12, 12);
        }

        /// <summary>
        /// Draws the when active.
        /// </summary>
        /// <param name="image">The image.</param>
        protected override void DrawWhenActive(System.Drawing.Graphics image)
        {
            image.FillRectangle(System.Drawing.Brushes.Green, this.X-1, this.Y-1, 22, 52);
            DrawWhenNormal(image);
        }
        public void Reset()
        {
            timePassed = 0;
            CurrentState = State.None;
            brushGreen = System.Drawing.Brushes.White;
            brushYellow = System.Drawing.Brushes.White;
            brushRed = System.Drawing.Brushes.White;
        }
    }
}
