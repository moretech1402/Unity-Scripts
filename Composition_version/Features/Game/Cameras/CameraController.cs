using Unity.Cinemachine;
using UnityEngine;

namespace MC.Game.Cameras
{
    public class CameraController : MonoBehaviour
    {
        private const int _activePriority = 100;
        private const int _inactivePriority = 0;

        public void SetCameraActive(bool active, CinemachineCamera camera)
        {
            if (camera == null)
                return;

            camera.Priority = active ? _activePriority : _inactivePriority;
        }
    }
}
