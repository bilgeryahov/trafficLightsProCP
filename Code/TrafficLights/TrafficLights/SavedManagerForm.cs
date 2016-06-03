using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrafficLights
{
    public partial class SavedManagerForm : Form
    {
        TrafficManager manager;

        public SavedManagerForm(TrafficManager mn)
        {
            manager = mn;
            InitializeComponent();
        }

        private void SavedManagerForm_Load(object sender, EventArgs e)
        {
            UpdatePane1();
        }

        private void UpdatePane1()
        {
            panel1.Controls.Clear();

            int y = 10;
            int labelCount = 1;

            foreach (Crossing cr in manager.SavedCrossingManager.Crossings)
            {
                Label lb = new Label();
                lb.Text = labelCount.ToString();
                lb.Location = new System.Drawing.Point(10, y + 50);

                PictureBox pb = new PictureBox();
                pb.Size = new System.Drawing.Size(150, 150);
                pb.Location = new System.Drawing.Point(50, y);
                pb.Image = cr.Image;
                pb.SizeMode = PictureBoxSizeMode.StretchImage;

                panel1.Controls.Add(pb);
                panel1.Controls.Add(lb);

                y += 160;
                labelCount++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Crossing cr = manager.SavedCrossingManager.Crossings[Convert.ToInt32(tbInformation.Text) - 1];
            int cars = 0;
            int pedestrian = 0;
            
            foreach (Lane l in cr.Lanes)
            {
                cars += l.Flow;
            }

            MessageBox.Show(cars.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(tbX.Text);
            int y = Convert.ToInt32(tbY.Text);
            Crossing cr = manager.SavedCrossingManager.Crossings[Convert.ToInt32(tbPlace.Text) - 1];
            manager.Grid.AddAt(x, y, cr);      
        }
    }
}
