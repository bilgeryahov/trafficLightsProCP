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
    public abstract class UpdateLightIntervalAction : UndoableAction
    {
        public Trafficlight Light { get; private set; }
        public int Interval { get; private set; }
        private int previousInterval;
        /// <summary>
        /// Defines changes to remove
        /// </summary>
        protected override void OnUndo()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Defines changes to apply
        /// </summary>
        protected override void OnRedo()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Defines how the action will be named in String format
        /// </summary>
        protected override string AsString
        {
            get { throw new NotImplementedException(); }
        }
    }
}
