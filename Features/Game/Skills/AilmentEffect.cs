using System;
using Ailments;
using Core.Contracts;
using Core.Gameplay;
using UnityEngine;

namespace Skills.AilmentEffects
{
    public enum Intention{ Inflict, Heal }
    [Serializable]
    public class AilmentEffect
    {
        [SerializeField] Ailment ailment;
        [SerializeField] Subject target;
        [SerializeField] Intention intention;
        [SerializeField][Range(0, 1)] float successProb = 1;
    }

    [Serializable]
    public class AilmentEffectComposition : EffectComposition<AilmentEffect>
    {
        public AilmentEffectComposition(AilmentEffect[] ailments) : base(ailments) { }

        protected override void ApplyEffect(AilmentEffect effect, Target target)
        {
            throw new NotImplementedException();
        }
    }

}

