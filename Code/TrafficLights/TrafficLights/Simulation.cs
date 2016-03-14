using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    public class Simulation : Renderable
    {
        public float Speed
        {
            get
            {
                throw new System.NotImplementedException();
            }
            private set
            {
            }
        }

        public Crossing[] Crossings = new Crossing[] { };
        

        public int TotalCars
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int CarsPassed
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public void Start()
        {
            throw new System.NotImplementedException();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        public void Finish()
        {
            throw new System.NotImplementedException();
        }

        public void Restart()
        {
            throw new System.NotImplementedException();
        }
    }
}
