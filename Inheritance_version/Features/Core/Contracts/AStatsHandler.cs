using UnityEngine;

namespace Stats
{
    public abstract class AStatsHandler : MonoBehaviour
    {
        public abstract float GetCurrent(StatKeys stat);
    }

}
