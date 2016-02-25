using System;
using System.Windows.Forms;

namespace TrafficComponentsLibrary
{
	public class Road
	{
		private RoadType roadType;

		private bool crosswalk;

		private Crossing owner;

		public bool Crosswalk
		{
			get
			{
				return this.crosswalk;
			}
			set
			{
				this.SetCrosswalk(value);
				this.owner.Invalidate();
			}
		}

		public RoadType Type
		{
			get
			{
				return this.roadType;
			}
			set
			{
				this.SetRoadType(value);
				this.owner.Invalidate();
			}
		}

		public Road(Crossing owner)
		{
			this.owner = owner;
			this.Crosswalk = true;
		}

		private void SetCrosswalk(bool value)
		{
			if (value != this.crosswalk)
			{
				this.crosswalk = value;
			}
		}

		private void SetRoadType(RoadType value)
		{
			if (value != this.roadType)
			{
				this.roadType = value;
			}
		}
	}
}