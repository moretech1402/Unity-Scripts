using System.Collections;
using System.Collections.Generic;
using Stats;
using UnityEngine;

namespace Ailments
{
    [CreateAssetMenu(menuName = "Ailments/Stat Mod")]
    public class AilmentStatMod : Ailment
    {
        [SerializeField] StatMod mod = new (StatKeys.MovementSpeed, "mov * 0.5");
        
        public override void Apply()
        {
            throw new System.NotImplementedException();
        }
    }

}
