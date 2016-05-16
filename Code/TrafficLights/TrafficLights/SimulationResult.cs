using ExportToExcel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;

namespace TrafficLights
{
    /// <summary>
    /// contains the results of a performed simulation
    /// </summary>
    public class SimulationResult
    {
        /// <summary>
        /// Gets the date performed.
        /// </summary>
        /// <value>The date performed.</value>
        public DateTime DatePerformed { get; private set; }
        /// <summary>
        /// Gets the simulation setup.
        /// </summary>
        /// <value>The simulation setup.</value>
        public Simulation SimulationSetup { get; private set; }
        //todo what kind of results to be kept

        /// <summary>
        /// Initializes a new instance of the <see cref="SimulationResult"/> class.
        /// </summary>
        /// <param name="simulation">The simulation.</param>
        public SimulationResult(Simulation simulation)
        {
            this.SimulationSetup = simulation;
        }

        /// <summary>
        /// Exports to excel.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void ExportToExcel(string fileName)
        {
            //throw new NotImplementedException();

            DataTable myTable = new DataTable();
            myTable.Columns.Add("name", typeof(string));
            myTable.Columns.Add("age", typeof(int));
            myTable.Rows.Add("bilger", 20);
            myTable.Rows.Add("bilger next year", 21);




            // Step 2: Create the Excel .xlsx file
            try
            {
                // if you want to try this, uncomment it and change the path where the excel file will be created.
                CreateExcelFile.CreateExcelDocument(myTable, "E:\\Sample.xlsx");
            }
            catch (Exception ex)
            {
                return;
            }
        }

        /// <summary>
        /// Creates the snap shot.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void CreateSnapShot(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
