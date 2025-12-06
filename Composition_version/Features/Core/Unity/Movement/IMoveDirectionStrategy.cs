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
}
