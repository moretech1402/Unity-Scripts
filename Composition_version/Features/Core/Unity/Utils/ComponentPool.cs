using UnityEngine;
using UnityEngine.Pool;

namespace MC.Core.Unity.Utils
{
    public class ComponentPool<T> where T : Component
    {
        private readonly T _prefab;
        private readonly Transform _parent;
        private ObjectPool<T> _pool;

        public ComponentPool(T prefab, Transform parent)
        {
            _prefab = prefab;
            _parent = parent;
            InitializePool();
        }

        private void InitializePool()
        {
            _pool = new ObjectPool<T>(
                () => Object.Instantiate(_prefab, _parent),
                (element) => element.gameObject.SetActive(true),
                (element) => element.gameObject.SetActive(false));
        }

        public T Get() => _pool.Get();

        public void Release(T element) => _pool.Release(element);
    }
}