using UnityEngine;
using UnityEngine.Events;

namespace MC.Core.Unity
{
    public class LifecycleEventEmitter : MonoBehaviour
    {
        public UnityEvent OnAwaked;
        public UnityEvent OnStarted;
        public UnityEvent OnEnabled;
        public UnityEvent OnDisabled;

        private void Awake() => OnAwaked?.Invoke();
        private void Start() => OnStarted?.Invoke();
        private void OnEnable() => OnEnabled?.Invoke();
        private void OnDisable() => OnDisabled?.Invoke();
    }
}
