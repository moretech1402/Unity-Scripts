using System;
using Damages;
using Skills.Effects;
using Stats;
using UnityEngine;

public enum Target
{
    User, Ally, Enemy, Allies, Enemies
}

namespace Skills
{

    [CreateAssetMenu(menuName = "Skills/Active")]
    public class ActiveSkill : Skill
    {
        [SerializeField] Target target = Target.Enemy;
        [SerializeField] ResourceCost[] costs = new[] { new ResourceCost(StatKeys.MP, 3) };
        [SerializeField] Effect effect = new();

        public Damage[] Damage => effect.Damage;
    }

    [Serializable]
    public struct ResourceCost
    {
        [SerializeField] private StatKeys resource;
        [SerializeField] private float cost;

        public readonly StatKeys Resource => resource;
        public readonly float Cost => cost;

        public ResourceCost(StatKeys resource = StatKeys.MP, float cost = 3)
        {
            this.resource = resource;
            this.cost = cost;
        }
    }

}
