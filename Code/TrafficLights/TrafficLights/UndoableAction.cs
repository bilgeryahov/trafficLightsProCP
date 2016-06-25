using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    /// <summary>
    /// abstract class containing basic logic for undoing and redoing of actions
    /// </summary>
    public abstract class UndoableAction
    {
        /// <summary>
        /// True = Is done, False = is not done / undone
        /// </summary>
        protected bool isApplied = false;
        /// <summary>
        /// Gets a value indicating whether this instance is undone.
        /// </summary>
        /// <value><c>true</c> if this instance is undone; otherwise, <c>false</c>.</value>
        public bool IsUndone { get { return !isApplied; } }
        /// <summary>
        /// Gets a value indicating whether this instance is done.
        /// </summary>
        /// <value><c>true</c> if this instance is done; otherwise, <c>false</c>.</value>
        public bool IsDone { get { return isApplied; } }

        /// <summary>
        /// Initializes a new instance of the UndoableAction class.
        /// </summary>
        public UndoableAction()
        {
        }

        /// <summary>
        /// Removes changes defined by the action
        /// </summary>
        public void Undo()
        {
            if (IsUndone) return;
            OnUndo();
            this.isApplied = false;
        }

        /// <summary>
        /// Applies changes defined by the action
        /// </summary>
        public void Redo()
        {
            if (IsDone) return;
            OnRedo();
            this.isApplied = true;
        }

        /// <summary>
        /// Calls Redo
        /// </summary>
        public void Apply()
        {
            Redo();
        }

        /// <summary>
        /// Calls Undo
        /// </summary>
        public void Cancel()
        {
            Undo();
        }

        /// <summary>
        /// Defines changes to remove
        /// </summary>
        protected abstract void OnUndo();
        /// <summary>
        /// Defines changes to apply
        /// </summary>
        protected abstract void OnRedo();
        /// <summary>
        /// Defines how the action will be named in String format
        /// </summary>
        /// <value>As string.</value>
        protected abstract string AsString { get; }
        /// <summary>
        /// Defines how the action will be in string format
        /// </summary>
        /// <returns>(IsDone?Perform:Undo)+AsString</returns>
        public override string ToString()
        {
            return
                (this.IsDone ? "Perform " : "Undo ")
                +
                this.AsString;
        }
    }
}
