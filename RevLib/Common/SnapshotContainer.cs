using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace RevLib
{
    public sealed class SnapshotContainer<T> : ISnapshotContainer<T>
    {
        private Stack<T> undoStack;
        private Stack<T> redoStack;
        private T _initialState;

        public SnapshotContainer()
        {
            undoStack = new Stack<T>();
            redoStack = new Stack<T>();
        }

        public void Clear()
        {
            undoStack.Clear();
            OnCollectionChanged(NotifyCollectionChangedAction.Reset);

            redoStack.Clear();
        }

        public void TakeSnapShot(T item)
        {
            undoStack.Push(item);
            OnCollectionChanged(NotifyCollectionChangedAction.Reset);
            redoStack.Clear();
        }

        public T Undo()
        {
            if (!undoStack.Any())
            {
                return _initialState;
            }

            var previous = undoStack.Pop();
            OnCollectionChanged(NotifyCollectionChangedAction.Reset);

            redoStack.Push(previous);

            return undoStack.Any() ? undoStack.Peek() : _initialState;

        }

        public T Redo()
        {
            if (!redoStack.Any())
            {
                throw new InvalidOperationException("No action to redo.");
            }

            var next = redoStack.Pop();

            undoStack.Push(next);
            OnCollectionChanged(NotifyCollectionChangedAction.Reset);

            return undoStack.Peek();
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        void OnCollectionChanged(NotifyCollectionChangedAction action)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(action));
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return undoStack.Reverse().GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return undoStack.Reverse().GetEnumerator();
        }

        public void SetInitialState(T item)
        {
            _initialState = item;
        }

        public bool CanUndo()
        {
            return undoStack.Any();
        }

        public bool CanRedo()
        {
            return redoStack.Any();
        }
    }
}
