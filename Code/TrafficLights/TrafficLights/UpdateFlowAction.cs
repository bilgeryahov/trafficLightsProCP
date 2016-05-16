using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    /// <summary>
    /// an action that can be undone and redone in the system
    /// </summary>
    /// <seealso cref="TrafficLights.UndoableAction" />
    public class UpdateFlowAction : UndoableAction
    {
        public Lane Lane { get; private set; }
        public int Flow { get; private set; }
        private int previousFlow;
        public UpdateFlowAction(int flow, Lane lane)
        {
            this.Lane = lane;
            this.Flow = flow;
            this.previousFlow = lane.Flow;
        }
        /// <summary>
        /// Defines changes to remove
        /// </summary>
        protected override void OnUndo()
        {
            Lane.UpdateFlow(previousFlow);
        }

        /// <summary>
        /// Defines changes to apply
        /// </summary>
        protected override void OnRedo()
        {
            Lane.UpdateFlow(Flow);
        }

        /// <summary>
        /// Defines how the action will be named in String format
        /// </summary>
        protected override string AsString
        {
            get { return string.Format("Flow changed from {0} to {1}", previousFlow, Flow); }
        }
    }
}
