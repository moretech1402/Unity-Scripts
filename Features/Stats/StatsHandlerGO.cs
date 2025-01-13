using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Stats
{
    public class StatsHandlerGO : StatsHandlerBase
    {
        [SerializeField] StatsData data;

        Dictionary<StatKeys, float> temporaryMod, permanentMod;

        public override void Configure(StatsData stats) => data = stats;
        public override float GetCurrent(StatKeys stat) => data.Get(stat) + temporaryMod[stat] + permanentMod[stat];

        private Dictionary<StatKeys, float> GetDefaultDict() =>
            Enum.GetValues(typeof(StatKeys))
                .Cast<StatKeys>()
                .ToDictionary(stat => stat, stat => 0f);

        private void Awake()
        {
            temporaryMod = GetDefaultDict();
            permanentMod = GetDefaultDict();
        }
    }

}
