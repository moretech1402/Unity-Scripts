using Core;
using UnityEngine;

namespace Stats
{
    public enum StatKeys { Level, MovementSpeed }

    public class StatsManager : Singleton<StatsManager>
    {
        [SerializeField] string level = "Nivel", movementSpeed = "Movement Speed";
    }

}
