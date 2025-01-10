using System;
using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using UnityEngine;

namespace Stats
{
    public class StatsHandlerGO : AStatsHandler, IConfigurable<StatsData>
    {
        [SerializeField] StatsData data;

        Dictionary<StatKeys, float> temporaryMod, permanentMod;

        public override float GetCurrent(StatKeys stat) => data.Get(stat) + temporaryMod[stat] + permanentMod[stat];

        public void Configure(StatsData stats) => data = stats;

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
