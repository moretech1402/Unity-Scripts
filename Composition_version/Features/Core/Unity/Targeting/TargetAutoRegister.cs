using UnityEngine;

namespace MC.Core.Unity.Targeting
{
    public class TargetAutoRegister : MonoBehaviour, ITarget
    {
        [SerializeField] private GameObject _targetObject;

        public bool IsAlive { get; set; } = true;
        public Vector3 GetPosition() => _targetObject.transform.position;

        private ITargetRegistryService _service;
        private bool _isRegistered;

        void Awake()
        {
            if (_targetObject == null)
                _targetObject = gameObject;
        }

        void OnEnable()
        {
            TryRegister();
        }

        void OnDisable()
        {
            if (_isRegistered && _service != null)
            {
                _service.Unregister(this);
                _isRegistered = false;
            }
        }

        private void TryRegister()
        {
            if (!ServiceLocator.TryGet<ITargetRegistryService>(out _service))
            {
                StartCoroutine(RegisterNextFrame());
                return;
            }

            _service.Register(this);
            _isRegistered = true;
        }

        private System.Collections.IEnumerator RegisterNextFrame()
        {
            yield return null;
            if (this != null && enabled)
                TryRegister();
        }
    }
}
