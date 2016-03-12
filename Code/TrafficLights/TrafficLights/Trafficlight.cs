using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    public class Trafficlight : Renderable
    {
        public enum State
        {
            Red, Yellow, YellowBlink, Green, NoPower,
        }

        public State CurrentState
        {
            get
            {
                throw new System.NotImplementedException();
            }
            private set
            {
            }
        }

        public State NextState
        {
            get
            {
                throw new System.NotImplementedException();
            }
            private set
            {
            }
        }

        public State PreviousState
        {
            get
            {
                throw new System.NotImplementedException();
            }
            private set
            {
            }
        }

        public void Override(State state, int time)
        {
            throw new System.NotImplementedException();
        }

        public void Override(State state)
        {
            throw new System.NotImplementedException();
        }
    }
}
