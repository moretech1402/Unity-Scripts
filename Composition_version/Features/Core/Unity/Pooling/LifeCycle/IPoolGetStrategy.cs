using UnityEngine;

namespace MC.Core.Unity.Pooling.LifeCycle
{
    public interface IPoolGetStrategy<T> where T : Component
    {
        void OnGet(T instance);
    }
}
