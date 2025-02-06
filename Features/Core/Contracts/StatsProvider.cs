using UnityEngine;

namespace Core.Contracts
{
    public abstract class StatsProvider : MonoBehaviour
    {
        public abstract float GetMovementSpeed();
        public abstract float GetJumpForce();
    }

}
