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
    public partial class RecycleManagerForm : Form
    {
        TrafficManager manager;
        public RecycleManagerForm(TrafficManager mn)
        {
            manager = mn;
            InitializeComponent();
        }

        private void RecycleManagerForm_Load(object sender, EventArgs e)
        {
            UpdatePanel();
        }

        private void UpdatePanel()
        {
            panel1.Controls.Clear();

            int y = 10;
            int labelCount = 1;

            foreach (Crossing cr in manager.RecycleCrossingManager.Crossings)
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
            int id = Convert.ToInt32(textBox1.Text) - 1;

            try
            {
                Crossing cr = manager.RecycleCrossingManager.Crossings[id];

                manager.Grid.AddAt(cr.RowRecycleManager, cr.ColumnRecycleManager, cr);
                manager.RecycleCrossingManager.Remove(cr);

                UpdatePanel();
            }
            catch
            {
                MessageBox.Show("Crossing ID " + (id + 1) + " does not exist in recycle manager.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox2.Text) - 1;
            int cars = 0;

            try
            {
                Crossing cr = manager.RecycleCrossingManager.Crossings[id];
                if((cr is CrossingA) || (cr is CrossingB))
                {
                    foreach (Lane l in cr.Lanes)
                    {
                        cars += l.Flow;
                    }
                    MessageBox.Show("Total car flow is " + cars.ToString());
                }
                else
                {
                    foreach (Lane l in cr.Lanes)
                    {
                        cars += l.Flow;
                    }
                    MessageBox.Show("Total car flow is " + cars.ToString());
                }
            }
            catch
            {
                MessageBox.Show("Crossing ID " + (id + 1) + " does not exist in recycle manager.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            manager.RecycleCrossingManager.Clear();
            panel1.Controls.Clear();
        }
    }
}
