using System.Collections.Generic;

namespace Core.Patterns.Pooling
{
    /// <summary>
    /// Pooling Fifo Strategy (First-In, First-Out) or that maintains the order using a list.
    /// </summary>
    /// <typeparam name="T">The type of monobehaviour a Pool.</typeparam>
    public class ListPoolingStrategy<T> : IPoolingStrategy<T>
    {
        private readonly List<T> _pool = new();

        public void Add(T obj)
        {
            _pool.Add(obj);
        }

        public T Get()
        {
            if (_pool.Count == 0) return default;

            T obj = _pool[0];
            _pool.RemoveAt(0);
            return obj;
        }

        public int Count => _pool.Count;

        public void Clear()
        {
            _pool.Clear();
        }

        public bool Contains(T obj)
        {
            return _pool.Contains(obj);
        }

        public IEnumerable<T> AllObjects => _pool;
    }
}