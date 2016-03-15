﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    public class Trafficlight : Renderable
    {
        public enum State
        {
            Red, Yellow, YellowBlink, Green, None
        }

        public static State[] DefaultStateOrder { get { return new State[] { State.Red, State.Yellow, State.Green }; } }

        public State CurrentState { get; private set; }
        public State PreviousState { get; private set; }

        public State OnOverrideState{get; private set;}
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
        public bool IsOveridden{get{return OnOverrideState != State.None;}}

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

        public void Next()
        {
            //todo set amount of time for YEllow

            this.PreviousState = CurrentState;
            this.CurrentState = NextState;
        }

        public void Override(State state, int time)
        {
            throw new System.NotImplementedException();
        }

        public void Override(State state)
        {
            Override(state, int.MaxValue);
        }

        public void CancelOverride()
        {
            this.CurrentState = OnOverrideCancelNextState;
            this.OnOverrideState = State.None;
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        public override void Draw()
        {
            throw new NotImplementedException();
        }
    }
}