using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TrafficComponentsLibrary
{
	public class Trafficlight : UserControl
	{
		private TrafficlightColor color;

		private bool blinkOn;

		private int numberOfLamps;

		private Position whereIsRed;

		private Timer timer;

		private System.ComponentModel.Container components;

		[Category("Trafficlight")]
		[Description("Which color of the trafficlight is on")]
		public TrafficlightColor Color
		{
			get
			{
				return this.color;
			}
			set
			{
				this.SetTrafficlightColor(value);
			}
		}

		[Category("Trafficlight")]
		[Description("The number of lamps of this trafficlight (3 lamps for cars; 2 lamps for pedestrians)")]
		public int NumberOfLamps
		{
			get
			{
				return this.numberOfLamps;
			}
			set
			{
				this.SetNumberOfLamps(value);
			}
		}

		[Category("Trafficlight")]
		[Description("Defines where the red light is shown")]
		public Position WhereIsRed
		{
			get
			{
				return this.whereIsRed;
			}
			set
			{
				this.SetWhereIsRed(value);
			}
		}

		public Trafficlight()
		{
			this.InitializeComponent();
			this.timer = new Timer()
			{
				Interval = 3000
			};
			this.timer.Tick += new EventHandler(this.TimerCalled);
			base.Width = 60;
			base.Height = 20;
			this.numberOfLamps = 3;
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
			base.Name = "TSrafficlight";
			base.Resize += new EventHandler(this.Trafficlight_Resize);
			base.Paint += new PaintEventHandler(this.Trafficlight_Paint);
		}

		public new void SetBounds(int x, int y, int width, int height)
		{
			base.SetBounds(x, y, width, height);
			base.Invalidate();
		}

		private void SetNumberOfLamps(int value)
		{
			if (this.numberOfLamps != value && value >= 2 && value <= 3)
			{
				this.numberOfLamps = value;
				base.Invalidate();
			}
		}

		private void SetTrafficlightColor(TrafficlightColor value)
		{
			if (this.color != value)
			{
				this.color = value;
				this.timer.Enabled = value == TrafficlightColor.YellowBlink;
				base.Invalidate();
			}
		}

		private void SetWhereIsRed(Position value)
		{
			if (this.whereIsRed != value)
			{
				this.whereIsRed = value;
				base.Invalidate();
			}
		}

		private void TimerCalled(object myObject, EventArgs myEventArgs)
		{
			this.blinkOn = !this.blinkOn;
			base.Invalidate();
		}

		private void Trafficlight_Paint(object sender, PaintEventArgs e)
		{
			int width;
			Graphics graphic = base.CreateGraphics();
			switch (this.WhereIsRed)
			{
				case Position.Left:
				case Position.Right:
				{
					width = base.Width;
					break;
				}
				default:
				{
					width = base.Height;
					break;
				}
			}
			int numberOfLamps = width / (3 * this.NumberOfLamps + 1);
			int num = numberOfLamps;
			int width1 = 0;
			int height = 0;
			int num1 = 0;
			int height1 = 0;
			int width2 = 0;
			int height2 = 0;
			switch (this.WhereIsRed)
			{
				case Position.Left:
				{
					width1 = 2 * numberOfLamps;
					height = base.Height / 2;
					num1 = width - width1;
					height1 = height;
					width2 = base.Width / 2;
					height2 = height;
					break;
				}
				case Position.Right:
				{
					num1 = 2 * numberOfLamps;
					height1 = base.Height / 2;
					width1 = width - num1;
					height = height1;
					width2 = base.Width / 2;
					height2 = height1;
					break;
				}
				case Position.Up:
				{
					height = 2 * numberOfLamps;
					width1 = base.Width / 2;
					height1 = width - height;
					num1 = width1;
					height2 = base.Height / 2;
					width2 = width1;
					break;
				}
				case Position.Down:
				{
					height1 = 2 * numberOfLamps;
					num1 = base.Width / 2;
					height = width - height1;
					width1 = num1;
					height2 = base.Height / 2;
					width2 = num1;
					break;
				}
			}
			if (this.Color != TrafficlightColor.Green)
			{
				graphic.FillEllipse(Brushes.White, num1 - num, height1 - num, 2 * num, 2 * num);
			}
			else
			{
				graphic.FillEllipse(Brushes.Green, num1 - num, height1 - num, 2 * num, 2 * num);
			}
			if (this.NumberOfLamps == 3)
			{
				if (this.Color == TrafficlightColor.Yellow || this.Color == TrafficlightColor.YellowBlink && this.blinkOn)
				{
					graphic.FillEllipse(Brushes.Yellow, width2 - num, height2 - num, 2 * num, 2 * num);
				}
				else
				{
					graphic.FillEllipse(Brushes.White, width2 - num, height2 - num, 2 * num, 2 * num);
				}
			}
			if (this.Color == TrafficlightColor.Red || this.Color == TrafficlightColor.YellowBlink && this.NumberOfLamps == 2 && this.blinkOn)
			{
				graphic.FillEllipse(Brushes.Red, width1 - num, height - num, 2 * num, 2 * num);
			}
			else
			{
				graphic.FillEllipse(Brushes.White, width1 - num, height - num, 2 * num, 2 * num);
			}
			graphic.DrawRectangle(Pens.White, 0, 0, base.Width - 1, base.Height - 1);
			graphic.Dispose();
		}

		private void Trafficlight_Resize(object sender, EventArgs e)
		{
			this.SetBounds(base.Left, base.Top, base.Width, base.Height);
		}
	}
}