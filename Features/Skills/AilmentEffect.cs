using System;
using Ailments;
using Core.Contracts;
using UnityEngine;

[Serializable]
public class AilmentEffect
{
    [SerializeField] Ailment ailment;
    [SerializeField] Target target;
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

