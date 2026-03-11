using System.Collections.Generic;
using MC.Core.Unity.Pooling.LifeCycle;
using UnityEngine;
using UnityEngine.Pool;

namespace MC.Core.Unity.Pooling
{
    /// <summary>
    /// Generic prefab-based object pool for Unity components.
    ///
    /// This pool instantiates objects from a prefab and manages their lifecycle
    /// using Unity's ObjectPool. Retrieved instances are automatically activated,
    /// and released instances are automatically deactivated.
    ///
    /// The pool also keeps track of all currently active instances, enabling
    /// bulk operations such as releasing every active object at once.
    /// </summary>
    /// <typeparam name="T">
    /// The Unity Component type handled by the pool.
    /// </typeparam>
    public class PrefabPool<T> : ObjectPool<T> where T : Component
    {
        /// <summary>
        /// List of instances currently checked out from the pool.
        /// </summary>
        private readonly List<T> _activeInstances = new();

        private readonly IPoolGetStrategy<T> _getStrategy;

        /// <summary>
        /// Creates a new prefab pool with activation, deactivation,
        /// and destruction behaviors configured automatically.
        /// </summary>
        public PrefabPool(
            T prefab,
            Transform parent,
            IPoolGetStrategy<T> getStrategy = null,
            int defaultCapacity = 10,
            int maxSize = 100)
            : base(
                () => Object.Instantiate(prefab, parent),
                actionOnGet: instance =>
                {
                    instance.gameObject.SetActive(true);
                    (getStrategy ?? new DefaultPoolGetStrategy<T>()).OnGet(instance);
                },
                actionOnRelease: instance => instance.gameObject.SetActive(false),
                actionOnDestroy: instance => Object.Destroy(instance.gameObject),
                collectionCheck: false,
                defaultCapacity: defaultCapacity,
                maxSize: maxSize
            )
        {
            _getStrategy = getStrategy ?? new DefaultPoolGetStrategy<T>();
        }

        /// <summary>
        /// Pre-instantiates a number of objects and returns them to the pool.
        ///
        /// Useful to avoid runtime allocation spikes by warming up the pool
        /// ahead of time.
        /// </summary>
        public void Warmup(int count)
        {
            var items = new T[count];

            for (int i = 0; i < count; i++)
                items[i] = Get();

            for (int i = 0; i < count; i++)
                Release(items[i]);
        }

        /// <summary>
        /// Retrieves an instance from the pool and tracks it as active.
        /// </summary>
        public new T Get()
        {
            var obj = base.Get();
            _activeInstances.Add(obj);
            return obj;
        }

        /// <summary>
        /// Returns an instance to the pool and removes it from active tracking.
        /// </summary>
        public new void Release(T element)
        {
            base.Release(element);
            _activeInstances.Remove(element);
        }

        /// <summary>
        /// Releases all currently active instances back to the pool.
        ///
        /// Intended for cleanup scenarios such as scene resets or system shutdown.
        /// </summary>
        public void ReleaseAll()
        {
            var copy = new List<T>(_activeInstances);

            foreach (var item in copy)
                Release(item);
        }
    }
}