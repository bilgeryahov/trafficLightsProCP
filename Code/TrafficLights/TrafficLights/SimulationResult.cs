using ExportToExcel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

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
        public DateTime DatePerformed { get; set; }
        /// <summary>
        /// Gets the simulation setup.
        /// </summary>
        /// <value>The simulation setup.</value>
        public Simulation SimulationSetup { get; set; }
        
        /// <summary>
        /// The time passed since the beginning of the simulation.
        /// </summary>
        public float TimePassed { get; set; }

        /// <summary>
        ///  Total number of cars set up from the beginning of the simulation.
        /// </summary>
        public int TotalCars { get; set; }

        /// <summary>
        /// The number of cars crossed at least one crossing.
        /// </summary>
        public int CarsCrossed { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimulationResult"/> class.
        /// </summary>
        /// <param name="simulation">The simulation.</param>
        public SimulationResult(Simulation simulation)
        {
            this.SimulationSetup = simulation;
            this.SimulationSetup.CurrentSimulationResult = this;           
        }

        public void SaveResults(float theTime, int totalCars, int carsCrossed)
        {
            this.DatePerformed = DateTime.Now;
            this.TimePassed = theTime;
            this.TotalCars = totalCars;
            this.CarsCrossed = carsCrossed;
        }

        /// <summary>
        /// Exports to excel.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void ExportToExcel(string fileName)
        {
            DataTable myTable = new DataTable();
            myTable.Columns.Add("Date Performed", typeof(DateTime));
            myTable.Columns.Add("Time Passed", typeof(float));
            myTable.Columns.Add("Total Cars", typeof(int));
            myTable.Columns.Add("Cars Crossed", typeof(int));
            myTable.Rows.Add(this.DatePerformed, this.TimePassed, this.TotalCars, this.CarsCrossed);
            
            // Step 2: Create the Excel .xlsx file
            try
            {
                CreateExcelFile.CreateExcelDocument(myTable, fileName + ".xlsx");
                MessageBox.Show("Creating results Excel file is successful!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Creates the snap shot.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void CreateSnapShot(string fileName)
        {
            try
            {
                var frm = Form.ActiveForm;
                using (var bmp = new Bitmap(frm.Width, frm.Height))
                {
                    frm.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
                    bmp.Save(fileName + ".png");
                    MessageBox.Show("Snapshot created!");
                }
            }
            catch
            {
                MessageBox.Show("Failed to create the snapshot!");
            }
            
        }
    }
}
