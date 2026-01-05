using MC.Core.Definitions;
using UnityEngine;

namespace MC.Core.Unity.Definitions
{
    public abstract class RuntimeDefinitionSO<T> : ScriptableObject, IRuntimeDefinition<T>
    {
        private T _cached;

        protected virtual bool CacheInstance => false;

        public T Instance
            => CacheInstance
                ? _cached ??= CreateInstance()
                : CreateInstance();

        protected abstract T CreateInstance();
    }

}