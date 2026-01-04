using UnityEngine;

namespace MC.Core.Unity.Cameras.Rig
{
    public class ClassicCameraRig : CameraRig
    {
        [SerializeField] Camera _camera;

        protected override void SetCameraActive(bool active)
        {
            _camera.enabled = active;
        }
    }
}
