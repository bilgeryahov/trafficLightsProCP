using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    /// <summary>Contains the current state of the system</summary>
    public class TrafficManager
    {
        public event Action<SystemState> OnSystemStateChanged = (x) => { }; 

        public Grid Grid { get; private set; }
        public ActionStack UndoRedoStack { get; private set; }
        public SystemState CurrentState { get; private set; }

        public TrafficManager(int rows, int columns)
        {
            this.Grid = new Grid(rows, columns);
        }

        public void ChangeStateTo(SystemState state)
        {
            this.CurrentState = state;
            OnSystemStateChanged(state);
        }

        public Simulation CreateSimulation()
        {
            return new Simulation(this.Grid);
        }

        public void IncreaseSimulationSpeed()
        {
            throw new System.NotImplementedException();
        }

        public void DecreaseSimulationSpeed()
        {
            throw new System.NotImplementedException();
        }

        public void StartSimulation()
        {
            throw new System.NotImplementedException();
        }

        public void StopSimulation()
        {
            throw new System.NotImplementedException();
        }

        public void PauseSimulation()
        {
            throw new System.NotImplementedException();
        }

        public void ResumeSimulation()
        {
            throw new System.NotImplementedException();
        }

        public void FinishSimulation()
        {
            throw new System.NotImplementedException();
        }

        public void RestartSimulation()
        {
            throw new System.NotImplementedException();
        }

        public void RemoveCrossing()
        {
            throw new System.NotImplementedException();
        }

        public void SaveSimulation()
        {
            throw new System.NotImplementedException();
        }

        public void LoadFromFile()
        {
            throw new System.NotImplementedException();
        }

    }
}
