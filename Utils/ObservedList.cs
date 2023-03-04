using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace E404.Utils
{
    [Serializable]
    public class ObservedList<T> : IList<T>
    {
        public delegate void ChangedDelegate(int index, T oldValue, T newValue);
        [SerializeField] private List<T> _list = new List<T>();

        public event ChangedDelegate PropertyUpdated;
        public event Action CollectionUpdated;

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            _list.Add(item);
            CollectionUpdated?.Invoke();
        }

        public void Clear()
        {
            _list.Clear();
            CollectionUpdated?.Invoke();
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            var output = _list.Remove(item);
            CollectionUpdated?.Invoke();

            return output;
        }

        public int Count => _list.Count;
        public bool IsReadOnly => false;

        public int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _list.Insert(index, item);
            CollectionUpdated?.Invoke();
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
            CollectionUpdated?.Invoke();
        }

        public void AddRange(IEnumerable<T> collection)
        {
            _list.AddRange(collection);
            CollectionUpdated?.Invoke();
        }

        public void RemoveAll(Predicate<T> predicate)
        {
            _list.RemoveAll(predicate);
            CollectionUpdated?.Invoke();
        }

        public void InsertRange(int index, IEnumerable<T> collection)
        {
            _list.InsertRange(index, collection);
            CollectionUpdated?.Invoke();
        }

        public void RemoveRange(int index, int count)
        {
            _list.RemoveRange(index, count);
            CollectionUpdated?.Invoke();
        }

        public T this[int index]
        {
            get { return _list[index]; }
            set
            {
                var oldValue = _list[index];
                _list[index] = value;
                PropertyUpdated?.Invoke(index, oldValue, value);
                CollectionUpdated?.Invoke();
            }
        }
    }
}
//EOF.