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
        /// Gets the undo redo stack.
        /// </summary>
        /// <value>The undo redo stack.</value>
        public ActionStack UndoRedoStack { get; private set; }

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

        /// <summary>
        /// Initializes a new instance of the <see cref="TrafficManager"/> class.
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <param name="columns">The columns.</param>
        public TrafficManager(int rows, int columns)
        {
            this.Grid = new Grid(rows, columns);
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
            // Increase speed with?
        }

        /// <summary>
        /// Decreases the simulation speed.
        /// </summary>
        public void DecreaseSimulationSpeed()
        {
            this.CurrentSimulation.DecreaseSpeed();
            // Decrease speed with?
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
        public void SaveSimulation()
        {
            FileStream myFileStream = null;
            BinaryFormatter myBinaryFormatter = null;

            try
            {
                myFileStream = new FileStream(this.CurrentSimulation.Destination + ".tlm", FileMode.Create, FileAccess.Write);
                myBinaryFormatter = new BinaryFormatter();

                myBinaryFormatter.Serialize(myFileStream, this.CurrentSimulation);
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
        public void LoadFromFile()
        {
            //Are there unsaved changes?

            FileStream myFileStream = null;
            BinaryFormatter myBinaryFormatter = null;
            try
            {
                myFileStream = new FileStream("", FileMode.Open, FileAccess.Read);
                myBinaryFormatter = new BinaryFormatter();

                this.CurrentSimulation = (Simulation)myBinaryFormatter.Deserialize(myFileStream);

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

    }
}
