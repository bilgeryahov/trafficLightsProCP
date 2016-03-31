using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficLights
{
    /// <summary>
    /// handles a simulation based on the grid created by the System
    /// </summary>
    /// <seealso cref="TrafficLights.Renderable" />
    public class Simulation : Renderable
    {
        /// <summary>
        /// Occurs when [on completed].
        /// </summary>
        public event Action<SimulationResult> OnCompleted = (x) => { };

        /// <summary>
        /// The min speed
        /// </summary>
        public const float MIN_SPEED = 0.1f;
        /// <summary>
        /// The max speed
        /// </summary>
        public const float MAX_SPEED = 10;
        /// <summary>
        /// The default adjust speed
        /// </summary>
        public const float DEFAULT_ADJUST_SPEED = 0.1f;
        /// <summary>
        /// The default grid rows
        /// </summary>
        public const float DEFAULT_GRID_ROWS = 3;
        /// <summary>
        /// The default grid columns
        /// </summary>
        public const float DEFAULT_GRID_COLUMNS = 3;

        /// <summary>
        /// The current frame
        /// </summary>
        float currentFrame;
        /// <summary>
        /// The is paused
        /// </summary>
        bool isPaused;

        /// <summary>
        /// Gets the grid.
        /// </summary>
        /// <value>The grid.</value>
        public Grid Grid { get; private set; }

        /// <summary>
        /// The speed
        /// </summary>
        private float speed;
        /// <summary>
        /// Gets the speed.
        /// </summary>
        /// <value>The speed.</value>
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

        /// <summary>
        /// Gets the total cars.
        /// </summary>
        /// <value>The total cars.</value>
        public int TotalCars { get { return Grid.AllCrossings.Select(x => x.Feeders.Select(y => y.Flow).Sum(y => y)).Sum(x => x); } }

        /// <summary>
        /// Gets the cars passed.
        /// </summary>
        /// <value>The cars passed.</value>
        public int CarsPassed { get { throw new System.NotImplementedException(); } }

        /// <summary>
        /// Gets the cars left.
        /// </summary>
        /// <value>The cars left.</value>
        public int CarsLeft { get { return TotalCars - CarsPassed; } }
        /// <summary>
        /// Gets a value indicating whether this instance has pedestrians crossing.
        /// </summary>
        /// <value><c>true</c> if this instance has pedestrians crossing; otherwise, <c>false</c>.</value>
        public bool HasPedestriansCrossing { get { return Grid.AllCrossings.Any(x => x.HasPedestriansCrossing); } }

        /// <summary>
        /// Initializes a new instance of the <see cref="Simulation"/> class.
        /// </summary>
        /// <param name="grid">The grid.</param>
        public Simulation(Grid grid)
        {
            this.Grid = grid;
        }

        // <summary> Increases speed by DEFAULT_ADJUST_SPEED </summary>
        /// <summary>
        /// Increases the speed.
        /// </summary>
        public void IncreaseSpeed()
        {
            IncreaseSpeed(DEFAULT_ADJUST_SPEED);
        }

        /// <summary>
        /// Decreases speed by DEFAULT_ADJUST_SPEED
        /// </summary>
        public void DecreaseSpeed()
        {
            DecreaseSpeed(DEFAULT_ADJUST_SPEED);
        }

        /// <summary>
        /// Increases the speed.
        /// </summary>
        /// <param name="amount">The amount.</param>
        public void IncreaseSpeed(float amount)
        {
            if (amount < 0) return;

            AdjustSpeed(DEFAULT_ADJUST_SPEED);
        }

        /// <summary>
        /// Decreases the speed.
        /// </summary>
        /// <param name="amount">The amount.</param>
        public void DecreaseSpeed(float amount)
        {
            if (amount < 0) amount *= -1;

            AdjustSpeed(amount);
        }

        /// <summary>
        /// Adjusts the speed.
        /// </summary>
        /// <param name="amount">The amount.</param>
        public void AdjustSpeed(float amount)
        {
            this.Speed += amount;
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            throw new System.NotImplementedException();
            if (isPaused)
            {
                Resume();
                return;
            }
        }

        /// <summary>
        /// Pauses this instance.
        /// </summary>
        public void Pause()
        {
            if (isPaused) return;
            throw new System.NotImplementedException();
            isPaused = true;
        }

        /// <summary>
        /// Resumes this instance.
        /// </summary>
        public void Resume()
        {
            throw new System.NotImplementedException();
            if (!isPaused) return;
            isPaused = false;
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            throw new System.NotImplementedException();
            Reset();
        }

        /// <summary>
        /// Finishes this instance.
        /// </summary>
        public void Finish()
        {
            throw new System.NotImplementedException();
            if (CarsLeft == 0 && !HasPedestriansCrossing)
            {
                OnCompleted(new SimulationResult(this));
                Stop();
            }

        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public void Reset()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Restarts this instance.
        /// </summary>
        public void Restart()
        {
            Stop();
            Start();
        }

        /// <summary>
        /// Updates the specified seconds.
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        public override void Update(float seconds)
        {
            if (isPaused) return;
            currentFrame += speed;
            throw new NotImplementedException();
        }

        /// <summary>
        /// Draws the when normal.
        /// </summary>
        /// <param name="image">The image.</param>
        protected override void DrawWhenNormal(System.Drawing.Bitmap image)
        {
            if (isPaused) return;
            //draw components based on which frame they should be at
            throw new NotImplementedException();
        }

        /// <summary>
        /// Draws the when active.
        /// </summary>
        /// <param name="image">The image.</param>
        protected override void DrawWhenActive(System.Drawing.Bitmap image)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the current simulation result.
        /// </summary>
        /// <value>The current simulation result.</value>
        public SimulationResult CurrentSimulationResult
        {
            get
            {
                throw new System.NotImplementedException();
            }
            private set
            {
            }
        }
    }
}
