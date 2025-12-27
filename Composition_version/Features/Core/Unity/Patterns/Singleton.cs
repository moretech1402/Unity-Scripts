using UnityEngine;

namespace MC.Core.Unity.Patterns
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = FindFirstObjectByType<T>();
                if (Instance == null)
                {
                    CreateSingletonInstance();
                }
            }
            else if (Instance != this)
            {
                Debug.LogError($"[Singleton] Multiple {typeof(T)} instances! Destroying.");
                Destroy(gameObject);
            }
        }

        private void CreateSingletonInstance()
        {
            GameObject singletonGO = new(typeof(T).Name + " Singleton");
            Instance = singletonGO.AddComponent<T>();
            Debug.LogWarning($"[Singleton] Instance of {typeof(T)} auto-created.");
        }
    }
}