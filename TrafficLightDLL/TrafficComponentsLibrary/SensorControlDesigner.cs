using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Security.Permissions;
using System.Windows.Forms.Design;

namespace TrafficComponentsLibrary
{
	[PermissionSet(SecurityAction.Demand, Name="FullTrust")]
	public class SensorControlDesigner : ControlDesigner
	{
		public SensorControlDesigner()
		{
		}

		protected override void PreFilterEvents(IDictionary events)
		{
			base.PreFilterEvents(events);
			events.Remove("Click");
		}

		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			properties.Remove("Dock");
		}
	}
}