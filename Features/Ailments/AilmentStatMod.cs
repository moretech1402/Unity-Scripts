using System.Collections;
using System.Collections.Generic;
using Stats;
using UnityEngine;

namespace Ailments
{
    public class AilmentStatMod : Ailment
    {
        StatKeys stat = StatKeys.MovementSpeed;
        int fixedMod = 0;
        float percentMod = .5f;
        
        public override void Apply()
        {
            throw new System.NotImplementedException();
        }
    }

}
