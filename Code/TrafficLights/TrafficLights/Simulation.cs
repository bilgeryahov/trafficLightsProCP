using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficLights
{
    /// <summary>Handles a simulation based on the grid created by the System</summary>
    public class Simulation : Renderable
    {
        public event Action<SimulationResult> OnCompleted = (x) => { };

        public const float MIN_SPEED = 0.1f;
        public const float MAX_SPEED = 10;
        public const float DEFAULT_ADJUST_SPEED = 0.1f;
        public const float DEFAULT_GRID_ROWS = 3;
        public const float DEFAULT_GRID_COLUMNS = 3;

        float currentFrame;
        bool isPaused;

        public Grid Grid { get; private set; }

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

        public int TotalCars { get { return Grid.AllCrossings.Select(x => x.Feeders.Select(y => y.CarsInitialyOn).Sum(y => y)).Sum(x => x); } }

        public int CarsPassed { get { throw new System.NotImplementedException(); } }

        public int CarsLeft { get { return TotalCars - CarsPassed; } }
        public bool HasPedestriansCrossing { get { return Grid.AllCrossings.Any(x => x.HasPedestriansCrossing); } }

        public Simulation(Grid grid)
        {
            this.Grid = grid;
        }

        // <summary> Increases speed by DEFAULT_ADJUST_SPEED </summary>
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
            if (isPaused)
            {
                Resume();
                return;
            }
        }

        public void Pause()
        {
            if (isPaused) return;
            throw new System.NotImplementedException();
            isPaused = true;
        }

        public void Resume()
        {
            throw new System.NotImplementedException();
            if (!isPaused) return;
            isPaused = false;
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
            Reset();
        }

        public void Finish()
        {
            throw new System.NotImplementedException();
            if (CarsLeft == 0 && !HasPedestriansCrossing)
            {
                OnCompleted(new SimulationResult(this));
                Stop();
            }

        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void Restart()
        {
            Stop();
            Start();
        }

        public override void Update(float seconds)
        {
            if (isPaused) return;
            currentFrame += speed;
            throw new NotImplementedException();
        }

        public override void Draw(System.Drawing.Bitmap image)
        {
            if (isPaused) return;
            //draw components based on which frame they should be at
            throw new NotImplementedException();
        }
    }
}
