using System;
using Damages;
using UnityEngine;

namespace Skills.Effects
{
    [Serializable]
    public class Effect
    {
        [SerializeField] DamageComposition damage;
        [SerializeField] AilmentEffectComposition ailments;

        public Damage[] Damage => damage.Composition;
    }
}
