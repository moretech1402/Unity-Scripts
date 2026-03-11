using UnityEngine;

namespace MC.Core.Unity.Pooling.LifeCycle
{
    public class DefaultPoolGetStrategy<T> : IPoolGetStrategy<T> where T : Component
    {
        public void OnGet(T instance)
        {
            instance.gameObject.SetActive(true);
        }
    }
}