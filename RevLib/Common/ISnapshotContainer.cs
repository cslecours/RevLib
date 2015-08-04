using System.Collections.Generic;
using System.Collections.Specialized;

namespace RevLib
{
    public interface ISnapshotContainer<T> : INotifyCollectionChanged, IEnumerable<T>
    {
        void SetInitialState(T item);
        void Clear();
        T Redo();
        void TakeSnapShot(T item);
        T Undo();
        bool CanUndo();
        bool CanRedo();
    }
}
