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
    /// <summary>
    /// the user interface of the application
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class MainForm : Form
    {
        Dictionary<int, PictureBox> slotIDToPBoxLookup = new Dictionary<int, PictureBox>();
        Dictionary<PictureBox, int> pBoxToSlotIDLookup = new Dictionary<PictureBox, int>();

        private TrafficManager manager = new TrafficManager(3, 3);
        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            ActionStack.OnRedoAltered += (actionsToRedo, actionRedone) =>
            {
                if (actionsToRedo > 0)
                {
                    ToggleControl(redoToolStripMenuItem1, true);
                    ToggleControl(redoBtn, true);
                }
                else
                {
                    ToggleControl(redoToolStripMenuItem1, false);
                    ToggleControl(redoBtn, false);
                }
                PopulateActionStackListbox();
            };

            ActionStack.OnUndoAltered += (actionsToUndo, actionUndone) =>
            {
                if (actionsToUndo > 0)
                {
                    ToggleControl(undoToolStripMenuItem1, true);
                    ToggleControl(undoBtn, true);
                }
                else
                {
                    ToggleControl(undoToolStripMenuItem1, false);
                    ToggleControl(undoBtn, false);
                }
                PopulateActionStackListbox();
            };

            ToggleControl(redoToolStripMenuItem1, false);
            ToggleControl(redoBtn, false);
            ToggleControl(undoToolStripMenuItem1, false);
            ToggleControl(undoBtn, false);

            manager.OnCurrentActiveComponentChanged += (x) =>
                {
                    if (x == null)
                    {
                        propertiesEditGBox.Visible = false;
                        return;
                    }
                    propertiesEditGBox.Visible = true;
                    if (x is Trafficlight)
                        propertiesLbl.Text = "Green interval";
                    else if (x is Lane)
                        propertiesLbl.Text = "Car flow";
                };

            manager.CurrentActiveComponent = null;

            foreach (Control item in this.Controls)
            {
                if (item.Name.StartsWith("gridSlot"))
                {

                    int slotID = int.Parse(item.Name.Substring("gridSlot".Length)) - 1;
                    pBoxToSlotIDLookup.Add(item as PictureBox, slotID);
                    slotIDToPBoxLookup.Add(slotID, item as PictureBox);
                }
            }

            foreach (Control item in this.Controls)
                item.KeyDown += MainForm_KeyDown;

            this.Click += MainForm_Click;

            PicBoxTypeA.Click += PicBoxTypeA_Click;
            PicBoxTypeB.Click += PicBoxTypeB_Click;
            PicBoxTypeC.Click += PicBoxTypeC_Click;

            PicBoxTypeA.Cursor = Cursors.Hand;
            PicBoxTypeB.Cursor = Cursors.Hand;
            PicBoxTypeC.Cursor = Cursors.Hand;
        }

        private void PopulateActionStackListbox()
        {
            lbActions.Items.Clear();
            foreach (UndoableAction action in ActionStack.UndoableActions)
                lbActions.Items.Add(action.ToString());
        }

        private void ToggleControl(Control control, bool state)
        {
            control.Enabled = state;
        }

        private void ToggleControl(ToolStripMenuItem control, bool state)
        {
            control.Enabled = state;
        }

        /// <summary>
        /// Handles the Click event of the helpToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the toolStripMenuItem1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the toolStripMenuItem2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void ShowCrossingsManager(CrossingManager manager)
        {

        }

        private void ShowRecycleCrossings()
        {
            ShowCrossingsManager(this.manager.RecycleCrossingManager);
        }

        private void ShowSavedCrossings()
        {
            ShowCrossingsManager(this.manager.SavedCrossingManager);
        }

        private void UpdateSimulation()
        {
            manager.CurrentSimulation.Update(timer.Interval);
        }

        private void UpdateInterface()
        {
            for (int i = 0; i < (manager.Grid.Columns ^ 2); i++)
            {
                Crossing c = manager.Grid.CrossingAt(i);
                if (c == null)
                    continue;
                c.Draw(slotIDToPBoxLookup[i].Image as Bitmap);
            }
        }

        private void ShowResults(SimulationResult results)
        {
        }

        /// <summary>
        /// Handles the Enter event of the groupBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "Trafic lights manager (*.tlm)|*.tlm";
            System.Windows.Forms.DialogResult result = d.ShowDialog();
            if(result == System.Windows.Forms.DialogResult.OK || result == System.Windows.Forms.DialogResult.Yes)
                manager.LoadFromFile(d.FileName);
        }

        private void ShowSaveDialog()
        {
            SaveFileDialog d = new SaveFileDialog();
            d.Filter = "Trafic lights manager (*.tlm)|*.tlm";
            System.Windows.Forms.DialogResult result = d.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK || result == System.Windows.Forms.DialogResult.Yes)
                if (MessageBox.Show("Would you like to overwrite the specified file?", "Overwrite file", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    manager.SaveToFile(d.FileName);
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowSaveDialog();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActionStack.HasChanges)
                if (MessageBox.Show("There are changes on this grid. Do you want to save them?", "Save changes", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    ShowSaveDialog();
            manager.CreateNewGrid();
        }

        private void undoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ActionStack.Undo();
        }

        private void redoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ActionStack.Redo();
        }

        private Manual activeManual = null;

        private void showHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (activeManual == null || activeManual.IsDisposed)
                activeManual = new Manual();
            activeManual.Show();
            activeManual.Focus();
        }

        private void undoBtn_Click(object sender, EventArgs e)
        {
            ActionStack.Undo();
        }

        private void redoBtn_Click(object sender, EventArgs e)
        {
            ActionStack.Redo();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            manager.StartSimulation();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            manager.PauseSimulation();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            manager.StopSimulation();
        }

        private void btnDecSpeed_Click(object sender, EventArgs e)
        {
            manager.IncreaseSimulationSpeed();
        }

        private void btnIncSpeed_Click(object sender, EventArgs e)
        {
            manager.DecreaseSimulationSpeed();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            manager.RestartSimulation();
        }

        private void btnSaveStats_Click(object sender, EventArgs e)
        {
            if(manager.CurrentSimulation != null)
            {
                //todo excel; snapshot
                
            }
        }

        private void updateFlowBtn_Click(object sender, EventArgs e)
        {
            if (manager.CurrentActiveLane != null)
                manager.CurrentActiveLane.UpdateFlow((int)propertiesEditNUD.Value);
            else if (manager.CurrentActiveTrafficLight != null)
                manager.CurrentActiveTrafficLight.GreenSeconds = (float)propertiesEditNUD.Value;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            UpdateSimulation();
        }

         /// <summary>
        /// Changes border of the picturebox crossing, whenever clicked on it.
        /// </summary>
        /// <param name="thePictureBox"></param>
        /// <param name="isActive">Pass true if you want to make it active - clicked on</param>
        private void ChangeCursor(PictureBox thePictureBox, bool isActive)
        {
            if (isActive)
            {
                thePictureBox.BorderStyle = BorderStyle.Fixed3D;
                ChangeGridSlotsCursor(true);
            }
            else
            {
                thePictureBox.BorderStyle = BorderStyle.None;
            }
        }
  private void ChangeGridSlotsCursor(bool isActive)
        {
            if (isActive)
            {
                gridSlot1.Cursor = Cursors.Hand;
                gridSlot2.Cursor = Cursors.Hand;
                gridSlot3.Cursor = Cursors.Hand;
                gridSlot4.Cursor = Cursors.Hand;
                gridSlot5.Cursor = Cursors.Hand;
                gridSlot6.Cursor = Cursors.Hand;
                gridSlot7.Cursor = Cursors.Hand;
                gridSlot8.Cursor = Cursors.Hand;
                gridSlot9.Cursor = Cursors.Hand;
            }
            else
            {
                gridSlot1.Cursor = Cursors.Default;
                gridSlot2.Cursor = Cursors.Default;
                gridSlot3.Cursor = Cursors.Default;
                gridSlot4.Cursor = Cursors.Default;
                gridSlot5.Cursor = Cursors.Default;
                gridSlot6.Cursor = Cursors.Default;
                gridSlot7.Cursor = Cursors.Default;
                gridSlot8.Cursor = Cursors.Default;
                gridSlot9.Cursor = Cursors.Default;
            }
            
        }

   private void MainForm_Click(object sender, EventArgs e)
        {
            /*
                Whenever the user clicks somewhere in the form, the crossing pictures will return out of mode.
            */
            ChangeCursor(PicBoxTypeA, false);
            ChangeCursor(PicBoxTypeB, false);
            ChangeCursor(PicBoxTypeC, false);

            ChangeGridSlotsCursor(false);
        }
   private void PicBoxTypeA_Click(object sender, EventArgs e)
        {
            ChangeCursor(PicBoxTypeA,true);
            ChangeCursor(PicBoxTypeB, false);
            ChangeCursor(PicBoxTypeC, false);
        }

        private void PicBoxTypeC_Click(object sender, EventArgs e)
        {
            ChangeCursor(PicBoxTypeC,true);
            ChangeCursor(PicBoxTypeA, false);
            ChangeCursor(PicBoxTypeB, false);
        }

        private void PicBoxTypeB_Click(object sender, EventArgs e)
        {
            ChangeCursor(PicBoxTypeB,true);
            ChangeCursor(PicBoxTypeA, false);
            ChangeCursor(PicBoxTypeC, false);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ChangeCursor(PicBoxTypeA, false);
                ChangeCursor(PicBoxTypeB, false);
                ChangeCursor(PicBoxTypeC, false);

                ChangeGridSlotsCursor(false);
            }
        }
    }
}
