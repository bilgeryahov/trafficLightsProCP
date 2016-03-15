using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    public class Simulation : Renderable
    {
        public const float MIN_SPEED = 0.1f;
        public const float MAX_SPEED = 10;
        public const float DEFAULT_ADJUST_SPEED = 0.1f;

        private float speed;
        public float Speed
        {
            get { return speed; }
            private set
            {
                if (value < 0.1) value = 0.1f;
                else if (value > 10) value = 10;
                this.speed = value;
            }
        }

        public Crossing[] Crossings = new Crossing[] { };

        public int TotalCars { get { throw new System.NotImplementedException(); } }

        public int CarsPassed { get { throw new System.NotImplementedException(); } }

        /// <summary> Increases speed by DEFAULT_ADJUST_SPEED </summary>
        public void IncreaseSpeed()
        {
            IncreaseSpeed(DEFAULT_ADJUST_SPEED);
        }

        /// <summary> Decreases speed by DEFAULT_ADJUST_SPEED </summary>
        public void DecreaseSpeed()
        {
            DecreaseSpeed(DEFAULT_ADJUST_SPEED);
        }

        public void IncreaseSpeed(float amount)
        {
            if (amount < 0) return;

            AdjustSpeed(DEFAULT_ADJUST_SPEED);
        }

        public void DecreaseSpeed(float amount)
        {
            if (amount < 0) amount *= -1;

            AdjustSpeed(amount);
        }

        public void AdjustSpeed(float amount)
        {
            this.Speed += amount;
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
