using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrafficLights
{
    [Serializable]
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
        public event Action<bool> OnPauseStateChanged = (x) => { };
        public event Action<float> OnSpeedChanged = (x) => { };

        /// <summary>
        /// 
        /// All the crossings crosed X-times.
        /// Gets refreshed every 'Stop' of the simulation.
        /// </summary>
        private int XTimes { get; set; }
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

        public float TimePassed { get; private set; }

        /// <summary>
        /// The current frame
        /// </summary>
        float currentFrame;
        /// <summary>
        /// The is paused
        /// </summary>
        bool isPaused = false;
        public bool IsPaused { get { return isPaused; } }

        /// <summary>
        /// Gets the grid.
        /// </summary>
        /// <value>The grid.</value>
        public Grid Grid { get; private set; }

        private float[] speeds = new float[] {
        0.1f, 0.2f, 0.5f,
        1,
        2, 5, 10
        };

        /// <summary>
        /// The speed
        /// </summary>
        private float speed = 1;
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
                OnSpeedChanged(this.speed);
            }
        }

        /// <summary>
        /// Gets the total cars.
        /// </summary>
        /// <value>The total cars.</value>
        public int TotalCars { get { return Grid.AllCrossings.Where(x=>x!=null).Select(x => x.Feeders.Select(y => y.Flow).Sum(y => y)).Sum(x => x); } }

        /// <summary>
        /// Gets the cars passed.
        /// </summary>
        /// <value>The cars passed.</value>
        public int CarsPassed { get { return 0; } set { } }

        /// <summary>
        /// Gets the cars left.
        /// </summary>
        /// <value>The cars left.</value>
        public int CarsLeft { get { return TotalCars - CarsPassed; } }

        public List<Car> CurrentCars
        {
            get
            {
                List<Car> cars = new List<Car>();
                foreach (Crossing crossing in this.Grid.AllCrossings)
                {
                    foreach (Lane lane in crossing.Lanes)
                    {
                        cars.AddRange(lane.CarsCurrentlyOn);
                    }
                }

                return cars;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has pedestrians crossing.
        /// </summary>
        /// <value><c>true</c> if this instance has pedestrians crossing; otherwise, <c>false</c>.</value>
        public bool HasPedestriansCrossing { get { return Grid.AllCrossings.Where(x=>x!=null).Any(x => x.HasPedestriansCrossing); } }

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
            float nextSpeed = speed;
            for (int i = 0; i < this.speeds.Length; i++)
			{
                if (nextSpeed == speeds[i])
                {
                    if (i != this.speeds.Length - 1)
                        nextSpeed = speeds[i+1];
                    break;
                }
			}
            Speed = nextSpeed;
        }

        /// <summary>
        /// Decreases speed by DEFAULT_ADJUST_SPEED
        /// </summary>
        public void DecreaseSpeed()
        {
            float nextSpeed = speed;
            for (int i = 0; i < this.speeds.Length; i++)
            {
                if (nextSpeed == speeds[i])
                {
                    if (i != 0)
                        nextSpeed = speeds[i - 1];
                    break;
                }
            }
            Speed = nextSpeed;
        }

        /// <summary>
        /// Increases the speed.
        /// </summary>
        /// <param name="amount">The amount.</param>
        public void IncreaseSpeed(float amount)
        {
            if (amount < 0) return;

            AdjustSpeed(amount);
        }

        /// <summary>
        /// Decreases the speed.
        /// </summary>
        /// <param name="amount">The amount.</param>
        public void DecreaseSpeed(float amount)
        {
            if (amount > 0) amount *= -1;

            AdjustSpeed(amount);
        }

        /// <summary>
        /// Adjusts the speed.
        /// </summary>
        /// <param name="amount">The amount.</param>
        public void AdjustSpeed(float amount)
        {
            this.Speed += amount;

            this.speed = ((int)(this.speed * 100)) / 100.0f;
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            if (isPaused)
            {
                Resume();
                return;
            }
            SetActive(true);
            isPaused = false;
            OnPauseStateChanged(isPaused);
            this.Grid.Reset();
        }

        /// <summary>
        /// Pauses this instance.
        /// </summary>
        public void Pause()
        {
            if (isPaused) return;
            isPaused = true;
            OnPauseStateChanged(isPaused);
            SetActive(false);
        }

        /// <summary>
        /// Resumes this instance.
        /// </summary>
        public void Resume()
        {
            if (!isPaused) return;
            isPaused = false;
            OnPauseStateChanged(isPaused);
            SetActive(true);
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            OnCompleted(new SimulationResult(this));
            this.CarsPassed =0 ;
            this.XTimes = 0;
            Reset();
        }

        /// <summary>
        /// Finishes this instance.
        /// </summary>
        public void Finish()
        {
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
            Pause();

            this.Grid.Reset();

            TimePassed = 0;
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
            seconds *= speed;
            float fps = 15;
            currentFrame += (seconds * 1000) / (1000/fps);
            foreach (Crossing crossing in Grid.AllCrossings)
                if(crossing != null)
                    crossing.Update(seconds);
            TimePassed += seconds;
            //throw new NotImplementedException();
            Finish();
        }

        /// <summary>
        /// Draws the when normal.
        /// </summary>
        /// <param name="image">The image.</param>
        protected override void DrawWhenNormal(System.Drawing.Graphics image)
        {
        }

        /// <summary>
        /// Draws the when active.
        /// </summary>
        /// <param name="image">The image.</param>
        protected override void DrawWhenActive(System.Drawing.Graphics image)
        {
        }

        /// <summary>
        /// Gets the current simulation result.
        /// </summary>
        /// <value>The current simulation result.</value>
        public SimulationResult CurrentSimulationResult
        {
            get;
            set;
        }

        public int GetXTimesCrossingsCrossed()
        {
            
            foreach(Crossing cr in this.Grid.AllCrossings)
            {
                if (cr !=null)
                {
                    this.XTimes += cr.XTimesCrossed;
                }
                
            }

            return this.XTimes;
        }
    }
}
