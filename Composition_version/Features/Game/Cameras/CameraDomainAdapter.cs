using MC.Core.Events;
using MC.Game.Characters.View;
using Unity.Cinemachine;
using UnityEngine;

namespace MC.Game.Cameras
{
    public class CameraDomainAdapter : MonoBehaviour
    {
        [SerializeField] private CameraController _cameraController;

        [SerializeField] private int _activePriority = 100;
        [SerializeField] private int _inactivePriority = 0;

        [Header("Cameras")]
        [SerializeField] private CinemachineCamera _basicCamera;
        [SerializeField] private CinemachineCamera _targeterCamera;

        private IEventBus _eventBus;

        void OnEnable()
        {
            _eventBus = GlobalEventBus.Instance;
            _eventBus.Subscribe<CharacterSpawnedEvent>(HandleCharacterSpawned);
        }

        void OnDisable()
        {
            _eventBus.Unsubscribe<CharacterSpawnedEvent>(HandleCharacterSpawned);
        }

        private void HandleCharacterSpawned(CharacterSpawnedEvent @event)
        {
            _targeterCamera.Follow = @event.Transform;

            SetCameraActive(_targeterCamera, true);
            SetCameraActive(_basicCamera, false);
        }

        private void SetCameraActive(CinemachineCamera cam, bool active)
        {
            if (cam == null) return;
            cam.Priority = active ? _activePriority : _inactivePriority;
        }
    }
}
