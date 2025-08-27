using System.Collections.Generic;

namespace Core.Patterns.Pooling
{
    /// <summary>
    /// Estrategia de pooling LIFO (Last-In, First-Out) usando un Stack.
    /// </summary>
    /// <typeparam name="T">El tipo de MonoBehaviour a pool.</typeparam>
    public class StackPoolingStrategy<T> : IPoolingStrategy<T>
    {
        private readonly Stack<T> _pool = new();

        public void Add(T obj)
        {
            _pool.Push(obj);
        }

        public T Get()
        {
            return _pool.Pop();
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