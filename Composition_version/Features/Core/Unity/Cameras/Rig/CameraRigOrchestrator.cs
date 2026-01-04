using MC.Core.Patterns;
using MC.Core.Unity.Patterns;

namespace MC.Core.Unity.Cameras.Rig
{
    public sealed class CameraRigOrchestrator : Singleton<CameraRigOrchestrator>
    {
        CameraRig _current;
        IEventBus _eventBus;

        new void Awake()
        {
            base.Awake();
            _eventBus = GlobalEventBus.Instance;
            _eventBus.Subscribe<CameraContextChangedEvent>(OnCameraContextChanged);
        }

        void OnDestroy()
        {
            _eventBus.Unsubscribe<CameraContextChangedEvent>(OnCameraContextChanged);
        }

        void OnCameraContextChanged(CameraContextChangedEvent evt)
        {
            var rig = CameraRigRegistry.Get(evt.Context);

            if (_current == rig)
                return;

            if (_current != null)
                _current.SetActive(false);

            _current = rig;

            if (_current != null)
                _current.SetActive(true);
        }
    }

}
