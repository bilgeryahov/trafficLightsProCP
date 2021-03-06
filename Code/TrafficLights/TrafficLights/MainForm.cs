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
    /// <summary>
    /// the user interface of the application
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class MainForm : Form
    {
        Dictionary<int, PictureBox> slotIDToPBoxLookup = new Dictionary<int, PictureBox>();
        Dictionary<PictureBox, int> pBoxToSlotIDLookup = new Dictionary<PictureBox, int>();
        private SystemState state;
        private Crossing crossingToBePlaced;
        private TrafficManager manager = new TrafficManager(3, 3);
        SavedManagerForm smform;
        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            smform = new SavedManagerForm(manager);
            state = SystemState.None;
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

                if (actionRedone is UpdateFlowAction && manager.CurrentActiveLane == (actionRedone as UpdateFlowAction).Lane)
                {
                    propertiesEditNUD.Value = manager.CurrentActiveLane.Flow;
                }
                else if (actionRedone is UpdateLightIntervalAction && manager.CurrentActiveTrafficLight == (actionRedone as UpdateLightIntervalAction).Light)
                {
                    propertiesEditNUD.Value = (decimal)manager.CurrentActiveTrafficLight.GreenSeconds;
                }
                else if (actionRedone is UpdateMultipleIntervalAction)
                {
                    if (manager.CurrentActiveTrafficLight != null)
                    {
                        if (!isApplying)
                            propertiesEditNUD.Value = (decimal)manager.CurrentActiveTrafficLight.GreenSeconds;
                      
                            slotIDToPBoxLookup[(actionRedone as UpdateMultipleIntervalAction).Crossing.Column + (actionRedone as UpdateMultipleIntervalAction).Crossing.Row * 3].Invalidate();
                    }
                }
                else if (actionRedone is UpdateMultipleFlowAction)
                {
                    if (manager.CurrentActiveLane != null)
                    {
                        if(!isApplying)
                            propertiesEditNUD.Value = manager.CurrentActiveLane.Flow;
                        //if (manager.CurrentActiveLane.Owner.Owner.IsOnTheGrid)
                       
                    }slotIDToPBoxLookup[(actionRedone as UpdateMultipleFlowAction).Crossing.Column + (actionRedone as UpdateMultipleFlowAction).Crossing.Row * 3].Invalidate();
                }
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
                if (actionUndone is UpdateFlowAction && manager.CurrentActiveLane == (actionUndone as UpdateFlowAction).Lane)
                {
                    propertiesEditNUD.Value = manager.CurrentActiveLane.Flow;
                }
                else if (actionUndone is UpdateLightIntervalAction && manager.CurrentActiveTrafficLight == (actionUndone as UpdateLightIntervalAction).Light)
                {
                    propertiesEditNUD.Value = (decimal)manager.CurrentActiveTrafficLight.GreenSeconds;
                }
                else if (actionUndone is UpdateMultipleIntervalAction)
                {
                    if (manager.CurrentActiveTrafficLight != null)
                    {
                        if (!isApplying)
                            propertiesEditNUD.Value = (decimal)manager.CurrentActiveTrafficLight.GreenSeconds;
                        
                        slotIDToPBoxLookup[(actionUndone as UpdateMultipleIntervalAction).Crossing.Column + (actionUndone as UpdateMultipleIntervalAction).Crossing.Row * 3].Invalidate();
                    }
                }
                else if (actionUndone is UpdateMultipleFlowAction)
                {
                    if (manager.CurrentActiveLane != null)
                    {
                        
                        if (!isApplying)
                            propertiesEditNUD.Value = manager.CurrentActiveLane.Flow;
                        //if (manager.CurrentActiveLane.Owner.Owner.IsOnTheGrid)
                            
                    }slotIDToPBoxLookup[(actionUndone as UpdateMultipleFlowAction).Crossing.Column + (actionUndone as UpdateMultipleFlowAction).Crossing.Row * 3].Invalidate();
                }
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
                    {
                        propertiesLbl.Text = "Green interval";
                        propertiesEditNUD.Value = (decimal)manager.CurrentActiveTrafficLight.GreenSeconds;
                    }
                        
                    else if (x is Lane)
                    {
                        propertiesLbl.Text = "Car flow";
                        propertiesEditNUD.Value = manager.CurrentActiveLane.Flow;
                    } 
                };

            manager.CurrentActiveComponent = null;

            foreach (Control item in this.Controls)
            {
                if (item.Name.StartsWith("gridSlot"))
                {

                    int slotID = int.Parse(item.Name.Substring("gridSlot".Length)) - 1;
                    pBoxToSlotIDLookup.Add(item as PictureBox, slotID);
                    slotIDToPBoxLookup.Add(slotID, item as PictureBox);


                    item.Click += (x, y) =>
                    {
                        if (state != SystemState.None)
                        {
                            PlaceCrossing(x as PictureBox);
                            RemoveCrossing(x as PictureBox);

                            Form fc = Application.OpenForms["SavedManagerForm"];

                            if (fc != null)
                            {
                                int row = pBoxToSlotIDLookup[x as PictureBox] / 3;
                                int column = pBoxToSlotIDLookup[x as PictureBox] % 3;
                                manager.SavedCrossingManager.Add(manager.Grid[row][column]);
                               smform.UpdatePanel();
                            }
                        }
                        else
                        {
                            SelectComponent(x as PictureBox, y as MouseEventArgs);
                        }
                    };

                    item.Paint += (x, y) =>
                    {
                        int slot = pBoxToSlotIDLookup[x as PictureBox];
                        Crossing crossing = manager.Grid.CrossingAt(slot);
                        if(crossing!=null)
                                crossing.Draw(y.Graphics);
                    };
                }
            }

            foreach (Control item in this.Controls)
                item.KeyDown += MainForm_KeyDown;

            this.Click += MainForm_Click;

            PicBoxTypeA.Click += PicBoxTypeA_Click;
            PicBoxTypeA.Click += ClearToggles;
            PicBoxTypeB.Click += PicBoxTypeB_Click;
            PicBoxTypeB.Click += ClearToggles;
            PicBoxTypeC.Click += PicBoxTypeC_Click;
            PicBoxTypeC.Click += ClearToggles;

            PicBoxTypeA.Cursor = Cursors.Hand;
            PicBoxTypeB.Cursor = Cursors.Hand;
            PicBoxTypeC.Cursor = Cursors.Hand;

            // Attach the grid.
            this.AttachGrid();

            this.manager.GridLoaded += () =>
            {
                gridSlot1.Image = null;
                gridSlot2.Image = null;
                gridSlot3.Image = null;
                gridSlot4.Image = null;
                gridSlot5.Image = null;
                gridSlot6.Image = null;
                gridSlot7.Image = null;
                gridSlot8.Image = null;
                gridSlot9.Image = null;

                foreach (Crossing[] c in this.manager.Grid.Crossings)
                {
                    foreach(Crossing cr in c)
                    {
                        if(cr != null)
                        {
                            int id = cr.Row * 3 + cr.Column;
                            this.slotIDToPBoxLookup[id].Image = cr.Image;
                        }
                    }
                }
            };
            manager.CurrentSimulation.OnPauseStateChanged +=
                (isPaused) =>
                {
                    timer.Enabled = !isPaused;
                    ToggleControl(undoToolStripMenuItem1, isPaused);
                    ToggleControl(redoToolStripMenuItem1, isPaused);
                    ToggleControl(undoBtn, isPaused);
                    ToggleControl(redoBtn, isPaused);

                    ToggleControl(newSimulationToolStripMenuItem, isPaused);
                    ToggleControl(openToolStripMenuItem, isPaused);
                    ToggleControl(saveToolStripMenuItem1, isPaused);

                    ToggleControl(btnSaveCrossingManager, isPaused);
                    ToggleControl(button2, isPaused);
                    ToggleControl(button1, isPaused);
                    ToggleControl(updatePropertiesBtn, isPaused);

                    ToggleControl(PicBoxTypeA, isPaused);
                    ToggleControl(PicBoxTypeB, isPaused);
                    ToggleControl(PicBoxTypeC, isPaused);

                    if (!isPaused)
                    {
                        manager.CurrentActiveComponent = null;

                        ChangeBorder(PicBoxTypeA, false);
                        ChangeBorder(PicBoxTypeB, false);
                        ChangeBorder(PicBoxTypeC, false);
                        ChangeGridSlotsCursor(false);
                        button2.FlatStyle = FlatStyle.Standard;
                        rmToggled = false;
                        state = SystemState.None;
                    }
                };
            manager.CurrentSimulation.OnSpeedChanged += (x) => lblSpeed.Text = x + "x";
            manager.CurrentSimulation.OnCompleted += (x) => {
              
                timer.Stop();

                listBox1.Items.Clear();

                x.SaveResults(x.SimulationSetup.TimePassed, x.SimulationSetup.TotalCars, x.SimulationSetup.GetXTimesCrossingsCrossed());

                listBox1.Items.Add("Date performed: ");
                listBox1.Items.Add(x.DatePerformed.ToString());

                listBox1.Items.Add("");

                listBox1.Items.Add("Time passed: ");
                listBox1.Items.Add(x.TimePassed.ToString());

                listBox1.Items.Add("");

                listBox1.Items.Add("Total cars: ");
                listBox1.Items.Add(x.TotalCars.ToString());

                listBox1.Items.Add("");

                listBox1.Items.Add("Successfully crossings of cars: ");
                listBox1.Items.Add(x.CarsCrossed.ToString());
            };
        }
        bool isApplying = false;
        /// <summary>
        /// Split up in a separate function since after loading a new grid, it should be attached
        /// and to reduce repetitions the piece of code is split up into a method.
        /// </summary>
        private void AttachGrid()
        {
            this.manager.Grid.OnCrossingAdded += (crossing, row, column) =>
            {
                int id = row * 3 + column;
                this.slotIDToPBoxLookup[id].Image = crossing.Image;
            };

            this.manager.Grid.OnCrossingRemoved += (crossing, row, column) =>
            {
                int id = row * 3 + column;
                this.slotIDToPBoxLookup[id].Image = null;
            };
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
            manager.CurrentSimulation.Update(timer.Interval / 1000.0f);
            RefreshAll();

            int seconds = (int)manager.CurrentSimulation.TimePassed;
            int minutes = seconds / 60;
            seconds %= 60;
            TimeElapsed.Text = minutes + ":" + seconds;
        }

        private void UpdateInterface()
        {
            RefreshAll();
        }

        private void ShowResults(SimulationResult results)
        {
        }

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

            // Attach the newly loaded grid.
            this.AttachGrid();

            // Refresh the results listbox.
            listBox1.Items.Clear();

        }

        private void ShowSaveDialog()
        {
            SaveFileDialog d = new SaveFileDialog();
            d.Filter = "Trafic lights manager (*.tlm)|*.tlm";
            System.Windows.Forms.DialogResult result = d.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK || result == System.Windows.Forms.DialogResult.Yes)
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
            RefreshAll();

            // Refresh the results listbox.
            listBox1.Items.Clear();
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
            RefreshAll();
        }

        private void RefreshAll()
        {
            foreach (var item in this.pBoxToSlotIDLookup)
            {
                item.Key.Invalidate();
            }
        }

        private void btnDecSpeed_Click(object sender, EventArgs e)
        {
            manager.DecreaseSimulationSpeed();
        }

        private void btnIncSpeed_Click(object sender, EventArgs e)
        {
            manager.IncreaseSimulationSpeed();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
           manager.RestartSimulation();
        }

        private void btnSaveStats_Click(object sender, EventArgs e)
        { 
            if(this.manager.CurrentSimulation.CurrentSimulationResult != null)
            {
                SaveFileDialog dlgResult = new SaveFileDialog();
                System.Windows.Forms.DialogResult result = dlgResult.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK || result == System.Windows.Forms.DialogResult.Yes)
                   this.manager.CurrentSimulation.CurrentSimulationResult.ExportToExcel(dlgResult.FileName);
            }
            else
            {
                MessageBox.Show("Results still do not exist!");
            }
        }
        private void SelectComponent(PictureBox sender, MouseEventArgs e)
        {
            if (manager.CurrentSimulation.IsActive)
                return;

            int slot = pBoxToSlotIDLookup[sender];
            Crossing crossing = manager.Grid.CrossingAt(slot);
            if(crossing!=null)
            {
                foreach (Lane lane in crossing.Feeders)
                {
                    if (FindCollision(e, lane))
                    {
                        if(manager.CurrentActiveLane != null)
                            if(manager.CurrentActiveLane.Owner.Owner.IsOnTheGrid)
                                slotIDToPBoxLookup[manager.CurrentActiveLane.Owner.Owner.Column + manager.CurrentActiveLane.Owner.Owner.Row * 3].Invalidate();
                                manager.CurrentActiveComponent = lane;
                        sender.Invalidate();
                        return;
                    }
                }
                foreach (Trafficlight light in crossing.Lights)
                {
                    if (FindCollision(e, light))
                    {
                        if (manager.CurrentActiveTrafficLight != null)
                            slotIDToPBoxLookup[manager.CurrentActiveTrafficLight.Owner.Column + manager.CurrentActiveTrafficLight.Owner.Row * 3].Invalidate();
                        manager.CurrentActiveComponent = light;
                        sender.Invalidate();
                        return;
                    }
                }
            }
        }
        private bool FindCollision(MouseEventArgs e, Trafficlight light)
        {
            if (e.X < light.X || e.Y < light.Y) return false;
            if (e.X - light.X <= 16 && e.Y - light.Y <= 46)
            {
                return true;
            }

            return false;
        }

        private bool FindCollision(MouseEventArgs e, Lane lane)
        {
            if (e.X < lane.X || e.Y < lane.Y) return false;
            if (lane.Owner.From == Direction.Up || lane.Owner.From == Direction.Down)
            {
                if (e.X - lane.X <= 20 &&  e.Y - lane.Y <= 60)
                {

                    return true;
                }
            }
            else
            {
                if (e.X - lane.X <= 60 && e.Y - lane.Y <= 20)
                {
                    return true;
                }
            }

            return false;
        }
        private void updateFlowBtn_Click(object sender, EventArgs e)
        {
            if (manager.CurrentActiveLane != null)
            {
                // TODO-IF Current Lane is allowed to have pedestrians enable button, otherwise disable
                // when pedestrians activated the cars which are about to get into the lane have to wait for the pedestrians to pass (or other type of functionality)
                if (manager.CurrentActiveLane.Flow == (int)propertiesEditNUD.Value) return;
                if (cbApply.Checked || cbApplyCrossing.Checked)
                {
                    ApplyForAll();
                }
                else
                {
                    ActionStack.AddAction(new UpdateFlowAction((int)propertiesEditNUD.Value, manager.CurrentActiveLane));
                    slotIDToPBoxLookup[manager.CurrentActiveLane.Owner.Owner.Column + manager.CurrentActiveLane.Owner.Owner.Row * 3].Invalidate();
                }
            }
            else if (manager.CurrentActiveTrafficLight != null)
            {
                if (manager.CurrentActiveTrafficLight.GreenSeconds == (float)propertiesEditNUD.Value) return;
                if (cbApply.Checked || cbApplyCrossing.Checked)
                {
                    ApplyForAll();
                }
                else
                {
                    ActionStack.AddAction(new UpdateLightIntervalAction((int)propertiesEditNUD.Value, manager.CurrentActiveTrafficLight));
                    slotIDToPBoxLookup[manager.CurrentActiveTrafficLight.Owner.Column + manager.CurrentActiveTrafficLight.Owner.Row * 3].Invalidate();
                }
            }
            UpdateInterface(); 
        }

        private void ApplyForAll()
        {
            isApplying = true;
            foreach (Crossing crossing in manager.Grid.AllCrossings)
            {
                if (crossing == null) continue;
                if (manager.CurrentActiveComponent is Lane)
                {
                    if (cbApplyCrossing.Checked && manager.CurrentActiveLane.Owner.Owner != crossing)
                    {
                        continue;
                    }
                    ActionStack.AddAction(new UpdateMultipleFlowAction((int)propertiesEditNUD.Value, crossing));
                }
                if (manager.CurrentActiveComponent is Trafficlight)
                {
                    if (cbApplyCrossing.Checked && manager.CurrentActiveTrafficLight.Owner != crossing)
                    {
                        continue;
                    }

                    ActionStack.AddAction(new UpdateMultipleIntervalAction((int)propertiesEditNUD.Value, crossing));
                }
                slotIDToPBoxLookup[crossing.Column + crossing.Row * 3].Invalidate();
            }
            isApplying = false;
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
        private void ChangeBorder(PictureBox thePictureBox, bool isActive)
        {
            if (isActive)
            {
                thePictureBox.BorderStyle = BorderStyle.Fixed3D;
                ChangeGridSlotsCursor(true);
            }
            else
            {
                thePictureBox.BorderStyle = BorderStyle.FixedSingle;
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
            ChangeBorder(PicBoxTypeA, false);
            ChangeBorder(PicBoxTypeB, false);
            ChangeBorder(PicBoxTypeC, false);
            ChangeGridSlotsCursor(false);
            if (sender == button2) return;
            ClearToggles(sender, e);
            state = SystemState.None;
        }
   private void PicBoxTypeA_Click(object sender, EventArgs e)
        {
            state = SystemState.Place;
            crossingToBePlaced = new CrossingA(manager);
            ChangeBorder(PicBoxTypeA,true);
            ChangeBorder(PicBoxTypeB, false);
            ChangeBorder(PicBoxTypeC, false);
        }

        private void PicBoxTypeC_Click(object sender, EventArgs e)
        {
            state = SystemState.Place;
            crossingToBePlaced = new CrossingBRotated(manager);
            ChangeBorder(PicBoxTypeC,true);
            ChangeBorder(PicBoxTypeA, false);
            ChangeBorder(PicBoxTypeB, false);
        }

        private void PicBoxTypeB_Click(object sender, EventArgs e)
        {
            state = SystemState.Place;
            crossingToBePlaced = new CrossingB(manager);
            ChangeBorder(PicBoxTypeB,true);
            ChangeBorder(PicBoxTypeA, false);
            ChangeBorder(PicBoxTypeC, false);
        }
        private void ClearToggles(object sender, EventArgs e)
        {
            button2.FlatStyle = FlatStyle.Standard;
            rmToggled = false;
        }
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ChangeBorder(PicBoxTypeA, false);
                ChangeBorder(PicBoxTypeB, false);
                ChangeBorder(PicBoxTypeC, false);
                state = SystemState.None;
                ChangeGridSlotsCursor(false);
                button2.FlatStyle = FlatStyle.Standard;
                rmToggled = false;
            }
        }
        private void PlaceCrossing(PictureBox currentBox)
        {
            if (state != SystemState.Place) return;

            int id = pBoxToSlotIDLookup[currentBox];
            ActionStack.AddAction(new PlaceCrossingAction(id / 3, id % 3, Activator.CreateInstance(crossingToBePlaced.GetType(), manager) as Crossing));

        }
        private void RemoveCrossing(PictureBox currentBox)
        {
            if (state != SystemState.Delete) return;
            if (currentBox.Image == null) return;

            int id = pBoxToSlotIDLookup[currentBox];
            ActionStack.AddAction(new RemoveCrossingAction(id / 3, id % 3, manager.Grid.Crossings[id / 3][id % 3]));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ToggleRemoveButton();
            MainForm_Click(sender, e);
        }
        private bool rmToggled = false;
        private void ToggleRemoveButton()
        {
            if (!rmToggled)
            {
                state = SystemState.Delete;
                button2.FlatStyle = FlatStyle.Flat;
                rmToggled = true;
            }
            else
            {
                state = SystemState.None;
                button2.FlatStyle = FlatStyle.Standard;
                rmToggled = false;
            }
        }

        private void cbApplyCrossing_CheckedChanged(object sender, EventArgs e)
        {
            if(cbApplyCrossing.Checked)
            {
                cbApply.Checked = false;
            }
        }

        private void cbApply_CheckedChanged(object sender, EventArgs e)
        {
            if (cbApply.Checked)
            {
                cbApplyCrossing.Checked = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RecycleManagerForm rmform = new RecycleManagerForm(manager);
            rmform.Show();
        }

        private void PicBoxTypeB_Click_1(object sender, EventArgs e)
        {

        }
       
        private void btnSaveCrossingManager_Click(object sender, EventArgs e)
        {
            if(smform == null)
            {
                smform = new SavedManagerForm(manager);
            }
            if (smform.IsDisposed)
            {
                smform = new SavedManagerForm(manager);
            }
            smform.Show();
        }

        private void buttonCreateSnapshot_Click(object sender, EventArgs e)
        {
            if(this.manager.CurrentSimulation.CurrentSimulationResult!= null)
            {
                SaveFileDialog d = new SaveFileDialog();
                System.Windows.Forms.DialogResult result = d.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK || result == System.Windows.Forms.DialogResult.Yes)
                    this.manager.CurrentSimulation.CurrentSimulationResult.CreateSnapShot(d.FileName);
            }
            else
            {
                MessageBox.Show("Results still do not exist!");
            }
          
                
        }
    }
}
