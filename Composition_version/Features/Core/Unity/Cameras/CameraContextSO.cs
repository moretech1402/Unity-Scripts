using UnityEngine;

namespace MC.Core.Unity.Cameras
{
    public interface ICameraContextDefinition
    {
        CameraContext Context { get; }
    }

    [CreateAssetMenu(menuName = "Core/Unity/Camera/Create new Camera Context")]
    public class CameraContextSO : ScriptableObject, ICameraContextDefinition
    {
        [SerializeField] string Id;

        CameraContext _cached;

        public CameraContext Context
        {
            get
            {
                _cached ??= new CameraContext(Id);
                return _cached;
            }
        }
    }

}