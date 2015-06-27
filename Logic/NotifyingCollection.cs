using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace OpenMesh
{

    public class NotifyingCollection<T> : IList<T>, INotifyCollectionChanged<T>
    {

        public void Insert(int index, T item)
        {
            this.List.Insert(index, item);

            this.OnCollectionChanged(new CollectionChangedEventArgs<T>(
                new CollectionChangedItem<T>[] { },
                new CollectionChangedItem<T>[] { new CollectionChangedItem<T>(index, item) }));
            this.OnItemCameIn(new ItemTransferedEventArgs<T>(item));
        }

        public void RemoveAt(int index)
        {
            var T = this.List[index];
            this.List.RemoveAt(index);
            this.OnCollectionChanged(new CollectionChangedEventArgs<T>(
                new CollectionChangedItem<T>[] { new CollectionChangedItem<T>(index, T) },
                new CollectionChangedItem<T>[] { }));
            this.OnItemWentOut(new ItemTransferedEventArgs<T>(T));
        }

        public T this[int index]
        {
            get
            {
                return this.List[index];
            }
            set
            {
                var T = this.List[index];
                this.List[index] = value;

                this.OnCollectionChanged(new CollectionChangedEventArgs<T>(
                    new CollectionChangedItem<T>[] { new CollectionChangedItem<T>(index, T) },
                    new CollectionChangedItem<T>[] { new CollectionChangedItem<T>(index, value) }));
                this.OnItemWentOut(new ItemTransferedEventArgs<T>(T));
                this.OnItemCameIn(new ItemTransferedEventArgs<T>(value));
            }
        }

        public void Add(T item)
        {
            this.List.Add(item);

            this.OnCollectionChanged(new CollectionChangedEventArgs<T>(
                new CollectionChangedItem<T>[] { },
                new CollectionChangedItem<T>[] { new CollectionChangedItem<T>(this.List.Count - 1, item) }));
            this.OnItemCameIn(new ItemTransferedEventArgs<T>(item));
        }

        public void Clear()
        {
            var A = new CollectionChangedItem<T>[this.List.Count];
            for (int i = 0; i < A.Length; i++)
            {
                A[i] = new CollectionChangedItem<T>(i, this.List[i]);
            }

            this.List.Clear();

            this.OnCollectionChanged(new CollectionChangedEventArgs<T>(
                A,
                new CollectionChangedItem<T>[] { }));
            for (int i = 0; i < A.Length; i++)
            {
                this.OnItemWentOut(new ItemTransferedEventArgs<T>(A[i].Item));
            }
        }

        public bool Remove(T item)
        {
            var I = this.List.IndexOf(item);
            if (I == -1)
                return false;

            var T = this.List[I];
            this.List.RemoveAt(I);

            this.OnCollectionChanged(new CollectionChangedEventArgs<T>(
                new CollectionChangedItem<T>[] { new CollectionChangedItem<T>(I, T) },
                new CollectionChangedItem<T>[] { }));
            this.OnItemWentOut(new ItemTransferedEventArgs<T>(T));

            return true;
        }

        public void Move(int OldIndex, int NewIndex)
        {
            var T = this.List[OldIndex];
            this.List.RemoveAt(OldIndex);
            this.List.Insert(NewIndex, T);

            this.OnCollectionChanged(new CollectionChangedEventArgs<T>(
                new CollectionChangedItem<T>[] { new CollectionChangedItem<T>(OldIndex, T) },
                new CollectionChangedItem<T>[] { new CollectionChangedItem<T>(NewIndex, T) }));
        }

        public int IndexOf(T item)
        {
            return this.List.IndexOf(item);
        }

        public bool Contains(T item)
        {
            return this.List.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.List.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get
            {
                return this.List.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public List<T>.Enumerator GetEnumerator()
        {
            return this.List.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #region CollectionChanged Event
        public event EventHandler<CollectionChangedEventArgs<T>> CollectionChanged;

        protected virtual void OnCollectionChanged(CollectionChangedEventArgs<T> E)
        {
            if (this.CollectionChanged != null)
            {
                this.CollectionChanged.Invoke(this, E);
            }
        }
        #endregion

        #region ItemCameIn Event
        public event EventHandler<ItemTransferedEventArgs<T>> ItemCameIn;

        protected virtual void OnItemCameIn(ItemTransferedEventArgs<T> E)
        {
            if (this.ItemCameIn != null)
            {
                this.ItemCameIn.Invoke(this, E);
            }
        }
        #endregion

        #region ItemWentOut Event
        public event EventHandler<ItemTransferedEventArgs<T>> ItemWentOut;

        protected virtual void OnItemWentOut(ItemTransferedEventArgs<T> E)
        {
            if (this.ItemWentOut != null)
            {
                this.ItemWentOut.Invoke(this, E);
            }
        }
        #endregion

        private readonly List<T> List = new List<T>();
        private static readonly Boolean IsNotifyPropertyChanged = typeof(INotifyPropertyChanged).IsAssignableFrom(typeof(T));

    }

    public interface INotifyCollectionChanged<T>
    {

        event EventHandler<CollectionChangedEventArgs<T>> CollectionChanged;
        event EventHandler<ItemTransferedEventArgs<T>> ItemCameIn;
        event EventHandler<ItemTransferedEventArgs<T>> ItemWentOut;

    }

    public class ItemTransferedEventArgs<T> : EventArgs
    {

        public ItemTransferedEventArgs(T Item)
        {
            this._Item = Item;
        }

        private readonly T _Item;

        public T Item
        {
            get
            {
                return this._Item;
            }
        }

    }

    public class CollectionChangedEventArgs<T> : EventArgs
    {

        public CollectionChangedEventArgs(CollectionChangedItem<T>[] RemovedOnes, CollectionChangedItem<T>[] AddedOnes)
        {
            this._RemovedOnes = RemovedOnes;
            this._AddedOnes = AddedOnes;
        }

        private readonly CollectionChangedItem<T>[] _RemovedOnes;

        public CollectionChangedItem<T>[] RemovedOnes
        {
            get
            {
                return this._RemovedOnes;
            }
        }

        private readonly CollectionChangedItem<T>[] _AddedOnes;

        public CollectionChangedItem<T>[] AddedOnes
        {
            get
            {
                return this._AddedOnes;
            }
        }

    }

    public struct CollectionChangedItem<T>
    {

        public CollectionChangedItem(Int32 Position, T Item)
        {
            this._Position = Position;
            this._Item = Item;
        }

        private readonly Int32 _Position;

        public Int32 Position
        {
            get
            {
                return this._Position;
            }
        }

        private readonly T _Item;

        public T Item
        {
            get
            {
                return this._Item;
            }
        }

    }

}
