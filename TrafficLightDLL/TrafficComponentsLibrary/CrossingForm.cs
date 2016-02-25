using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TrafficComponentsLibrary
{
	public class CrossingForm : Form
	{
		protected TrafficComponentsLibrary.Crossing crossing;

		protected TrafficComponentsLibrary.Messenger messenger;

		private IContainer components;

		[Category("Crossing")]
		[Description("The crossing that belongs to this form")]
		public TrafficComponentsLibrary.Crossing Crossing
		{
			get
			{
				return this.crossing;
			}
		}

		public int CrossingId
		{
			get
			{
				return this.Crossing.Id;
			}
		}

		[Category("Messenger")]
		[Description("The messenger that belongs to this form")]
		public TrafficComponentsLibrary.Messenger Messenger
		{
			get
			{
				return this.messenger;
			}
			set
			{
				this.messenger = value;
			}
		}

		public CrossingForm()
		{
			this.InitializeComponent();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.crossing = new TrafficComponentsLibrary.Crossing();
			this.messenger = new TrafficComponentsLibrary.Messenger(this.components);
			base.SuspendLayout();
			this.crossing.AutoStretch = false;
			this.crossing.BackColor = Color.Black;
			this.crossing.Dock = DockStyle.Fill;
			this.crossing.Id = 0;
			this.crossing.Location = new Point(0, 0);
			this.crossing.Name = "crossing";
			this.crossing.RoadDown_Crosswalk = false;
			this.crossing.RoadDown_RoadType = RoadType.TwoLane;
			this.crossing.RoadLeft_Crosswalk = false;
			this.crossing.RoadLeft_RoadType = RoadType.TwoLane;
			this.crossing.RoadRight_Crosswalk = false;
			this.crossing.RoadRight_RoadType = RoadType.TwoLane;
			this.crossing.RoadUp_Crosswalk = false;
			this.crossing.RoadUp_RoadType = RoadType.TwoLane;
			this.crossing.Size = new System.Drawing.Size(292, 266);
			this.crossing.TabIndex = 0;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(292, 266);
			base.Controls.Add(this.crossing);
			base.Name = "CrossingForm";
			base.StartPosition = FormStartPosition.Manual;
			this.Text = "FormCrossing";
			base.ResumeLayout(false);
		}
	}
}