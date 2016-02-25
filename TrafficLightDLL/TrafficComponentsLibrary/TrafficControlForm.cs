using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TrafficComponentsLibrary
{
	public class TrafficControlForm : Form
	{
		private CrossingForm[] crossings;

		private TrafficComponentsLibrary.Messenger messenger;

		private IContainer components;

		[Category("Messenger")]
		[Description("Set messenger")]
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

		public TrafficControlForm()
		{
			this.InitializeComponent();
			this.crossings = new CrossingForm[20];
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		public CrossingForm GetCrossingForm(int crossingId)
		{
			if (crossingId < 0 || crossingId > 19)
			{
				throw new Exception("This position in the array is not possible");
			}
			if (this.crossings[crossingId] == null)
			{
				throw new Exception("This position in the array does not contain a crossing");
			}
			return this.crossings[crossingId];
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.messenger = new TrafficComponentsLibrary.Messenger(this.components);
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(472, 266);
			base.IsMdiContainer = true;
			base.Name = "TrafficControlForm";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "TFormRegeling";
		}

		public void InsertCrossingForm(int crossingId, CrossingForm crossingForm, int x, int y, int width, int height)
		{
			if (crossingId < 0 || crossingId > 19)
			{
				throw new Exception("This position in the array is not possible");
			}
			if (this.crossings[crossingId] != null)
			{
				throw new Exception("This position in the array is occupied");
			}
			crossingForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			crossingForm.MdiParent = this;
			this.crossings[crossingId] = crossingForm;
			crossingForm.Crossing.Id = crossingId;
			crossingForm.Messenger.MessengerEvent += new TrafficComponentsLibrary.Messenger.MessengerHandler(this.Receive);
			crossingForm.SetBounds(x, y, width, height);
			crossingForm.Show();
		}

		private void Receive(TrafficComponentsLibrary.Messenger sender, int crossingId)
		{
			if (crossingId < 0 || crossingId > 19)
			{
				throw new Exception("This position in the array is not possible");
			}
			if (this.crossings[crossingId] != null)
			{
				this.crossings[crossingId].Messenger.Receive(sender);
			}
		}

		public void RemoveCrossingForm(int crossingId)
		{
			if (crossingId < 0 || crossingId > 19)
			{
				throw new Exception("This position in the array is not possible");
			}
			if (this.crossings[crossingId] != null)
			{
				this.crossings[crossingId].Messenger.MessengerEvent -= new TrafficComponentsLibrary.Messenger.MessengerHandler(this.Receive);
				this.crossings[crossingId].Visible = false;
				this.crossings[crossingId] = null;
			}
		}
	}
}