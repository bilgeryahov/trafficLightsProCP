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
        List<string> savedCrossingName = new List<string>();

        public SavedManagerForm(TrafficManager mn)
        {
            manager = mn;
            InitializeComponent();
            UpdatePanel();
        }

        private void SavedManagerForm_Load(object sender, EventArgs e)
        {
            
        }

        private void UpdatePanel()
        {
            panel1.Controls.Clear();

            int y = 10;
            int labelCount = 1;

            foreach (Crossing cr in manager.SavedCrossingManager.Crossings)
            {
                Label lbId = new Label();
                lbId.Text = labelCount.ToString();
                lbId.Location = new System.Drawing.Point(10, y + 50);

                Label lbName = new Label();
                lbName.Text = savedCrossingName.ElementAt(labelCount);
                lbName.Location = new System.Drawing.Point(30, y + 50);

                PictureBox pb = new PictureBox();
                pb.Size = new System.Drawing.Size(150, 150);
                pb.Location = new System.Drawing.Point(50, y);
                pb.Image = cr.Image;
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                panel1.Controls.Add(pb);
                panel1.Controls.Add(lbId);
                y += 160;
                labelCount++;                
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                int column = Convert.ToInt32(textBoxColumn.Text);
                int row = Convert.ToInt32(textBoxRow.Text);
                string name = tbName.Text;
                Crossing crossingToBeSaved = manager.Grid.Crossings[row - 1][column - 1];

                if (crossingToBeSaved.IsOnTheGrid)
                {
                    manager.SavedCrossingManager.Add(crossingToBeSaved);
                    savedCrossingName.Add(name);
                    UpdatePanel();
                }
            }
            catch
            {
                MessageBox.Show("There is no crossing there!");
            }                    
        }        

        private void btnPlace_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(tbPlaceId.Text) - 1;
            int row = Convert.ToInt32(textBoxPlaceRow.Text) - 1;
            int column = Convert.ToInt32(tbPlaceColumn.Text) - 1;

            try
            {
                Crossing cr = manager.SavedCrossingManager.Crossings[id];

                manager.Grid.AddAt(row, column, cr);
            }
            catch
            {
                MessageBox.Show("Crossing ID " + (id + 1) + " does not exist in saved crossing manager.");
            }
        }

        private void btnLook_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(tbSearchId.Text) - 1;            
            int cars = 0;

            try
            {
                Crossing cr = manager.SavedCrossingManager.Crossings[id];
                if ((cr is CrossingA) || (cr is CrossingB))
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
                MessageBox.Show("Crossing ID " + (id + 1) + " does not exist in saved crossing manager.");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            manager.SavedCrossingManager.Clear();
            savedCrossingName.Clear();
            panel1.Controls.Clear();
            textBoxRow.Text = "";
            textBoxColumn.Text = "";
            textBoxPlaceRow.Text = "";
            tbLookId.Text = "";
            tbPlaceColumn.Text = "";
            tbPlaceId.Text = "";
            tbPlaceRow.Text = "";
            tbSearchId.Text = "";
        }

        private void btnSearchId_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            int id = Convert.ToInt32(tbId.Text);
            int y = 10;
            try
            {
                foreach (Crossing cr in manager.SavedCrossingManager.Crossings)
                {
                    if (cr.Owner.ToString().Contains(id.ToString()))
                    {
                        Label lb = new Label();
                        lb.Text = cr.Owner.ToString();
                        lb.Location = new System.Drawing.Point(10, y + 50);
                        PictureBox pb = new PictureBox();
                        pb.Size = new System.Drawing.Size(150, 150);
                        pb.Location = new System.Drawing.Point(50, y);
                        pb.Image = cr.Image;
                        pb.SizeMode = PictureBoxSizeMode.StretchImage;
                        panel2.Controls.Add(pb);
                        panel2.Controls.Add(lb);
                        y += 160;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Crossing ID " + (id + 1) + " does not exist in recycle manager.");
            }
        }

        private void btnSearchName_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            string searchName = tbSearchName.Text;
            int y = 10;
            int id;

            try
            {
                foreach (var n in savedCrossingName)
                {
                    if (n.Contains(searchName))
                    {
                        id = n.Count();
                        Crossing cr = manager.SavedCrossingManager.Crossings[id];

                        Label lb = new Label();
                        lb.Text = cr.Owner.ToString();
                        lb.Location = new System.Drawing.Point(10, y + 50);
                        PictureBox pb = new PictureBox();
                        pb.Size = new System.Drawing.Size(150, 150);
                        pb.Location = new System.Drawing.Point(50, y);
                        pb.Image = cr.Image;
                        pb.SizeMode = PictureBoxSizeMode.StretchImage;
                        panel2.Controls.Add(pb);
                        panel2.Controls.Add(lb);
                        y += 160;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Crossing  does not exist in recycle manager.");
            }
        }
    }
}
