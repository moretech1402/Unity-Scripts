using System;
using Core.Contracts;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public class StatMod : KeyMod<StatKeys>
    {
        public StatMod(StatKeys stat = StatKeys.Attack, string formula = "10") :base(stat, formula) { }

        public override float GetValue()
        {
            throw new NotImplementedException();
        }
    }

}
