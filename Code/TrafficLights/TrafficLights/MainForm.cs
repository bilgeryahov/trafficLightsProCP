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

        /// <summary>
        /// Handles the Click event of the PicBoxTypeB control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PicBoxTypeB_Click(object sender, EventArgs e)
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
            //update simulation per Tick of the timer
        }

        private void UpdateInterface()
        {
            //calls Render
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
    }
}
