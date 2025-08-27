using System;
using Core.Contracts;
using UnityEngine;

namespace Damages
{
    [Serializable]
    public class Damage
    {
        [SerializeField] string formula = "a.atk * 4 - b.def * 2";
        [SerializeField] Affinities.Keys affinity = Affinities.Keys.Slash;

        public string Formula => formula;
        public Affinities.Keys Affinity => affinity;

        public Damage(string formula, Affinities.Keys affinity){
            this.formula = formula;
            this.affinity = affinity;
        }
    }

    [Serializable]
    public class DamageComposition : EffectComposition<Damage>
    {
        public DamageComposition(Damage[] composition) : base(composition){}

        protected override void ApplyEffect(Damage effect, Target target)
        {
            throw new NotImplementedException();
        }
    }

}
