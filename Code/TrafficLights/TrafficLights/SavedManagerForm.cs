﻿using System;
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
            UpdatePanel();
        }

        private void SavedManagerForm_Load(object sender, EventArgs e)
        {
            
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                int column = Convert.ToInt32(textBoxColumn.Text);
                int row = Convert.ToInt32(textBoxRow.Text);
                Crossing crossingToBeSaved = manager.Grid.Crossings[row - 1][column - 1];
                manager.SavedCrossingManager.Add(crossingToBeSaved);
                UpdatePanel();
            }
            catch
            {
                MessageBox.Show("There is no crossing there!");
            }            
        
        }

        private void UpdatePanel()
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
    }
}
