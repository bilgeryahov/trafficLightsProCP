using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    /// <summary>
    /// Container that handles Undo and Redo of actions
    /// </summary>
    public class ActionStack
    {
        private Stack<UndoableAction> undoableStack = new Stack<UndoableAction>();
        public UndoableAction[] UndoableActions { get { return undoableStack.ToArray(); } }
        private Stack<RedoableAction> redoableStack = new Stack<RedoableAction>();
        public RedoableAction[] RedoableActions { get { return redoableStack.ToArray(); } }

        public bool CanUndo { get { return undoableStack.Count > 0; } }
        public bool CanRedo { get { return redoableStack.Count > 0; } }

        public void AddAction(UndoableAction action)
        {
            if (redoableStack.Count > 0) redoableStack.Clear();

            undoableStack.Push(action);
        }

        /// <summary>Undoes the last action performed, if any</summary>
        public void Undo()
        {
            if (!CanUndo) return;
            UndoableAction action = undoableStack.Pop();
            action.Apply();

            redoableStack.Push(action.ReverseAction);
        }

        /// <summary>Redoes the last action undone, if any</summary>
        public void Redo()
        {
            if (!CanRedo) return;

            RedoableAction action = redoableStack.Pop();
            action.Apply();
        }

        public void Clear()
        {
            redoableStack.Clear();
            undoableStack.Clear();
        }
    }
}
