using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrafficLights
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            //TrafficManager m = new TrafficManager(4, 3);
            //Crossing c = new CrossingA(m);
            //Car car = new Car();
            //car.From = c.Feeders.First();
            //car.Direction = car.From.To;
            
        }
    }
}
