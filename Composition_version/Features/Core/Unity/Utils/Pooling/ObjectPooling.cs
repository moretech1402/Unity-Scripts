using System.Linq;
using Core.Patterns.Pooling;
using UnityEngine;

namespace Core.Unity.Utils
{
    /// <summary>
    /// A pool of simple generic objects for instances of monobehaviour.
    /// </summary>
    /// <typeparam name="T">The type of monobehaviour a Pool.</typeparam>
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private readonly Pool<T> _pool;

        private readonly T _prefab;
        private readonly Transform _parentTransform;

        /// <summary>
        /// Initializes a new pool of objects.
        /// </summary>
        public ObjectPool(T prefab, Transform parentTransform, int initialSize = 0, Strategy strategy = Strategy.List)
        {
            _prefab = prefab;
            _parentTransform = parentTransform;
            _pool = new Pool<T>(() => CreateInstance(), strategy, initialSize);
        }

        /// <summary>
        /// Gets an object of pool. If the pool is empty, a new object is created.
        /// </summary>
        /// <returns>An active object of type T.</returns>
        public T Get()
        {
            T obj = _pool.Get();
            obj.gameObject.SetActive(true);
            return obj;
        }

        /// <summary> Returns an object to pool. The object is deactivated. </summary>
        public void Return(T obj)
        {
            if (obj == null) return;

            obj.gameObject.SetActive(false);
            _pool.Return(obj);
        }

        public void ReturnAllActiveObjects()
        {
            var objectsToReturn = _pool.ActiveObjects.ToList();
            foreach (var obj in objectsToReturn)
            {
                Return(obj);
            }
        }

        /// <summary> Clean and destroy all objects in pool. </summary>
        public void ClearAndDestroyAll()
        {
            _pool.ReturnAllActiveObjects();

            foreach (var obj in _pool.AllObjects.Where(obj => obj != null))
            {
                Object.Destroy(obj.gameObject);
            }
            
            _pool.Clear();
        }

        private T CreateInstance()
        {
            T obj = Object.Instantiate(_prefab, _parentTransform);
            obj.gameObject.SetActive(false);
            return obj;
        }
    }
}