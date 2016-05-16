using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace TrafficLights
{
    /// <summary>
    /// is responsible for all the actions that can be done from the GUI and represents the system
    /// </summary>
    public class TrafficManager
    {
        public string CurrentLoadedPath { get; private set; }
        /// <summary>
        /// Gets the current simulation or null if not created
        /// </summary>
        public Simulation CurrentSimulation { get; private set; }
        /// <summary>
        /// Occurs when [on system state changed].
        /// </summary>
        public event Action<SystemState> OnSystemStateChanged = (x) => { };


        /// <summary>
        /// Gets the grid.
        /// </summary>
        /// <value>The grid.</value>
        public Grid Grid { get; private set; }

        /// <summary>
        /// Manager with the recycled crossings
        /// </summary>
        public RecycleManager RecycleCrossingManager { get; private set; }
        /// <summary>
        ///  Manager with the saved crossings
        /// </summary>
        public SavedManager SavedCrossingManager { get; private set; }

        /// <summary>
        /// Gets the state of the current.
        /// </summary>
        /// <value>The state of the current.</value>
        public SystemState CurrentState { get; private set; }

        public event Action<Component> OnCurrentActiveComponentChanged = (x) => { };
        private Component currentActiveComponent = null;
        public Component CurrentActiveComponent
        {
            get { return currentActiveComponent; }
            set
            {
                if (currentActiveComponent != null)
                    currentActiveComponent.SetActive(false);
                currentActiveComponent = value;
                if (currentActiveComponent != null)
                    currentActiveComponent.SetActive(true);
                OnCurrentActiveComponentChanged(value);
            }
        }
        public Trafficlight CurrentActiveTrafficLight { get { return CurrentActiveComponent as Trafficlight; } }
        public Lane CurrentActiveLane { get { return CurrentActiveComponent as Lane; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrafficManager"/> class.
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <param name="columns">The columns.</param>
        public TrafficManager(int rows, int columns)
        {
            this.Grid = new Grid(rows, columns);

            this.RecycleCrossingManager = new RecycleManager();
            this.SavedCrossingManager = new SavedManager();
        }

        /// <summary>
        /// Changes the state to.
        /// </summary>
        /// <param name="state">The state.</param>
        public void ChangeStateTo(SystemState state)
        {
            this.CurrentState = state;
            OnSystemStateChanged(state);
        }

        /// <summary>
        /// Creates the simulation.
        /// </summary>
        public void CreateSimulation()
        {
            if (this.CurrentSimulation != null) this.CurrentSimulation.Stop();
            this.CurrentSimulation = new Simulation(this.Grid);
        }

        /// <summary>
        /// Increases the simulation speed.
        /// </summary>
        public void IncreaseSimulationSpeed()
        {
            this.CurrentSimulation.IncreaseSpeed();
        }

        /// <summary>
        /// Decreases the simulation speed.
        /// </summary>
        public void DecreaseSimulationSpeed()
        {
            this.CurrentSimulation.DecreaseSpeed();
        }

        /// <summary>
        /// Starts the simulation.
        /// </summary>
        public void StartSimulation()
        {
            this.CurrentSimulation.Start();
        }

        /// <summary>
        /// Stops the simulation.
        /// </summary>
        public void StopSimulation()
        {
            this.CurrentSimulation.Stop();
        }

        /// <summary>
        /// Pauses the simulation.
        /// </summary>
        public void PauseSimulation()
        {
            this.CurrentSimulation.Pause();
        }

        /// <summary>
        /// Resumes the simulation.
        /// </summary>
        public void ResumeSimulation()
        {
            this.CurrentSimulation.Resume();
        }

        /// <summary>
        /// Finishes the simulation.
        /// </summary>
        public void FinishSimulation()
        {
            this.CurrentSimulation.Finish();
        }

        /// <summary>
        /// Restarts the simulation.
        /// </summary>
        public void RestartSimulation()
        {
            this.CurrentSimulation.Restart();
        }

        /// <summary>
        /// Removes the crossing.
        /// </summary>
        public void RemoveCrossing(int row, int column)
        {
            this.Grid.RemoveAt(row, column);
        }

        /// <summary>
        /// Places the crossing on a specific row and column
        /// </summary>
        public void PlaceCrossing(Crossing crossing, int row, int column)
        {
            this.Grid.AddAt(row, column, crossing);
        }

        /// <summary>
        /// Saves the simulation.
        /// </summary>
        public void SaveToFile(string filepath)
        {
            FileStream myFileStream = null;
            BinaryFormatter myBinaryFormatter = null;

            try
            {
                myFileStream = new FileStream(filepath, FileMode.Create, FileAccess.Write);
                myBinaryFormatter = new BinaryFormatter();

                myBinaryFormatter.Serialize(myFileStream, this.Grid);
                CurrentLoadedPath = filepath;
                //Notify for success?
            }

            catch (SerializationException)
            {
                //Notify for failure?
            }
            catch (IOException)
            {
                //Notify for failure?
            }
            finally
            {
                if (myFileStream != null)
                {
                    myFileStream.Close();
                }
            }
        }

        /// <summary>
        /// Loads from file.
        /// </summary>
        public void LoadFromFile(string filepath)
        {
            //Are there unsaved changes?

            FileStream myFileStream = null;
            BinaryFormatter myBinaryFormatter = null;
            try
            {
                myFileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                myBinaryFormatter = new BinaryFormatter();

                this.Grid = (Grid)myBinaryFormatter.Deserialize(myFileStream);
                CurrentLoadedPath = filepath;
                ProcessNewGridLoaded();
                throw new NotImplementedException("Update grid UI");
                //Notify for success?
            }

            catch (SerializationException)
            {
                //Notify for failure?
            }
            catch (IOException)
            {
                //Notify for failure?
            }
            finally
            {
                if (myFileStream != null)
                {
                    myFileStream.Close();
                }
            }
        }

        public void CreateNewGrid()
        {
            for (int i = 0; i < Grid.Rows; i++)
            {
                for (int j = 0; j < Grid.Columns; j++)
                {
                    if(Grid.CrossingAt(i*Grid.Rows+j)!=null)
                    Grid.RemoveAt(i,j);
                }
            }
            ProcessNewGridLoaded();
        }

        private void ProcessNewGridLoaded()
        {
            this.CurrentSimulation = null;
            ActionStack.Clear();
        }
    }
}
