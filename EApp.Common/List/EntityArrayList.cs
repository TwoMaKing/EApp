using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;

namespace EApp.Common.List
{
    public class EntityArrayList<TEntity> : MarshalByRefObject, ICollection<TEntity>, IEnumerable<TEntity>, ICollection, IList, IEntityArrayList
    {
        public event NotifyCollectionChangedEventHandler ArrayListChanged;

        private List<TEntity> internalArrayList = new List<TEntity>();

        #region Custom public methods & properties

        public virtual void AddRange(TEntity[] items)
        {
            this.internalArrayList.AddRange(items);

            this.OnNotifyEntityArrayListChanged(NotifyCollectionChangedAction.Add, items, this.internalArrayList.Count - 1);
        }

        public TEntity this[int index]
        {
            get
            {
                return this.internalArrayList[index];
            }
            set
            {
                this.internalArrayList[index] = value;
            }
        }

        public TEntity[] ToArray()
        {
            return this.internalArrayList.ToArray();
        }

        public TEntity Find(Predicate<TEntity> match)
        {
            return this.internalArrayList.Find(match);
        }

        public bool Exists(Func<TEntity, bool> predicate)
        {
            return this.internalArrayList.Count(predicate).Equals(0);
        }

        #endregion

        #region ICollection<TEntity> members

        public void Add(TEntity item)
        {
            this.internalArrayList.Add(item);

            this.OnNotifyEntityArrayListChanged(NotifyCollectionChangedAction.Add, new TEntity[] { item }, this.internalArrayList.Count - 1);
        }

        public void Clear()
        {
            this.internalArrayList.Clear();

            this.OnNotifyEntityArrayListChanged(NotifyCollectionChangedAction.Reset, this.internalArrayList, 0);
        }

        public bool Contains(TEntity item)
        {
            return this.internalArrayList.Contains(item);
        }

        public void CopyTo(TEntity[] array, int arrayIndex)
        {
            this.internalArrayList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get 
            {
                return this.internalArrayList.Count;
            }
        }

        public bool IsReadOnly
        {
            get 
            {
                return false;
            }
        }

        public bool Remove(TEntity item)
        {
            int itemIndex = this.internalArrayList.IndexOf(item);

            bool removed = this.internalArrayList.Remove(item);

            if (removed)
            {
                this.OnNotifyEntityArrayListChanged(NotifyCollectionChangedAction.Remove, new TEntity[] { item }, itemIndex);
            }

            return removed;
        }

        public void RemoveAt(int index)
        {
            TEntity itemToRemoved = this.internalArrayList[index];

            this.internalArrayList.RemoveAt(index);

            this.OnNotifyEntityArrayListChanged(NotifyCollectionChangedAction.Remove, new TEntity[] { itemToRemoved }, index);
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return this.internalArrayList.GetEnumerator();
        }

        #endregion

        #region IEnumerable<TEntity> members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.internalArrayList.GetEnumerator();
        }

        #endregion

        #region IEntityArrayList event

        protected virtual void OnNotifyEntityArrayListChanged(NotifyCollectionChangedAction action, IList changedItems, int startingIndex) 
        {
            NotifyCollectionChangedEventArgs e = new NotifyCollectionChangedEventArgs(action, changedItems, startingIndex);

            if (this.ArrayListChanged != null)
            {
                this.ArrayListChanged(this, e);
            }
        }

        #endregion

        #region ICollection members

        void ICollection.CopyTo(Array array, int index)
        {
            this.internalArrayList.CopyTo((TEntity[])array, index);
        }

        int ICollection.Count
        {
            get 
            { 
                return this.internalArrayList.Count; 
            }
        }

        bool ICollection.IsSynchronized
        {
            get 
            { 
                return false; 
            }
        }

        private object syncRoot;

        object ICollection.SyncRoot
        {
            get
            {
                if (this.syncRoot == null)
                {
                    Interlocked.CompareExchange(ref this.syncRoot, new object(), null);
                }
                return this.syncRoot;
            }
        }

        #endregion

        #region IList members

        int IList.Add(object value)
        {
            if (value != null)
            {
                this.internalArrayList.Add((TEntity)value);
            }

            return this.Count - 1;
        }

        void IList.Clear()
        {
            this.internalArrayList.Clear();
        }

        bool IList.Contains(object value)
        {
            if (value == null)
            {
                return false;
            }

            return this.internalArrayList.Contains((TEntity)value);
        }

        int IList.IndexOf(object value)
        {
            if (value == null)
            {
                return -1;
            }

            return this.internalArrayList.IndexOf((TEntity)value);
        }

        void IList.Insert(int index, object value)
        {
            if (value == null)
            {
                return;
            }

            this.internalArrayList.Insert(index, (TEntity)value);
        }

        bool IList.IsFixedSize
        {
            get 
            { 
                return false; 
            }
        }

        bool IList.IsReadOnly
        {
            get 
            { 
                return false; 
            }
        }

        void IList.Remove(object value)
        {
            if (value == null)
            {
                return;
            }

            this.internalArrayList.Remove((TEntity)value);
        }

        void IList.RemoveAt(int index)
        {
            this.internalArrayList.RemoveAt(index);
        }

        object IList.this[int index]
        {
            get
            {
                return this.internalArrayList[index];
            }
            set
            {
                this.internalArrayList[index] = (TEntity)value;
            }
        }

        #endregion
    }
}
