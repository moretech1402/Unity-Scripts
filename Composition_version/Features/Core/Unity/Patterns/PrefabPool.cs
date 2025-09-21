using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace MC.Core.Unity.Patterns
{
    public class PrefabPool<T> : ObjectPool<T> where T : Component
    {
        private readonly List<T> _activeInstances = new();

        public PrefabPool(T prefab, Transform parent, int defaultCapacity = 10, int maxSize = 100)
            : base(
                () => Object.Instantiate(prefab, parent),
                actionOnGet: instance => instance.gameObject.SetActive(true),
                actionOnRelease: instance => instance.gameObject.SetActive(false),
                actionOnDestroy: instance => Object.Destroy(instance.gameObject),
                collectionCheck: false,
                defaultCapacity: defaultCapacity,
                maxSize: maxSize
            )
        { }

        public void Warmup(int count)
        {
            var items = new T[count];
            for (int i = 0; i < count; i++)
                items[i] = Get();
            for (int i = 0; i < count; i++)
                Release(items[i]);
        }

        public new T Get()
        {
            var obj = base.Get();
            _activeInstances.Add(obj);
            return obj;
        }

        public new void Release(T element)
        {
            base.Release(element);
            _activeInstances.Remove(element);
        }

        public void ReleaseAll()
        {
            var copy = new List<T>(_activeInstances);
            foreach (var item in copy)
            {
                Release(item);
            }
        }
    }
}
