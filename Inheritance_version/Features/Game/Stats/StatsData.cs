using System.Collections.Generic;
using UnityEngine;

namespace Stats
{
    [CreateAssetMenu(menuName = "Stats")]
    public class StatsData : ScriptableObject
    {
        [SerializeField] int level = 1;
        [SerializeField] float movementSpeed = 5;

        Dictionary<StatKeys, float> dict;

        public float Get(StatKeys stat) => dict[stat];

        private void OnEnable() {
            dict = new(){ { StatKeys.MovementSpeed, movementSpeed } };
        }
    }

}
