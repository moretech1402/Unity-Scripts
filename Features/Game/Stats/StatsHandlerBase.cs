using Core.Interfaces;
using UnityEngine;

namespace Stats
{
    public abstract class StatsHandlerBase : MonoBehaviour, IConfigurable<StatsData>
    {
        public abstract void Configure(StatsData stats);

        public abstract float GetCurrent(StatKeys stat);
    }
}
