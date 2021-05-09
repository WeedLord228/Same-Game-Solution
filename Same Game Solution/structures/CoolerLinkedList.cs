using System.Collections;
using System.Collections.Generic;

namespace Same_Game_Solution.structures
{
    public class CoolerLinkedList<T> : ICollection<T>
    {
        private LinkedList<T> _hiddenList = new LinkedList<T>();
        private Dictionary<int, LinkedList<T>> _hiddenDict = new Dictionary<int, LinkedList<T>>();

        public IEnumerator<T> GetEnumerator()
        {
            return _hiddenList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            var hash = item.GetHashCode();
            LinkedList<T> currentList;
            if (_hiddenDict.TryGetValue(hash, out currentList))
            {
                foreach (var val in currentList)
                {
                    if (val.Equals(item)) continue;
                    currentList.AddLast(item);
                    _hiddenList.AddLast(item);
                }
            }
            else
            {
                _hiddenDict.Add(hash, new LinkedList<T>(new[] {item}));
                _hiddenList.AddLast(item);
            }
        }

        public void Clear()
        {
            _hiddenList.Clear();
            _hiddenDict.Clear();
        }

        public bool Contains(T item)
        {
            if (item == null) return false;
            var hash = item.GetHashCode();
            return _hiddenDict.TryGetValue(hash, out var pretenders) && pretenders.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new System.NotImplementedException();
        }

        public int Count => _hiddenList.Count;

        public bool IsReadOnly { get; }
    }
}