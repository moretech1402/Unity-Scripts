using UnityEngine;

namespace MC.Core.Unity.Movement
{
    public interface IMoveDirectionStrategy
    {
        Vector3 GetDirection(Vector2 input, Camera camera);
    }

    public abstract class MoveDirectionStrategySO: ScriptableObject, IMoveDirectionStrategy
    {
        public abstract Vector3 GetDirection(Vector2 input, Camera camera);
    }

    public class SideScroller2DMoveStrategy : IMoveDirectionStrategy
    {
        public Vector3 GetDirection(Vector2 input, Camera camera)
        {
            return new Vector3(input.x, 0, 0);
        }
    }

    public class CameraRelative3DMoveStrategy : IMoveDirectionStrategy
    {
        public Vector3 GetDirection(Vector2 input, Camera camera)
        {
            Transform camTransf = camera.transform;
            Vector3 camForward = Vector3.ProjectOnPlane(camTransf.forward, Vector3.up).normalized;
            Vector3 camRight = Vector3.ProjectOnPlane(camTransf.right, Vector3.up).normalized;
            return (camForward * input.y + camRight * input.x).normalized;
        }
    }
}
