﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    /// <summary>
    /// container that handles Undo and Redo of actions
    /// </summary>
    public class ActionStack
    {
        /// <summary>Triggered when the Undo stack has been changed</summary>
        public static event Action<int, UndoableAction> OnUndoAltered = (x, y) => { };
        /// <summary>Triggered when the Redo stack has been changed</summary>
        public static event Action<int, UndoableAction> OnRedoAltered = (x, y) => { };

        /// <summary>True if any changes remain in the activity stack</summary>
        public static bool CanUndo
        {
            get
            {
                return activitiesStack.Count != 0;
            }
        }

        /// <summary>True if any changes remain in the redo stack</summary>
        public static bool CanRedo
        {
            get
            {
                return redoStack.Count != 0;
            }
        }

        public static bool HasChanges { get { return CanUndo || CanRedo; } }

        public static UndoableAction[] UndoableActions
        {
            get
            {
                List<UndoableAction> result = new List<UndoableAction>();

                result.AddRange(activitiesStack);
                result.AddRange(redoStack);

                return result.ToArray();
            }
        }

        private static Stack<UndoableAction> activitiesStack = new Stack<UndoableAction>();
        private static Stack<UndoableAction> redoStack = new Stack<UndoableAction>();

        /// <summary>Adds the specified action to be listed as an action that can be undone</summary>
        public static void AddAction(UndoableAction action)
        {
            if (redoStack.Count > 0) redoStack.Clear();

            activitiesStack.Push(action);

            //todo apply ( remove excess code )
            action.Apply();

            OnUndoAltered(activitiesStack.Count, action);
            OnRedoAltered(redoStack.Count, null);
        }

        /// <summary>Undoes the last action performed, if any</summary>
        public static void Undo()
        {
            if (!CanUndo) return;
            UndoableAction action = activitiesStack.Pop();
            action.Undo();
            redoStack.Push(action);

            OnRedoAltered(redoStack.Count, action);
            if (activitiesStack.Count == 0)
                OnUndoAltered(activitiesStack.Count, null);
            else
                OnUndoAltered(activitiesStack.Count, activitiesStack.Peek());
        }

        /// <summary>Redoes the last action undone, if any</summary>
        public static void Redo()
        {
            if (!CanRedo) return;

            UndoableAction action = redoStack.Pop();
            action.Redo();
            activitiesStack.Push(action);

            OnUndoAltered(activitiesStack.Count, action);
            if (redoStack.Count == 0)
                OnRedoAltered(redoStack.Count, null);
            else
                OnRedoAltered(redoStack.Count, redoStack.Peek());
        }

        public static void Clear()
        {
            redoStack.Clear();
            activitiesStack.Clear();
            OnUndoAltered(activitiesStack.Count, null);
            OnRedoAltered(activitiesStack.Count, null);
        }
    }
}
