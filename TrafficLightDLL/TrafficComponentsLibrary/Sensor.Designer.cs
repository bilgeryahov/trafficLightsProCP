using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TrafficComponentsLibrary
{
	[Designer(typeof(SensorControlDesigner))]
	public class Sensor : UserControl
	{
		private Color onColor;

		private Color offColor;

		private OnOff state;

		private System.ComponentModel.Container components;

		[Category("Sensor")]
		[Description("Set 'sensor off' color")]
		public Color OffColor
		{
			get
			{
				return this.offColor;
			}
			set
			{
				this.SetOffColor(value);
			}
		}

		[Category("Sensor")]
		[Description("Set 'sensor on' color")]
		public Color OnColor
		{
			get
			{
				return this.onColor;
			}
			set
			{
				this.SetOnColor(value);
			}
		}

		[Category("Sensor")]
		[Description("Set sensor state")]
		public OnOff State
		{
			get
			{
				return this.state;
			}
			set
			{
				this.SetState(value);
			}
		}

		public Sensor()
		{
			this.InitializeComponent();
			this.onColor = Color.Yellow;
			this.offColor = Color.White;
			base.Width = 24;
			base.Height = 24;
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
			base.Name = "TSensor";
			base.Resize += new EventHandler(this.Sensor_Resize);
			base.Paint += new PaintEventHandler(this.Sensor_Paint);
			base.MouseDown += new MouseEventHandler(this.Sensor_MouseDown);
		}

		private void Sensor_MouseDown(object sender, MouseEventArgs e)
		{
			if (this.SensorPressed != null)
			{
				this.SensorPressed(sender, e);
			}
		}

		private void Sensor_Paint(object sender, PaintEventArgs e)
		{
			Graphics graphic = base.CreateGraphics();
			if (this.state != OnOff.On)
			{
				Brush solidBrush = new SolidBrush(this.offColor);
				graphic.FillEllipse(solidBrush, 0, 0, base.Width, base.Height);
			}
			else
			{
				Brush brush = new SolidBrush(this.onColor);
				graphic.FillEllipse(brush, 0, 0, base.Width, base.Height);
			}
			graphic.Dispose();
		}

		private void Sensor_Resize(object sender, EventArgs e)
		{
			this.SetBounds(base.Left, base.Top, base.Width, base.Height);
		}

		public new void SetBounds(int x, int y, int width, int height)
		{
			base.SetBounds(x, y, width, height);
			base.Invalidate();
		}

		private void SetOffColor(Color value)
		{
			if (this.offColor != value)
			{
				this.offColor = value;
				base.Invalidate();
			}
		}

		private void SetOnColor(Color value)
		{
			if (this.onColor != value)
			{
				this.onColor = value;
				base.Invalidate();
			}
		}

		private void SetState(OnOff value)
		{
			if (this.state != value)
			{
				this.state = value;
				base.Invalidate();
			}
		}

		[Category("Sensor")]
		[Description("Sensor event ")]
		public event Sensor.SensorPressedHandler SensorPressed;

		public delegate void SensorPressedHandler(object sender, EventArgs e);
	}
}