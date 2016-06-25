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
    public class PlaceCrossingAction : RemoveCrossingAction
    {
        public PlaceCrossingAction(int row, int column, Crossing crossing) : base(row, column, crossing) { }
        /// <summary>
        /// Defines changes to remove
        /// </summary>
        protected override void OnUndo()
        {
            base.OnRedo();
        }

        /// <summary>
        /// Defines changes to apply
        /// </summary>
        protected override void OnRedo()
        {
            base.OnUndo();
        }

        /// <summary>
        /// Defines how the action will be named in String format
        /// </summary>
        /// <value>As string.</value>
        protected override string AsString
        {
            get { return string.Format("Place at {0}x{1}", this.row, this.column); }
        }
    }
}
