using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficLights
{
    /// <summary>
    /// an action that can be undone and redone in the system
    /// </summary>
    /// <seealso cref="TrafficLights.UndoableAction" />
    public class UpdateLightIntervalAction : UndoableAction
    {
        public Trafficlight Light { get; private set; }
        public float Interval { get; private set; }
        private float previousInterval;
        private string OnString;
        public UpdateLightIntervalAction(float interval,Trafficlight light)
        {
            this.Light = light;
            this.Interval = interval;
            this.previousInterval = light.GreenSeconds;
        }
        /// <summary>
        /// Defines changes to remove
        /// </summary>
        protected override void OnUndo()
        {
            Light.GreenSeconds = previousInterval;
            OnString = "Interval change from {1} to {0}";
        }
        
        /// <summary>
        /// Defines changes to apply
        /// </summary>
        protected override void OnRedo()
        {
            Light.GreenSeconds = Interval;
            OnString = "Interval change from {0} to {1}";
        }

        /// <summary>
        /// Defines how the action will be named in String format
        /// </summary>
        protected override string AsString
        {
            get { return string.Format(OnString, previousInterval, Interval); }
        }
    }
}
