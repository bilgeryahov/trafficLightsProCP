using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    /// <summary>
    /// is responsible for all the actions that can be done from the GUI and represents the system
    /// </summary>
    public class TrafficManager
    {
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
        /// <returns>Simulation.</returns>
        public Simulation CreateSimulation()
        {
            return new Simulation(this.Grid);
        }

        /// <summary>
        /// Increases the simulation speed.
        /// </summary>
        public void IncreaseSimulationSpeed()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Decreases the simulation speed.
        /// </summary>
        public void DecreaseSimulationSpeed()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Starts the simulation.
        /// </summary>
        public void StartSimulation()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Stops the simulation.
        /// </summary>
        public void StopSimulation()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Pauses the simulation.
        /// </summary>
        public void PauseSimulation()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Resumes the simulation.
        /// </summary>
        public void ResumeSimulation()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Finishes the simulation.
        /// </summary>
        public void FinishSimulation()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Restarts the simulation.
        /// </summary>
        public void RestartSimulation()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Removes the crossing.
        /// </summary>
        public void RemoveCrossing()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Saves the simulation.
        /// </summary>
        public void SaveSimulation()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Loads from file.
        /// </summary>
        public void LoadFromFile()
        {
            throw new System.NotImplementedException();
        }

    }
}
