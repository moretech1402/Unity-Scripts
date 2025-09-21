using UnityEngine;
using UnityEngine.Events;

namespace MC.Core.Unity.Targeting
{
    public interface ITargetDetector
    {
        ITarget DetectTarget();
    }

    public class NearestTargetDetector : MonoBehaviour, ITargetDetector
    {
        [SerializeField] Transform origin;
        [SerializeField] UnityEvent<Vector3> _onTargetDetected;

        void Update()
        {
            var target = DetectTarget();
            if(target != null)
                _onTargetDetected?.Invoke(target.GetPosition());
        }

        public ITarget DetectTarget() =>
            ServiceLocator.Get<ITargetRegistryService>().GetClosest(origin.position);
    }

}
