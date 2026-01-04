using UnityEngine;

namespace MC.Core.Unity.Cameras.Rig
{
    public class CameraRigProvider : MonoBehaviour
    {
        [SerializeField] CameraContextSO _contextDefinition;
        [SerializeField] CameraRig _cameraRig;

        private CameraContext Context => _contextDefinition.Context;

        void Awake()
        {
            CameraRigRegistry.Register(Context, _cameraRig);
        }

        void OnDestroy()
        {
            CameraRigRegistry.Unregister(Context);
        }
    }
}
