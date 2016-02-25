using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace TrafficComponentsLibrary
{
	public class Crossing : Panel
	{
		private System.ComponentModel.Container components;

		private int id;

		private bool autoStretch;

		private Road roadLeft;

		private Road roadRight;

		private Road roadUp;

		private Road roadDown;

		private int oldWidth;

		private int oldHeight;

		private BufferedGraphics offscreenBmp;

		[Browsable(false)]
		public bool AutoStretch
		{
			get
			{
				return this.autoStretch;
			}
			set
			{
				this.autoStretch = value;
			}
		}

		[Category("Crossing")]
		[Description("Identifier of this crossing")]
		public int Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		[Browsable(false)]
		public Road RoadDown
		{
			get
			{
				return this.roadDown;
			}
			set
			{
				this.roadDown = value;
			}
		}

		[Category("Crossing")]
		[Description("Crosswalk on the downward side")]
		public bool RoadDown_Crosswalk
		{
			get
			{
				return this.roadDown.Crosswalk;
			}
			set
			{
				this.roadDown.Crosswalk = value;
			}
		}

		[Category("Crossing")]
		[Description("Type of road on the downward side")]
		public RoadType RoadDown_RoadType
		{
			get
			{
				return this.roadDown.Type;
			}
			set
			{
				this.roadDown.Type = value;
			}
		}

		[Browsable(false)]
		public Road RoadLeft
		{
			get
			{
				return this.roadLeft;
			}
			set
			{
				this.roadLeft = value;
			}
		}

		[Category("Crossing")]
		[Description("Crosswalk on the leftward side")]
		public bool RoadLeft_Crosswalk
		{
			get
			{
				return this.roadLeft.Crosswalk;
			}
			set
			{
				this.roadLeft.Crosswalk = value;
			}
		}

		[Category("Crossing")]
		[Description("Type of road on the leftward side")]
		public RoadType RoadLeft_RoadType
		{
			get
			{
				return this.roadLeft.Type;
			}
			set
			{
				this.roadLeft.Type = value;
			}
		}

		[Browsable(false)]
		public Road RoadRight
		{
			get
			{
				return this.roadRight;
			}
			set
			{
				this.roadRight = value;
			}
		}

		[Category("Crossing")]
		[Description("Crosswalk on the rightward side")]
		public bool RoadRight_Crosswalk
		{
			get
			{
				return this.roadRight.Crosswalk;
			}
			set
			{
				this.roadRight.Crosswalk = value;
			}
		}

		[Category("Crossing")]
		[Description("Type of road on the rightward side")]
		public RoadType RoadRight_RoadType
		{
			get
			{
				return this.roadRight.Type;
			}
			set
			{
				this.roadRight.Type = value;
			}
		}

		[Browsable(false)]
		public Road RoadUp
		{
			get
			{
				return this.roadUp;
			}
			set
			{
				this.roadUp = value;
			}
		}

		[Category("Crossing")]
		[Description("Crosswalk on the upward side")]
		public bool RoadUp_Crosswalk
		{
			get
			{
				return this.roadUp.Crosswalk;
			}
			set
			{
				this.roadUp.Crosswalk = value;
			}
		}

		[Category("Crossing")]
		[Description("Type of road on the upward side")]
		public RoadType RoadUp_RoadType
		{
			get
			{
				return this.roadUp.Type;
			}
			set
			{
				this.roadUp.Type = value;
			}
		}

		public Crossing()
		{
			this.InitializeComponent();
			this.roadLeft = new Road(this);
			this.roadRight = new Road(this);
			this.roadUp = new Road(this);
			this.roadDown = new Road(this);
			this.roadLeft.Type = RoadType.None;
			this.roadRight.Type = RoadType.TwoLane;
			this.roadUp.Type = RoadType.ThreeLanesL;
			this.roadDown.Type = RoadType.ThreeLanesR;
			this.BackColor = Color.Black;
			this.Dock = DockStyle.None;
			this.autoStretch = true;
			this.oldWidth = 0;
			this.oldHeight = 0;
			base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			base.SetStyle(ControlStyles.UserPaint, true);
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
		}

		private void Arrow(int x, int y, int w, int arrowtype, int rotate, bool mirror)
		{
			int i;
			int[,] numArray = new int[,] { { 14, 0, 1, 9, 1, 2, 0, 2, 2, 0, 4, 2, 3, 2, 3, 5, 4, 5, 4, 4, 6, 6, 4, 8, 4, 7, 3, 7, 3, 9, 2, 8 }, { 10, 0, 1, 7, 1, 3, 3, 3, 4, 3, 4, 2, 6, 4, 4, 6, 4, 5, 3, 5, 3, 7, 2, 6, 0, 0, 0, 0, 0, 0, 0, 0 }, { 4, 0, 1, 9, 1, 0, 3, 0, 3, 9, 2, 8, 4, 2, 3, 2, 3, 5, 4, 5, 4, 4, 6, 6, 4, 8, 4, 7, 3, 7, 3, 9 } };
			int[] numArray1 = new int[32];
			int num = w / 10;
			for (i = 0; i < 32; i++)
			{
				numArray1[i] = numArray[arrowtype, i];
			}
			for (i = 1; i < 16; i++)
			{
				numArray1[2 * i] = num * numArray1[2 * i] - 3 * num;
				numArray1[2 * i + 1] = num * numArray1[2 * i + 1] - 5 * num;
			}
			if (mirror)
			{
				for (i = 1; i < 16; i++)
				{
					numArray1[2 * i] = -numArray1[2 * i];
				}
			}
			for (int j = 0; j < rotate; j++)
			{
				for (i = 1; i < 16; i++)
				{
					int num1 = numArray1[2 * i];
					int num2 = numArray1[2 * i + 1];
					numArray1[2 * i] = num2;
					numArray1[2 * i + 1] = -num1;
				}
			}
			x = x + w / 2;
			y = y + w / 2;
			Graphics graphics = this.offscreenBmp.Graphics;
			GraphicsPath graphicsPath = new GraphicsPath();
			Point point = new Point(x + numArray1[2], y + numArray1[3]);
			Point point1 = new Point(0, 0);
			for (i = 0; i < numArray1[0]; i++)
			{
				point1.X = x + numArray1[2 * (i + 1)];
				point1.Y = y + numArray1[1 + 2 * (i + 1)];
				graphics.DrawLine(Pens.White, point, point1);
				graphicsPath.AddLine(point, point1);
				point = point1;
			}
			point1.X = x + numArray1[2];
			point1.Y = y + numArray1[3];
			graphics.DrawLine(Pens.White, point, point1);
			graphicsPath.AddLine(point, point1);
			System.Drawing.Region region = new System.Drawing.Region(graphicsPath);
			graphics.FillRegion(Brushes.White, region);
			graphicsPath.Dispose();
		}

		private void Cross(int x1, int y1, int x2, int y2, bool horizontal)
		{
			if (horizontal)
			{
				for (int i = 0; i < 10; i++)
				{
					this.ThickLine(x1, y1 + (2 * i + 1) * (y2 - y1) / 21, x2, y1 + (2 * i + 2) * (y2 - y1) / 21);
				}
				return;
			}
			for (int j = 0; j < 10; j++)
			{
				this.ThickLine(x1 + (2 * j + 1) * (x2 - x1) / 21, y1, x1 + (2 * j + 2) * (x2 - x1) / 21, y2);
			}
		}

		private void Crossing_Resize(object sender, EventArgs e)
		{
			this.SetBounds(0, 0, base.ClientRectangle.Width, base.ClientRectangle.Height);
			this.oldWidth = base.ClientRectangle.Width;
			this.oldHeight = base.ClientRectangle.Height;
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
			base.Name = "Crossing";
			base.Resize += new EventHandler(this.Crossing_Resize);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			int width = base.ClientRectangle.Width;
			int height = base.ClientRectangle.Height;
			int num = width / 3;
			int height1 = base.Height / 3;
			int num1 = (width - num) / 2;
			int height2 = (base.Height - height1) / 2;
			int num2 = (width + num) / 2;
			int height3 = (base.Height + height1) / 2;
			BufferedGraphicsContext current = BufferedGraphicsManager.Current;
			current.MaximumBuffer = new System.Drawing.Size(base.Width + 1, base.Height + 1);
			this.offscreenBmp = current.Allocate(e.Graphics, new Rectangle(0, 0, base.Width, base.Height));
			Graphics graphics = this.offscreenBmp.Graphics;
			graphics.DrawRectangle(Pens.Black, num1, height2, num2 - num1, height3 - height2);
			graphics.FillRectangle(Brushes.Black, num1, height2, num2 - num1, height3 - height2);
			if (this.roadLeft.Type != RoadType.None)
			{
				graphics.DrawRectangle(Pens.Black, 0, height2, num1, height3 - height2);
				graphics.FillRectangle(Brushes.Black, 0, height2, num1, height3 - height2);
				this.ThickLine(0, height2, num1 + 3, height2 + 3);
				this.ThickLine(0, height2 + height1 / 2 - 2, num1, height2 + height1 / 2 + 2);
				this.ThickLine(0, height3 - 3, num1 + 3, height3);
				switch (this.roadLeft.Type)
				{
					case RoadType.ThreeLanesL:
					case RoadType.ThreeLanesR:
					{
						this.ThickLine(0, height2 + 3 * height1 / 4 - 1, num1, height2 + 3 * height1 / 4 + 1);
						if (this.roadLeft.Type == RoadType.ThreeLanesR)
						{
							this.Arrow(num1 - num / 4, height2 + 2 * height1 / 4, height1 / 4, 0, 3, true);
							this.Arrow(num1 - num / 4, height2 + 3 * height1 / 4, height1 / 4, 1, 3, false);
						}
						if (this.roadLeft.Type != RoadType.ThreeLanesL)
						{
							break;
						}
						this.Arrow(num1 - num / 4, height2 + 2 * height1 / 4, height1 / 4, 1, 3, true);
						this.Arrow(num1 - num / 4, height2 + 3 * height1 / 4, height1 / 4, 0, 3, false);
						break;
					}
				}
				if (this.roadLeft.Crosswalk)
				{
					this.Cross(num1 - num / 2, height2, num1 - num / 4, height3, true);
				}
			}
			else
			{
				this.ThickLine(num1, height2, num1 + 3, height3);
			}
			if (this.roadRight.Type != RoadType.None)
			{
				graphics.DrawRectangle(Pens.Black, num2, height2, width - num2, height3 - height2);
				graphics.FillRectangle(Brushes.Black, num2, height2, width - num2, height3 - height2);
				this.ThickLine(num2 - 3, height2, width, height2 + 3);
				this.ThickLine(num2, height2 + height1 / 2 - 2, width, height2 + height1 / 2 + 2);
				this.ThickLine(num2 - 3, height3 - 3, width, height3);
				switch (this.roadRight.Type)
				{
					case RoadType.ThreeLanesL:
					case RoadType.ThreeLanesR:
					{
						this.ThickLine(num2, height2 + height1 / 4 - 1, width, height2 + height1 / 4 + 1);
						if (this.roadRight.Type == RoadType.ThreeLanesR)
						{
							this.Arrow(num2, height2, height1 / 4, 1, 1, false);
							this.Arrow(num2, height2 + height1 / 4, height1 / 4, 0, 1, true);
						}
						if (this.roadRight.Type != RoadType.ThreeLanesL)
						{
							break;
						}
						this.Arrow(num2, height2, height1 / 4, 0, 1, false);
						this.Arrow(num2, height2 + height1 / 4, height1 / 4, 1, 1, true);
						break;
					}
				}
				if (this.roadRight.Crosswalk)
				{
					this.Cross(num2 + num / 4, height2, num2 + num / 2, height3, true);
				}
			}
			else
			{
				this.ThickLine(num2 - 3, height2, num2, height3);
			}
			if (this.roadUp.Type != RoadType.None)
			{
				graphics.DrawRectangle(Pens.Black, num1, 0, num2 - num1, height2);
				graphics.FillRectangle(Brushes.Black, num1, 0, num2 - num1, height2);
				this.ThickLine(num1, 0, num1 + 3, height2 + 3);
				this.ThickLine(num1 + num / 2 - 2, 0, num1 + num / 2 + 2, height2);
				this.ThickLine(num2 - 3, 0, num2, height2 + 3);
				switch (this.roadUp.Type)
				{
					case RoadType.ThreeLanesL:
					case RoadType.ThreeLanesR:
					{
						this.ThickLine(num1 + num / 4 - 1, 0, num1 + num / 4 + 1, height2);
						if (this.roadUp.Type == RoadType.ThreeLanesR)
						{
							this.Arrow(num1, height2 - height1 / 4, num / 4, 1, 2, false);
							this.Arrow(num1 + num / 4, height2 - height1 / 4, num / 4, 0, 2, true);
						}
						if (this.roadUp.Type != RoadType.ThreeLanesL)
						{
							break;
						}
						this.Arrow(num1, height2 - height1 / 4, num / 4, 0, 2, false);
						this.Arrow(num1 + num / 4, height2 - height1 / 4, num / 4, 1, 2, true);
						break;
					}
				}
				if (this.roadUp.Crosswalk)
				{
					this.Cross(num1, height2 - height1 / 2, num2, height2 - height1 / 4, false);
				}
			}
			else
			{
				this.ThickLine(num1, height2, num2, height2 + 3);
			}
			if (this.roadDown.Type != RoadType.None)
			{
				graphics.DrawRectangle(Pens.Black, num1, height3, num2 - num1, base.Height - height3);
				graphics.FillRectangle(Brushes.Black, num1, height3, num2 - num1, base.Height - height3);
				this.ThickLine(num1, height3 - 3, num1 + 3, base.Height);
				this.ThickLine(num1 + num / 2 - 2, height3, num1 + num / 2 + 2, base.Height);
				this.ThickLine(num2 - 3, height3 - 3, num2, base.Height);
				switch (this.roadDown.Type)
				{
					case RoadType.ThreeLanesL:
					case RoadType.ThreeLanesR:
					{
						this.ThickLine(num1 + 3 * num / 4 - 1, height3, num1 + 3 * num / 4 + 1, base.Height);
						if (this.roadDown.Type == RoadType.ThreeLanesR)
						{
							this.Arrow(num1 + 2 * num / 4, height3, num / 4, 0, 0, true);
							this.Arrow(num1 + 3 * num / 4, height3, num / 4, 1, 0, false);
						}
						if (this.roadDown.Type != RoadType.ThreeLanesL)
						{
							break;
						}
						this.Arrow(num1 + 2 * num / 4, height3, num / 4, 1, 0, true);
						this.Arrow(num1 + 3 * num / 4, height3, num / 4, 0, 0, false);
						break;
					}
				}
				if (this.roadDown.Crosswalk)
				{
					this.Cross(num1, height3 + height1 / 4, num2, height3 + height1 / 2, false);
				}
			}
			else
			{
				this.ThickLine(num1, height3 - 3, num2, height3);
			}
			if (this.offscreenBmp != null)
			{
				this.offscreenBmp.Render(e.Graphics);
			}
			base.OnPaint(e);
		}

		private int Round(int a, int b)
		{
			return (a + b / 2) / b;
		}

		public new void SetBounds(int x, int y, int width, int height)
		{
			base.Invalidate();
			double num = (double)width * 1 / (double)this.oldWidth;
			double num1 = (double)height * 1 / (double)this.oldHeight;
			for (int i = 0; i < base.Controls.Count; i++)
			{
				Control item = base.Controls[i];
				item.SetBounds(Convert.ToInt32((double)item.Left * num), Convert.ToInt32((double)item.Top * num1), Convert.ToInt32((double)item.Width * num), Convert.ToInt32((double)item.Height * num1));
			}
		}

		private void ThickLine(int x1, int y1, int x2, int y2)
		{
			Graphics graphics = this.offscreenBmp.Graphics;
			graphics.DrawRectangle(Pens.White, x1, y1, x2 - x1, y2 - y1);
			graphics.FillRectangle(Brushes.White, x1, y1, x2 - x1, y2 - y1);
		}
	}
}