using System;
using Core.Gameplay;
using Damages;
using Skills.Effects;
using Stats;
using UnityEngine;

namespace Skills
{

    [CreateAssetMenu(menuName = "Skills/Active")]
    public class ActiveSkillData : SkillData
    {
        [SerializeField] Target target = Target.Enemy;
        [SerializeField] ResourceCost[] costs = new[] { new ResourceCost(3, StatKeys.MP) };
        [SerializeField] Effect effect = new();

        public Damage[] Damage => effect.Damage;

        public void Execute(StatsHandlerBase caster, StatsHandlerBase target)
        {
            var text = "";
            foreach (var damageEl in Damage)
            {
                var calculatedDamage = SkillEvaluator.Evaluate(damageEl.Formula, caster, target);
                text += $"{calculatedDamage} damage of {damageEl.Affinity}";
            }
            Debug.Log(text);
        }
    }

    [Serializable]
    public struct ResourceCost
    {
        [SerializeField] private float cost;
        [SerializeField] private StatKeys resource;

        public readonly StatKeys Resource => resource;
        public readonly float Cost => cost;

        public ResourceCost(float cost = 3, StatKeys resource = StatKeys.MP)
        {
            this.cost = cost;
            this.resource = resource;
        }
    }

}
