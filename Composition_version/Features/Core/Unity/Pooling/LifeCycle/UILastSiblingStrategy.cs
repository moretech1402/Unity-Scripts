using UnityEngine;

namespace MC.Core.Unity.Pooling.LifeCycle
{
    public class UILastSiblingStrategy<T> : IPoolGetStrategy<T> where T : Component
    {
        public void OnGet(T instance)
        {
            instance.gameObject.SetActive(true);
            instance.transform.SetAsLastSibling();
        }
    }
}
