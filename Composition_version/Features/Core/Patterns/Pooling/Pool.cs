using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace Core.Patterns.Pooling
{
    public enum Strategy { Stack, List }

    /// <summary>
    /// A pool of simple generic objects.
    /// Allows you to configure the pooling strategy (LIFO, FIFO/orderly).
    /// </summary>
    public class Pool<T>
    {
        private readonly IPoolingStrategy<T> _strategy;
        private readonly List<T> _activeObjs = new();
        readonly Func<T> _createMethod;

        public ReadOnlyCollection<T> ActiveObjects => _activeObjs.AsReadOnly();

        public Pool(Func<T> createMethod, Strategy strategy, int initialSize = 3)
        {
            _createMethod = createMethod ?? throw new ArgumentNullException(
                nameof(createMethod), "Create method cannot be null.");

            _strategy = strategy == Strategy.Stack ? new StackPoolingStrategy<T>()
                : new ListPoolingStrategy<T>();

            InitializePool(initialSize);
        }

        public T Get()
        {
            T obj = _strategy.Count > 0 ? _strategy.Get() : _createMethod();
            _activeObjs.Add(obj);
            return obj;
        }

        /// <summary> Returns an object to pool. </summary>
        public void Return(T obj)
        {
            if (obj == null) return;

            if (_strategy.Contains(obj))
            {
                Debug.LogWarning($"ObjectPool: Attempted to return an object ({obj}) that is already in the pool. Skipping.");
                return;
            }

            _strategy.Add(obj);
            _activeObjs.Remove(obj);
        }

        public void ReturnAllActiveObjects()
        {
            var objectsToReturn = _activeObjs.ToList();
            foreach (var obj in objectsToReturn)
            {
                Return(obj);
            }
            _activeObjs.Clear();
        }

        public IEnumerable<T> AllObjects => _strategy.AllObjects;

        public void Clear()
        {
            _activeObjs.Clear();
            _strategy.Clear();
        }

        private void InitializePool(int initialSize)
        {
            for (int i = 0; i < initialSize; i++)
            {
                _strategy.Add(_createMethod());
            }
        }
    }
}