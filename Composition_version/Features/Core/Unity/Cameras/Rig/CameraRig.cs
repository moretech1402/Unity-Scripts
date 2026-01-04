using UnityEngine;

namespace MC.Core.Unity.Cameras.Rig
{
    public abstract class CameraRig : MonoBehaviour
    {
        [SerializeField] protected AudioListener _audioListener;

        public void SetActive(bool active)
        {
            SetCameraActive(active);

            if (_audioListener != null)
                _audioListener.enabled = active;
        }

        protected abstract void SetCameraActive(bool active);
    }
}
