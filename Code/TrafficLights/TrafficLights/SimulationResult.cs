using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    /// <summary>contains the results of a performed simulation</summary>
    public class SimulationResult
    {
        public DateTime DatePerformed { get; private set; }
        public Simulation SimulationSetup { get; private set; }
        //todo what kind of results to be kept

        public SimulationResult(Simulation simulation)
        {
            this.SimulationSetup = simulation;
        }

        public void ExportToExcel(string fileName)
        {
            throw new NotImplementedException();
        }

        public void CreateSnapShot(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
