using System;
using Ailments;
using Damages;
using Skills.AilmentEffects;
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

    [Serializable]
    public struct OnIntervalEffect{
        [SerializeField] Effect effect;
        [SerializeField] Interval interval;
    }
}
