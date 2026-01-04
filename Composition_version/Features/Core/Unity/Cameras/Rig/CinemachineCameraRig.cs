using UnityEngine;
using Unity.Cinemachine;

namespace MC.Core.Unity.Cameras.Rig
{
    public class CinemachineCameraRig : CameraRig
    {
        [SerializeField] CinemachineCamera _camera;

        protected override void SetCameraActive(bool active)
        {
            _camera.Priority = active ? 100 : 0;
        }
    }
}
