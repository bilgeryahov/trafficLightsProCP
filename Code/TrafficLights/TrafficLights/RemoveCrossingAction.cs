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
    public class RemoveCrossingAction : UndoableAction
    {
        protected int row, column;
        protected Crossing crossing;
        public RemoveCrossingAction(int row, int column, Crossing crossing)
        {
            this.row = row;
            this.column = column;
            this.crossing = crossing;
        }

        /// <summary>
        /// Defines changes to remove
        /// </summary>
        protected override void OnUndo()
        {
            crossing.Owner.RecycleCrossingManager.Remove(crossing);
            crossing.Owner.PlaceCrossing(crossing, row, column);
        }

        /// <summary>
        /// Defines changes to apply
        /// </summary>
        protected override void OnRedo()
        {
            crossing.Owner.RecycleCrossingManager.Add(crossing);
            crossing.Owner.RemoveCrossing(row,column);
        }

        /// <summary>
        /// Defines how the action will be named in String format
        /// </summary>
        /// <value>As string.</value>
        protected override string AsString
        {
            get { return string.Format("Remove at {0}x{1}", this.row, this.column);}
        }
    }
}
