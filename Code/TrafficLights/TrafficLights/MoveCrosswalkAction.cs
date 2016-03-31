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
    public class MoveCrosswalkAction : UndoableAction
    {

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
        /// <value>As string.</value>
        protected override string AsString
        {
            get { throw new NotImplementedException(); }
        }
    }
}
