using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ailments
{
    public enum SpecialEffects{ Paralyze, Silence }

    [CreateAssetMenu(menuName = "Ailments/Special")]
    public class AilmentSpecial : Ailment
    {
        [SerializeField] SpecialEffects[] effect;

        public override void Apply()
        {
            throw new System.NotImplementedException();
        }
    }

}
